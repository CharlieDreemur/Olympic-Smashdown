using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class SpecialUpgrade : ScriptableObject, IUpgrade
{

    public void OnUpgrade()
    {
        Player player = Player.Instance;
        player.onUpdate.AddListener(OnUpdate);
        player.onReflect.AddListener(OnReflect);
        player.onHurt.AddListener(OnHurt);
        player.onKillEnemy.AddListener(OnKillEnemy);
        player.onDeath.AddListener(OnDeath);
        player.onHeal.AddListener(OnHeal);
        Upgrade();
    }
    public virtual void Upgrade()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnHealthChange()
    {

    }
    public virtual void OnReflect()
    {

    }
    public virtual void OnHurt()
    {

    }
    public virtual void OnHeal()
    {

    }
    public virtual void OnKillEnemy()
    {

    }
    public virtual void OnDeath()
    {

    }


}
