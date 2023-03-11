using System;
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
    public TextMeshProUGUI funFact;

    [Header("upgrade data")]
    public UpgradeData upgradeData;

    [Header("hooks")]
    public GameObject startPos;
    public GameObject endPos;
    public float movingTime;

    public UpgradeData testData;

    void Awake()
    {
        EventManager.AddListener("ShowUpgradeTextEvent", SetUpgradeData);
        EventManager.AddListener("ShowUpgradeTextEvent", Show);
        EventManager.AddListener("PickUpgradeEvent", Hide);
    }
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
    public void SetUpgradeData(string jsonValue)
    {
        UpgradeArgs args = JsonUtility.FromJson<UpgradeArgs>(jsonValue);
        Debug.Log(args.upgradeData);
        if (args.upgradeData == null) return;
        SetUpgradeData(args.upgradeData);
    }
    public void SetUpgradeData(UpgradeData _upgrade_data)
    {
        upgradeData = _upgrade_data;
        icon.sprite = upgradeData.icon;
        desc.text = upgradeData.description;
        funFact.text = upgradeData.funFact;
        name.text = upgradeData.name;
    }

    public void Show(string jsonValue = "")
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOMove(endPos.transform.position, movingTime));
    }

    public void Hide(string jsonValue = "")
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOMove(startPos.transform.position, movingTime));
    }
}

public class UpgradeArgs : EventArgs
{
    public UpgradeData upgradeData;
    public UpgradeArgs(UpgradeData _upgradeData)
    {
        upgradeData = _upgradeData;
    }
}