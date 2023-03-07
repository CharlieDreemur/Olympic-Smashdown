using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour, ICanvasManager
{

    [Header("Child Objects")]
    public GameObject health_slider;
    public TextMeshProUGUI health_num;
    // public GameObject upgrade_panel_obj;
    // public GameObject buff_grid_obj;

    // private fields

    private BuffList buff_list;
    private UpgradePanel upgrade_panel;

    void Start()
    {
        buff_list = GameObject.FindObjectOfType<BuffList>();
        upgrade_panel = GameObject.FindObjectOfType<UpgradePanel>();
    }

    public void UpdateHealth(float cur_health, float max_health) {
        health_slider.GetComponent<Image>().fillAmount = cur_health / max_health;
        health_num.text = cur_health.ToString();
    }

    public void ShowUpgradePanel(UpgradeData.UpgradeDataStruct _upgrade_data) {
        upgrade_panel.SetUpgradeData(_upgrade_data);
        upgrade_panel.Show();
    }

    public void SetUpgradePanelData(UpgradeData.UpgradeDataStruct _upgrade_data) {
        upgrade_panel.SetUpgradeData(_upgrade_data);
    }

    public void HideUpgradePanel() {
        upgrade_panel.Hide();
    }

    public void AddBuff(UpgradeData.UpgradeDataStruct buff_data_) {
        buff_list.AddBuff(buff_data_);
    }

    public void RemoveBuff(UpgradeData.UpgradeDataStruct buff_data_) {
        buff_list.RemoveBuff(buff_data_);
    }


}