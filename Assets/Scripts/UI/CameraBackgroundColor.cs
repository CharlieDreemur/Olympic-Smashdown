using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackgroundColor : MonoBehaviour
{
    [SerializeField] Color cameraBackgroundColor;

    void Awake()
    {
        Camera.main.backgroundColor = cameraBackgroundColor;
    }
}
