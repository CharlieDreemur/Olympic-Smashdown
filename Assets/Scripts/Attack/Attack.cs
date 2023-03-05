using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackCategory { RegularMelee, HeavyMelee, Cast }

public enum BuffCategory
{
    Attack,
    Chill,
    Defense,
    Doom,
    Hangover,
    Speed,
    Sturdy,
    Weak
}

/* //!
public enum DamageType
{
    Default,
    Melee,
    HeavyMelee,
    Explosion,
    Hangover,
    Doom,
    Magic,
    Chill,
}
*/

public interface IAttack //Should likely contain its own enum or bool regarding the cooldown, maybe also internally handle time stuff in the inheriters?
{
    public string Name { get; set; }
    public float Damage { get; set; }
    public float Radius { get; set; } //Maybe get rid of, as this is based on hitbox definitions
    public float CancelableDuration { get; set; }
    public float Cooldown { get; set; }
    public float AnimationTime { get; set; }
    public float Knockback { get; set; }
    public bool HasStun { get; set; }
    public AttackCategory WeaponType { get; set; }

    public virtual void AdditionalLogic()
    {
        return;
    }
    
    public abstract void Trigger();
}

[System.Serializable]
public struct DamageInfo
{
    public DamageInfo(
        float damageIn, 
        Entity instigatorIn = null, 
        float knockbackIn = 0.0f, 
        bool canStunIn = true
        //! List<BuffFactory> buffsToApplyIn = null,
        //! DamageType damageTypeIn = DamageType.Default
    )
    {
        //Debug.Assert(damageIn >= 0.0f);

        Damage = damageIn;
        Knockback = knockbackIn;
        CanStun = canStunIn;

        instigator = instigatorIn;
        //! DamageType = damageTypeIn;
        hitEntities = new();
        damageQueue = new();

        /* //!
        if (buffsToApplyIn == null)
        {
            BuffsToApply = new List<BuffFactory>();
        }
        else
        {
            BuffsToApply = buffsToApplyIn;
        }
        */
    }

    public float Damage;
    public float Knockback;
    public bool CanStun;
    //! public List<BuffFactory> BuffsToApply;
    //! public DamageType DamageType;
    private Entity instigator;

    public Entity Instigator
    {
        get
        {
            if (instigator is null)
            {
                //Debug.LogWarning("Warning: Instigator is null!");
            }
            return instigator;
        }
        set => instigator = value;
    }

    public HashSet<Entity> hitEntities;
    public Queue<Entity> damageQueue;
}

public static class DamageInfoExtensions
{
    public static bool HitAny(this DamageInfo damageInfo) => damageInfo.hitEntities.Count > 0;

    public static bool TryAdd(this DamageInfo damageInfo, Entity entity)
    {
        if (entity is not IDamageable || entity == damageInfo.Instigator)
        {
            return false;
        }
        bool isSuccessful = damageInfo.hitEntities.Add(entity);

        if (isSuccessful)
        {
            damageInfo.damageQueue.Enqueue(entity);
        } 

        return isSuccessful;
    }

    public static void Clear(this DamageInfo? damageInfo)
    {
        if (damageInfo == null)
        {
            return;
        }
        damageInfo.Value.hitEntities.Clear();
        damageInfo.Value.damageQueue.Clear();
        damageInfo = null;
    }

    public static void ApplyDamage(this DamageInfo damageInfo)
    {
        for (int i = 0; i < damageInfo.damageQueue.Count; i++)
        {
            Entity current = damageInfo.damageQueue.Dequeue();
            (current as IDamageable).TakeDamage(damageInfo.Damage);
            Vector3 knockbackDirection = (current.transform.position - damageInfo.Instigator.transform.position).normalized * damageInfo.Knockback;
            (current as IDamageable).TakeKnockback(knockbackDirection);
        }
    }
}
