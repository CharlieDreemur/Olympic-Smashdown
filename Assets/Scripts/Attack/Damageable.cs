using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Armor { get; set; }
    public float MaxArmor { get; set; }
    public float DamageReceivedModifier { get; set; } // Raw damage is multiplied by damage multiplier before damage is applied 
    public Vector3 KnockbackDirection { get; set; }
    //! public BuffTarget BuffTarget { get; set; }

    public virtual void ApplyDamage(DamageInfo damageInfo)
    {
        TakeDamage(damageInfo.Damage);
        OnHit(damageInfo);
        Vector3 knockbackDisplacement = KnockbackDirection * damageInfo.Knockback;
        if (knockbackDisplacement != Vector3.zero)
        {
            TakeKnockback(knockbackDisplacement);
        }
        /* //!
        if (BuffTarget != null)
        {
            BuffTarget.ApplyBuffs(damageInfo.BuffsToApply);
        }
        */
    }

    public virtual void TakeDamage(float damage)
    {
        damage *= DamageReceivedModifier; // Apply damage modifier before any calculation

        /* //!
        if (BuffTarget is not null)
        {
            damage *= BuffTarget.DefenseDamageModifier;
        }
        */
        
        if (Armor > 0.0f)
        {
            Armor -= damage;
            Armor = Mathf.Min(Armor, MaxArmor);
            if (Armor <= 0.0f)
            {
                Armor = 0.0f;
                OnArmorBreak();
            }
        }
        else
        {
            Health -= damage;
            Health = Mathf.Min(Health, MaxHealth);
            if (Health <= 0.0f)
            {
                Health = 0.0f;
                OnDeath();
            }
        }
        OnHealthUpdate(damage);
    }

    public virtual void Heal(float amount)
    {
        Health = Mathf.Min(Health + amount, MaxHealth);
        OnHealthUpdate(-amount);
    }

    public virtual void AddArmor(float amount, float overrideMaxArmor = -1)
    {
        if (overrideMaxArmor > 0.0f)
        {
            MaxArmor = overrideMaxArmor;
            if (MaxArmor < Armor)
            {
                Debug.Log("Enemy MaxArmor is less than current Armor! Setting Armor = MaxArmor");
                Armor = MaxArmor;
            }
        }
        else
        {
            // automatically grow max armor value
            MaxArmor = Mathf.Min(MaxArmor, Armor);
        }
        Armor = Mathf.Min(Armor + amount, MaxArmor);
        OnHealthUpdate(-amount);
    }

    /// <summary>
    /// Used for damage response in enemies and player
    /// </summary>
    public virtual void OnHit(DamageInfo damageInfo) {}

    /// <summary>
    /// Used for HUD updates
    /// </summary>
    public virtual void OnHealthUpdate(float damage) {}

    public virtual void OnArmorBreak() {}

    public virtual void TakeKnockback(Vector3 displacement)
    {
        return;
    }

    public void SetKnockbackDirection(Vector3 direction)
    {
        direction.y = 0.0f;
        KnockbackDirection = direction.normalized;
    }

    public virtual bool HasArmor() //For knockback and stunning
    {
        return Armor > 0.0f;
    }

    public abstract void OnDeath();
}
