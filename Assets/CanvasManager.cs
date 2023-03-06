using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{

    [Header("Child Objects")]
    public GameObject health_slider;
    public TextMeshProUGUI health_num;
    public GameObject upgrade_panel;
    public GameObject buff_grid;

    // private fields



    public void UpdateHealth(float cur_health, float max_health) {
        health_slider.GetComponent<Image>().fillAmount = cur_health / max_health;
        health_num.text = cur_health.ToString();
    }
    {
        
    }
}
