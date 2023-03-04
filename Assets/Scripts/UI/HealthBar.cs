using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class HealthBar : MonoBehaviour
{
    public HealthBarData data;
    public Enemy enemy;
    [SerializeField]
    private UnityEngine.GameObject healthBar;
    [SerializeField]
    private HealthBarSetup setup;
    private void Start() {
        enemy = gameObject.GetComponent<Enemy>();
        
        if(enemy==null){
            return;
        }
        healthBar = data.Create();
        healthBar.transform.SetParent(enemy.transform, false);
        setup = healthBar.GetComponent<HealthBarSetup>();
        //setup.Init(enemy);
    
    }

    public void Update(){
        if(enemy==null){
            return;
        }
        //setup.Update();
    }
}
