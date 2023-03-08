using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ICanvasManager
{
    public void UpdateHealth(int cur_health, int max_health);
    public void ShowUpgradePanel(UpgradeData _upgrade_data);
    public void SetUpgradePanelData(UpgradeData _upgrade_data);
    public void HideUpgradePanel();
    public void AddBuff(UpgradeData buff_data_);
    public void RemoveBuff(UpgradeData buff_data_);
}
