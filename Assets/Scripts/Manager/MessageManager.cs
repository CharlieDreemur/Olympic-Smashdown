using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MessageManager : Singleton<MessageManager>
{
    private string currentText;
    public TextMeshProUGUI messageText;
    public bool isPrintTime;
    public static string AddMessage(string content){
        return Instance.PrintMessage(content);
    }
    private string PrintMessage(string content){
        string prefix = null;
        if(isPrintTime){
            prefix = "["+System.DateTime.Now.ToString("HH;mm:ss")+"]";
        }
        if(currentText == null){
            currentText = prefix + content;
        }
        currentText = currentText+ "\n"+ prefix + content ;
        messageText.text = currentText;
        return currentText;
    }
}
