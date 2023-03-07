using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ICanvasManager
{
    public void UpdateHealth(float cur_health, float max_health);
    public void ShowUpgradePanel(UpgradeData.UpgradeDataStruct _upgrade_data);
    public void SetUpgradePanelData(UpgradeData.UpgradeDataStruct _upgrade_data);
    public void HideUpgradePanel();
    public void AddBuff(UpgradeData.UpgradeDataStruct buff_data_);
    public void RemoveBuff(UpgradeData.UpgradeDataStruct buff_data_);
}
