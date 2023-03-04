using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarSetup : MonoBehaviour
{
    public Transform HealthFill; //HealthFill
    public Transform ShieldFill;
    public Transform MPFill;

   

    [SerializeField]
    private float HPscale=1;

    [SerializeField]
    private float MPscale=1;

    [SerializeField]
    private float Shieldscale=0;

    [SerializeField]
    private bool IsInit;

    [SerializeField]
    private float Health_Width = 0f;

    [SerializeField]
    private Vector3 ShieldPos;

    [SerializeField]
    private float HealthFill_Pos_x;



     
    /*
    /// <summary>
    /// Update只会在受到伤害时触发
    /// </summary>
    public void Update(){
        int MaxValue = fixedData.maxHP.FinalAttribute + fixedData.ShieldValue;
        if(IsInit){
            if(fixedData.maxHP.FinalAttribute != 0){
                HPscale = (float)charAttr.attributeData.NowHP/MaxValue;
                Shieldscale = (float)fixedData.ShieldValue/MaxValue;
            }   
            else{
                HPscale = 0;
                Shieldscale = 0;
            }

            if(fixedData.maxMP.FinalAttribute != 0){
                MPscale = (float)charAttr.attributeData.NowMP/fixedData.maxMP.FinalAttribute;
            }
            else{
                MPscale = 0;
            }
            ShieldPos = new Vector3(HPscale * Health_Width + HealthFill_Pos_x, 0, 0);
            ShieldFill.localPosition = ShieldPos;
            HealthFill.localScale = new Vector3(HPscale, 1, 1);
            ShieldFill.localScale = new Vector3(Shieldscale, 1, 1);
            MPFill.localScale = new Vector3(MPscale, MPFill.localScale.y, MPFill.localScale.z);
        }
        
    }
 
  

    public void Init(Enemy character){
        Health_Width = HealthFill.GetComponent<Renderer>().bounds.size.x;
        HealthFill_Pos_x = HealthFill.localPosition.x;
        IsInit = true;
        
    }



    */
    
}
