using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamageText : MonoBehaviour
{
    public DamageTextData data;
    private TextMeshPro textMesh_; 
    private Color color_;
    private CreateDamageTextEventArgs args_;
    private DamageType damageType_;
    private bool isCrit_;
    private float disappearTimer_;
    private Vector3 moveVector_ = new Vector3(0.35f,0.5f);

    private static int sortingOrder; //渲染层级，确保后生产的text会在上层
    private void Awake(){
        textMesh_ = transform.GetComponent<TextMeshPro>();
       
    }
    
    public void OnSpawn(){
        textMesh_.SetText(args_.damageAmount.ToString());
        switch(damageType_){
            case DamageType.Normal:
                textMesh_.fontSize = data.noramlFontSize;
                textMesh_.color = data.normalColor;
                break;
            case DamageType.Critical:
                textMesh_.fontSize = data.critFontSize;
                textMesh_.color = data.critColor;
                break;
            case DamageType.Heal:
                textMesh_.fontSize = data.healFontSize;
                textMesh_.color = data.healColor;
                break;
            case DamageType.Ability:
                textMesh_.fontSize = data.abilityFontSize;
                textMesh_.color = data.abilityColor;
                break;
            default:
                Debug.LogWarning("DamageText.OnObjectSpawn() Unknown DamageType");
                break;
        }
        color_ = textMesh_.color;
        disappearTimer_ = data.disappearTime;
        sortingOrder ++;
        textMesh_.sortingOrder = sortingOrder;
        moveVector_ *= data.moveSpeed;
        transform.localScale = Vector3.one;
    }

    public void Init(DamageTextData data, CreateDamageTextEventArgs args){
        if(data==null){
            Debug.LogWarning("DamageTextData is Null");
            return;
        }
        this.data = data;
        disappearTimer_ = data.disappearTime;
        args_ = args;
        OnSpawn();
    }
  
    public void Update(){
        transform.position += moveVector_*Time.deltaTime;
        moveVector_ -= moveVector_ * 8f * Time.deltaTime;
        if(disappearTimer_ > data.disappearTime * 0.5f){
            //First hald of the damageText lifetime
            float increaseScaleAmout = 1f;
            transform.localScale += Vector3.one * increaseScaleAmout * Time.deltaTime;
        }
        else{
            //Second half of the damageText lifetime
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;

        }

        disappearTimer_ -= Time.deltaTime;
        if(disappearTimer_ < 0){
            //Start disappearing
            color_.a -= data.disappearSpeed * Time.deltaTime;
            textMesh_.color = color_;
            if(color_.a < 0){
                Destroy(gameObject);   
            }
        }
    }

}
