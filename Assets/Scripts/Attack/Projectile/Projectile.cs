using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

public enum ProjectileTrackType { straight, homing, arc }

//Everything needed for instantiate a projectile that given not by projectile itself and need to set by outer classes
[System.Serializable]
public struct ProjectileArgs
{
    public bool isInit;
    public Vector3 spawnPos;
    public float DamageInfo;

    public ProjectileArgs(
        ProjectileData data, 
        Vector3 spawnPos, 
        float calculateDamageInfo, 
        Vector3 direction, 
        Entity target = null, 
        List<Entity> ignoredCollisionList = null)
    {
        this._data = data;
        this.spawnPos = spawnPos;
        this.DamageInfo = calculateDamageInfo;
        this.direction = direction;
        this.target = target;
        this.ignoredCollisionList = ignoredCollisionList;
        isInit = true;
    }

    public ProjectileData Data
    {
        get
        {
            if (_data is null)
            {
                Debug.LogWarning("Warning: ProjectileArgs.data is null!");
            }
            return _data;
        }
        set => _data = value;
    }

    public Vector3 direction;
    public Entity target; //homing target
    public List<Entity> ignoredCollisionList;
    [SerializeField]
    private ProjectileData _data;
}

public class Projectile : MonoBehaviour
{
    //Inheriter Base Values
    public bool isInit = false;
    public bool isTriggered = false; //You can't trigger it twice
    public ProjectileArgs args;
    public ProjectileTrack track;

    public float distance;
    public float time;
    public Rigidbody2D RB
    {
        get
        {
            if (rb is null)
            {
                Debug.LogWarning("Warning: ProjectileArgs.rigidbody is null!");
            }
            return rb;
        }
        set => rb = value;
    }

    public List<Entity> triggerEntities;
    [SerializeField] public Rigidbody2D rb;
    [HideInInspector] public float originalHeight = 0.0f;
    [HideInInspector] public float totalDistanceToTarget = 0.0f;
    [HideInInspector] public Vector3 targetPosition;

    private GameObject areaIndicator;

    public static GameObject InstantiateProjectile(
        ProjectileData data, 
        Vector3 spawnPos, 
        Entity instigator, 
        Vector3 attackDirection, 
        Entity target = null, 
        List<Entity> ignoredCollisionList = null)
    {
        float damageInfo = data.damage;
        return InstantiateProjectile(data, spawnPos, damageInfo, attackDirection, target, ignoredCollisionList);
    }

    public static GameObject InstantiateProjectile(
        ProjectileData data, 
        Vector3 spawnPos, 
        float damageInfo, 
        Vector3 attackDirection, 
        Entity target = null, 
        List<Entity> ignoredCollisionList = null)
    {
        ProjectileArgs projectileArgs = new ProjectileArgs(data, spawnPos, damageInfo, attackDirection, target, ignoredCollisionList); //Target is null
        return InstantiateProjectile(projectileArgs);
    }

    public static GameObject InstantiateProjectile(ProjectileArgs projectileArgs)
    {
        //! GameObject attack = PoolManager.SpawnObject(projectileArgs.Data.prefab, projectileArgs.spawnPos, Quaternion.identity);
        GameObject attack = Instantiate(projectileArgs.Data.prefab, projectileArgs.spawnPos, Quaternion.identity);
        Projectile projectileComponent = attack.GetComponent<Projectile>();
        projectileComponent.SetProjectileArgs(projectileArgs);
        return attack;
    }

    public void SetProjectileArgs(ProjectileArgs projectileArgs)
    {
        this.args = projectileArgs;
        //Only run after the cast is initialized
        if (!isInit || args.Data == projectileArgs.Data)
        {
            DataInit();
        }
        isInit = true;
        OnEnable();
    }

    void Awake()
    {
        gameObject.tag = "Projectile";
        RB = GetComponent<Rigidbody2D>();
    }

    //Be Called Each time when spawn by PoolManger
    private void OnEnable()
    {
        if (!isInit) { return; }
        isTriggered = false;
        distance = 0f;
        time = 0f;
        triggerEntities = new List<Entity>();
    }

    private void DataInit()
    {
        switch (args.Data.trackType)
        {
            case ProjectileTrackType.straight:
                rb.velocity = new Vector2(args.direction.x, args.direction.y)* args.Data.speedMultipler;
                // track = new ProjectileTrackStraight(this);
                break;
            case ProjectileTrackType.homing:
                if (args.target is null)
                {
                    Debug.LogWarning("Homing Projectile while target is null");
                    return;
                }
                track = new ProjectileTrackHoming(this);
                break;
            case ProjectileTrackType.arc:
                targetPosition = args.target.transform.position;
                originalHeight = transform.position.y;
                totalDistanceToTarget = transform.position.Distance2D(targetPosition);
                //! areaIndicator = PoolManager.SpawnByName("AttackIndicator", targetPosition, Quaternion.identity);
                //! areaIndicator.GetComponent<AttackCircleIndicator>().SetSize(args.Data.indicatorRadius);
                Debug.DrawRay(targetPosition.Flatten(), Vector3.up, Color.blue, 2f);
                track = new ProjectileTrackArc(this);
                break;
        }
    }

    /// <summary>
    /// It will activate the event list one by one in the data
    /// </summary>
    public void Trigger()
    {
        if (!isTriggered)
        {
            isTriggered = true;
            /* //!
            var actionList = args.Data.actionList;
            if (actionList != null)
            {
                for (var i = 0; i < actionList.Count; i++)
                {
                    actionList[i]?.Activate(this);
                    if (actionList[i].delayTime == 0)
                    {
                        continue;
                    }
                }
            }
            */
            Release();
        }
    }

    void FixedUpdate()
    {
        if (!isInit) { return; }
        time += Time.fixedDeltaTime;
        if (distance > args.Data.maxDistance || time > args.Data.lifeCycle)
        {
            if (args.Data.isTriggerWhenRelease)
            {
                Trigger();
            }
            else
            {
                Release();
            }
        }
        if (!isTriggered)
        {
            //! track.FixedUpdate();
        }
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (isTriggered) return;

        bool isEntity = collider.TryGetComponent(out Entity entity);
        if (!isEntity)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                Trigger();
            }
            return;
        }

        /* //!
        if (entity == args.DamageInfo.Instigator)
        {
            Physics.IgnoreCollision(args.DamageInfo.Instigator.GetComponent<Collider>(), collider);
            return;
        }

        if (!AreaAttack.IsAttackable(args.Data.attackObjectType, collider.tag))
        {
            return;
        }
        */

        if (args.ignoredCollisionList != null && args.ignoredCollisionList.Count > 0)
        {
            foreach (Entity item in args.ignoredCollisionList)
            {
                if (entity == item)
                {
                    Physics.IgnoreCollision(item.GetComponent<Collider>(), collider);
                    return;
                }
            }
        }

        triggerEntities.Add(entity);
        Trigger();
    }

    //Release the projectile
    void Release()
    {
        /* //!
        if (areaIndicator != null)
        {
            PoolManager.ReleaseObject(areaIndicator);
            areaIndicator = null;
        }
        PoolManager.ReleaseObject(gameObject);
        */
    }
}
