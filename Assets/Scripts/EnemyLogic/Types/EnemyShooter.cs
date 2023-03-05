using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyShooter : EnemyMaster
{

    [Header("stats")]
    public float shoot_range;
    public float shoot_interval;

    [Header("game objects")]
    private ShootRangeDetector shoot_detector;
    public GameObject bullet_prefab;
    


    public enum State {walking, shooting};

    private State state;
    private IEnumerator shoot_timer;

    // Start is called before the first frame update

    public override void Start()
    {
        base.Start();
        shoot_detector = gameObject.GetComponentInChildren<ShootRangeDetector>();
        shoot_detector.target = target;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        FollowTarget(target);

        
    }

    void FixedUpdate()
    {
        

        
    }

    public void ChangeState(State s) {
        state = s;
        // Debug.Log("state changed to:" + s.ToString());
        if (state == State.shooting) {
            shoot_timer = Shoot(shoot_interval);
            StartCoroutine(shoot_timer);
        } else if (state == State.walking) {
            StopCoroutine(shoot_timer);
        }
    }

    public void ChangeShootRange(float range) {
        shoot_detector.ChangeColliderRadius(range);
    }

    private IEnumerator Shoot(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            // print("WaitAndPrint " + Time.time);
            GameObject bullet = Instantiate(bullet_prefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().trigger_tags.Add("Player");
            bullet.GetComponent<Bullet>().direction = target.transform.position - transform.position;
            bullet.GetComponent<Bullet>().damage = 10f; // TODO: update damage
        }
    }

}

