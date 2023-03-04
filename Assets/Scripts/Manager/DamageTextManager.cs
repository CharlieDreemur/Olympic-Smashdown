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
    private UnityAction<string> action;
    public static DamageTextData data;
    void OnAwake(){  
        data = Resources.Load("Data/UI/DamageTextData") as DamageTextData;
        action = new UnityAction<string>(Create);
    }

    public void Init(){
        EventManager.AddListener("CreateDamageText", action);
    }
    
    private void OnDisable() {
        EventManager.RemoveListener("CreateDamageText", action);
    }
    
    public static void Create(string jsonValue){
        CreateDamageTextEventArgs args =  JsonUtility.FromJson<CreateDamageTextEventArgs>(jsonValue);
        Create(args);
    }

    public static void Create(CreateDamageTextEventArgs args){
        if(data==null){
            Debug.LogWarning("CreateDamageTextEventArgs is Null");
            //return null;
        }
        //GameObject damageTextGameObject = Instantiate(data.prefab, pos, Quaternion.identity);
        UnityEngine.GameObject damageTextGameObject = Instantiate(data.Prefab, args.pos, Quaternion.identity);
        DamageText damageText = damageTextGameObject.GetComponent<DamageText>();
        damageText.Init(data, args);
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
