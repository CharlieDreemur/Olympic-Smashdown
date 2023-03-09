using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public interface IUpgrade
{
    /// <summary>
    /// Invoke when the player first get this update
    /// </summary>
    void OnUpgrade(){}
    /// <summary>
    /// Invoke when the player start the level
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
    [SerializeField] 
    public UpgradeData upgradeData;
    [SerializeField] [ReadOnly]
    protected Player player;
    protected SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = upgradeData.icon;
    }
    
    public virtual void OnUpgrade()
    {
        Debug.Log("OnUpgrade: " + upgradeData.name);
        player = Player.Instance;
        player.playerStats.Add(upgradeData.statsBonus);
        upgradeData.specialUpgrade?.OnUpgrade();
        EventManager.Invoke("UpgradeEvent", "");
       
    }
    
}
