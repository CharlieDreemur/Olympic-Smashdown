using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface IUpgrade
{
    /// <summary>
    /// Invoke when the player first get this update
    /// </summary>
    void OnUpgrade(){}
    /// <summary>
    /// Invoke when the player start the level
    /// </summary>
    void OnStart(){}
    /// <summary>
    /// Invoke when the player update
    /// </summary>
    void OnUpdate(){}
    /// <summary>
    /// Invoke when the player dash
    /// </summary>
    void OnDash(){}
    /// <summary>
    /// Invoke when the player reflect a projectile
    /// </summary>
    void OnReflect(){}
    /// <summary>
    /// Invoke when the player get hurt
    /// </summary>
    void OnHurt(){}
    /// <summary>
    /// Invoke when the player kill an enemy
    /// </summary>
    void OnKillEnemy(){}
    /// <summary>
    /// Invoke when the player is killed
    /// </summary>
    void OnDeath(){}
}

public class Upgrade : MonoBehaviour, IUpgrade
{
    public UpgradeData upgradeData;
    public Player player;

    public virtual void OnUpgrade()
    {
        player.onUpdate.AddListener(OnUpdate);
        player.onStart.AddListener(OnStart);
        player.onDash.AddListener(OnDash);
        player.onReflect.AddListener(OnReflect);
        player.onHurt.AddListener(OnHurt);
       
    }
    public virtual void OnStart()
    {
    }
    public virtual void OnUpdate()
    {
        
    }
    public virtual void OnDash()
    {
        
    }
    public virtual void OnReflect()
    {
        
    }
    public virtual void OnHurt()
    {
        
    }
    public virtual void OnKillEnemy()
    {
        
    }
    public virtual void OnDeath()
    {
        
    }
    
}
