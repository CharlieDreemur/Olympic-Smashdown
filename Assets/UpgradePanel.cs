using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{

    [Header("child objects")]
    public Image icon;
    public TextMeshProUGUI desc;
    public TextMeshProUGUI fun_fact;

    [Header("upgrade data")]
    public UpgradeData upgrade_data;

    [Header("hooks")]
    public GameObject start_pos;
    public GameObject end_pos;
    public float moving_time;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpgradeData(UpgradeData _upgrade_data) {
        upgrade_data = _upgrade_data;
    }
}
