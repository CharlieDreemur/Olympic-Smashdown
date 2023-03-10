using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class TennisShooter : MonoBehaviour
{
    private HashSet<Enemy> _enemyInRange;
    public ProjectileData projectileData;
/*public SharedProjectileData projectileData;
    public SharedTransform target;
    public SharedTransform self;
    [SerializeField]
    private Vector3 _direction;

    public override TaskStatus OnUpdate()
    {
        _direction= new Vector2(target.Value.position.x - self.Value.position.x, target.Value.position.y - self.Value.position.y).normalized;
        Projectile.InstantiateProjectile(projectileData.Value, self.Value.position, ProjectileOwnerType.enemy, _direction, target.Value.gameObject.GetComponent<Entity>());
        return TaskStatus.Success;
    }*/
    
    // Start is called before the first frame update
    private void Awake()
    {
        _enemyInRange = new HashSet<Enemy>();
    }

    void Start()
    {
        StartCoroutine(RepeatCoro(1f));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var enemy = col.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            _enemyInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null && _enemyInRange.Contains(enemy))
        {
            _enemyInRange.Remove(enemy);
        }
    }

    IEnumerator RepeatCoro(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            var nonNullList = _enemyInRange.Where(entry => entry != null).ToList();
            if (!nonNullList.Any()) continue;
            var minDist = nonNullList.Min(
                entry => 
                    (entry.transform.position - this.transform.position).magnitude
            );
            var closest = nonNullList.Where
                (entry => 
                    Mathf.Abs(
                        (entry.transform.position - this.transform.position).magnitude - minDist) 
                    < 0.1f).ToList();
            // var nonNullList = _enemyInRange.Where(entry => entry != null).ToList();
            //if (nonNullList.Any())
            if (closest.Any())
            {
                var target = nonNullList[0].gameObject.transform;
                var direction= new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
                Projectile.InstantiateProjectile(projectileData, this.transform.position + (Vector3) direction * 1.4f, ProjectileOwnerType.player, direction, target.gameObject.GetComponent<Entity>());
                Debug.Log("Shot");
            }
        }
    }
}
