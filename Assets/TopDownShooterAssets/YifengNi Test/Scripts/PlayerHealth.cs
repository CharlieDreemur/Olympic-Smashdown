using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IPlayer
{
    public float health;
    public GameObject test;
    public int Blinks;
    public float time;
    private Renderer myRender;

    // Start is called before the first frame update
    void Start()
    {
    //    Debug.Log("hello");
       myRender = GetComponent<Renderer>();   
       health = 40;
        
        HealthBar.HealthCurrent = health;
        HealthBar.HealthMax = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(float damage) 
    {
        health -= damage;
        HealthBar.HealthCurrent = health;

        if (health <= 0) 
        {
            Instantiate(test,gameObject.transform.position,gameObject.transform.rotation);
            gameObject.SetActive(false);
        }
        BlinkPlayer(Blinks, time);
    }


    
    // private void OnCollisionEnter2D(Collision2D collision)
    // {

        
    //     if (collision.gameObject.CompareTag("Enemy") == true)
    //     {
    //         DamagePlayer(5);
    //     }
    // }

    void BlinkPlayer(int numBlinks, float seconds) 
    {
        if(gameObject.activeSelf == true){
        StartCoroutine(DoBlinks(numBlinks,seconds));
        }
    }

    IEnumerator DoBlinks(int numBlinks, float seconds) 
    {
        for (int i = 0; i < numBlinks * 2; i++) 
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
}
