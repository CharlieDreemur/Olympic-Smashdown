using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuffIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Transform detail_panel;

    public TextMeshProUGUI detail_desc;
    public TextMeshProUGUI detail_name;

    public float fade_duration;

    int UILayer;

    void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");

        // var im = detail_panel.GetComponent<Image>();
        // var tmp_color = im.color;
        // tmp_color.a = 0f;
        // im.color = tmp_color;
        // detail_desc.faceColor = new Color32(detail_desc.faceColor.r, detail_desc.faceColor.g, detail_desc.faceColor.b, 0);
        // detail_name.faceColor = new Color32(detail_desc.faceColor.r, detail_desc.faceColor.g, detail_desc.faceColor.b, 0);
    }

    // Start is called before the first frame update
    public void SetData(UpgradeData.UpgradeDataStruct buff_data_) {
        GetComponent<Image>().sprite = buff_data_.icon;
        detail_desc.text = buff_data_.description;
        detail_name.text = buff_data_.name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        detail_panel.gameObject.SetActive(true);
        detail_panel.SetParent(transform.parent.parent);
        detail_panel.SetAsLastSibling();
        var seq = DOTween.Sequence();
        seq.Append(detail_panel.GetComponent<Image>().DOFade(1f, fade_duration));
        seq.Append(detail_desc.DOFade(1f, fade_duration));
        seq.Append(detail_name.DOFade(1f, fade_duration));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
        var im = detail_panel.GetComponent<Image>();
        var tmp_color = im.color;
        tmp_color.a = 0f;
        im.color = tmp_color;
        // detail_desc.faceColor = new Color32(detail_desc.faceColor.r, detail_desc.faceColor.g, detail_desc.faceColor.b, 0);
        // detail_name.faceColor = new Color32(detail_desc.faceColor.r, detail_desc.faceColor.g, detail_desc.faceColor.b, 0);
        detail_panel.gameObject.SetActive(false);
        detail_panel.SetParent(transform);

    }




    // //Returns 'true' if we touched or hovering on Unity UI element.
    // public bool IsPointerOverUIElement()
    // {
    //     return IsPointerOverUIElement(GetEventSystemRaycastResults());
    // }
 
 
    // //Returns 'true' if we touched or hovering on Unity UI element.
    // private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    // {
    //     for (int index = 0; index < eventSystemRaysastResults.Count; index++)
    //     {
    //         RaycastResult curRaysastResult = eventSystemRaysastResults[index];
    //         if (curRaysastResult.gameObject.layer == UILayer)
    //             return true;
    //     }
    //     return false;
    // }
 
 
    // //Gets all event system raycast results of current mouse or touch position.
    // static List<RaycastResult> GetEventSystemRaycastResults()
    // {
    //     PointerEventData eventData = new PointerEventData(EventSystem.current);
    //     eventData.position = Input.mousePosition;
    //     List<RaycastResult> raysastResults = new List<RaycastResult>();
    //     EventSystem.current.RaycastAll(eventData, raysastResults);
    //     return raysastResults;
    // }
}
