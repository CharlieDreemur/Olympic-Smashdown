using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
public enum DamageType{
    Normal,
    Critical,
    Heal,
    Ability,
    None
}

public class DamageTextManager : Singleton<DamageTextManager>
{

    public DamageTextData data;
    [Required]
    public Transform canvas;
    void Awake(){  
        EventManager.AddListener("CreateDamageText", new UnityAction<string>(Create));
    }

    
    public static void Create(string jsonValue){
        CreateDamageTextEventArgs args =  JsonUtility.FromJson<CreateDamageTextEventArgs>(jsonValue);
        Create(args);
    }

    public static void Create(CreateDamageTextEventArgs args){
        if(Instance.data==null){
            Debug.LogWarning("CreateDamageTextEventArgs is Null");
            //return null;
        }
        GameObject damageTextGameObject = Instantiate(Instance.data.Prefab, args.pos, Quaternion.identity);
        damageTextGameObject.transform.SetParent(Instance.canvas);
        DamageText damageText = damageTextGameObject.GetComponent<DamageText>();
        damageText.Init(Instance.data, args);
        //return damageText;
    }
    
}

public class CreateDamageTextEventArgs : EventArgs{
    public CreateDamageTextEventArgs(Vector3 pos, int damageAmount, DamageType damageType)
    {
        this.pos = pos;
        this.damageAmount = damageAmount;
        this.damageType = damageType;
    }
    public Vector3 pos;
    public int damageAmount;
    public DamageType damageType;
}
