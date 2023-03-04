using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeSystem : MonoBehaviour
{
    [Header("UI elements")]
    public Text textLabel;
    public Image avatar;

    [Header("text file")]
    public TextAsset textFile;

    [Header("paramters")]
    public float waitSeconds;

    public bool textFinished;

    // text list
    List<string> dialouge = new List<string>();
    public int index;

    private void Awake()
    {
        GetTextFromFile(textFile);
    }

    private void OnEnable()
    {
        //textLabel.text = dialouge[index];
        //index++;
        StartCoroutine(SetText());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && textFinished)
        {
            //textLabel.text = dialouge[index];
            //index++;
            StartCoroutine(SetText());
        }
    }

    void GetTextFromFile(TextAsset textAsset)
    {
        index = 0;
        dialouge.Clear();

        var textLines = textAsset.text.Split('\n');
        foreach (var textEntry in textLines)
        {
            dialouge.Add(textEntry);
        }
    }

    IEnumerator SetText()
    {
        textFinished = false;
        textLabel.text = "";
        for (int i = 0; i < dialouge[index].Length; i++)
        {
            textLabel.text += dialouge[index][i];
            yield return new WaitForSeconds(waitSeconds);
        }
        index++;
        textFinished = true;
    }
}
