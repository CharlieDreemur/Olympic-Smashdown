using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{

    [Header("child objects")]
    public Image icon;
    public new TextMeshProUGUI name;
    public TextMeshProUGUI desc;
    public TextMeshProUGUI fun_fact;

    [Header("upgrade data")]
    public UpgradeData upgrade_data;

    [Header("hooks")]
    public GameObject start_pos;
    public GameObject end_pos;
    public float moving_time;

    public UpgradeData test_data;
    

    // Start is called before the first frame update
    void Start()
    {
        // SetUpgradeData(test_data);
        // Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpgradeData(UpgradeData _upgrade_data) {
        upgrade_data = _upgrade_data;

        icon.sprite = upgrade_data.icon;
        desc.text = upgrade_data.description;
        fun_fact.text = upgrade_data.funFact;
        name.text = upgrade_data.name;
    }

    public void Show() {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOMove(end_pos.transform.position, moving_time));
    }

    public void Hide() {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOMove(start_pos.transform.position, moving_time));
    }
}
