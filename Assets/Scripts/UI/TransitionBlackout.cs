using System;
using System.Collections;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class TransitionBlackout : MonoBehaviour
{
    private Material _mat;
    public Transform transitionCenterFollow;
    public Vector2 transitionCenter = new Vector2(1920f/2, 1080f/2);
    public float fadeoutStartRadius = 2200f;
    private float _radius;
    private Camera _cam;

    private void Awake()
    {
        _mat = GetComponent<Image>().material;

    }

    private void Update()
    {
        _mat.SetFloat("_Radius", _radius);
        
        if (transitionCenterFollow == null)
        {
            _mat.SetFloat("_CenterX", transitionCenter.x);
            _mat.SetFloat("_CenterY", transitionCenter.y);
        }
        else
        {
            var pos3 = _cam.WorldToScreenPoint(transitionCenterFollow.position);
            var posStandardized = new Vector2(pos3.x / _cam.pixelWidth * 1920f, pos3.y / _cam.pixelHeight * 1080f);
            _mat.SetFloat("_CenterX", posStandardized.x);
            _mat.SetFloat("_CenterY", posStandardized.y);
        }
    }
    


    public IEnumerator FadeOutCoro()
    {
        // should be plenty
        _radius = fadeoutStartRadius;
        while (_radius > 0)
        {
            _radius -= fadeoutStartRadius / 60f;
            yield return new WaitForSeconds(1 / 60f);
        }
    }
    
    public IEnumerator FadeInCoro()
    {
        // should be plenty
        _radius = 0;
        while (_radius < fadeoutStartRadius)
        {
            _radius += fadeoutStartRadius / 60f;
            yield return new WaitForSeconds(1 / 60f);
        }
    }

    private void Start()
    {
        _cam = Camera.main;
        StartCoroutine(FadeInCoro());
    }
    
    public IEnumerator LoadAsyncSceneWithFadeOut(string targetScene)
    {
        yield return FadeOutCoro();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
