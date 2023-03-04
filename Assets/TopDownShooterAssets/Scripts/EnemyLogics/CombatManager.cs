using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public int kill_count = 0;

    [Header("spawn logic")]
    public List<GameObject> enemy_types;
    public float spawn_wait_time;
    public float spawn_distance;
    public float spawn_tolerance;
    public bool is_spawning;

    [Header("dropping")]
    public float drop_radius;
    public List<GameObject> drops;
    public Dictionary<float, GameObject> random_drops; // TODO: unity can not serialize this, maybe setup another structure


    // private Transform canvas_manager_object;
    private ICanvasManager canvas_manager; 
    private GameObject player;
    private IEnumerator spawn_timer;
    private List<GameObject> current_enemies; 
    private List<GameObject> current_drops; 

    // Start is called before the first frame update
    void Start()
    {
        kill_count = 0;

        var canvas_manager_object = transform.parent.Find("CanvasManager");
        canvas_manager = canvas_manager_object.GetComponent<ICanvasManager>();
        if (canvas_manager == null) Debug.LogError("can not find canvas manager");

        player = GameObject.Find("Player");
        current_enemies = new List<GameObject>();

        SetSpawnActivity(true); // TODO: for debug only
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void HandleEnemyDeath(GameObject enemy) {
        kill_count += 1;
        if (canvas_manager != null) canvas_manager.UpdateKillCount(kill_count);

        if (current_enemies.Contains(enemy)) {
            current_enemies.Remove(current_enemies.Find((x) => x.Equals(enemy)));
            Debug.Log("removed enemy" + enemy.ToString());
        }

        SpawnDrops(enemy);

        Debug.Log("kill count now is " + kill_count); // TODO: get ref to update UI
    }

    // spawn enemy at outside of the circle
    private IEnumerator SpawnEnemy(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            // print("WaitAndPrint " + Time.time);
            // GameObject bullet = Instantiate(bullet_prefab, transform.position, Quaternion.identity);
            // bullet.GetComponent<Bullet>().trigger_tags.Add("Player");
            // bullet.GetComponent<Bullet>().direction = target.transform.position - transform.position;
            // bullet.GetComponent<Bullet>().damage = 10f; // TODO: update damage
            var enemy = enemy_types[Random.Range(0, enemy_types.Count)];
            var enemy_obj = Instantiate(enemy, GetRandomSpawnLocation(), Quaternion.identity);
            enemy_obj.GetComponent<EnemyMaster>().target = player.transform;
            current_enemies.Add(enemy_obj);
        }
    }

    public void SetSpawnActivity(bool is_active) {
        is_spawning = is_active;
        if (is_active) {
            spawn_timer = SpawnEnemy(spawn_wait_time);
            StartCoroutine(spawn_timer);
        } else {
            StopCoroutine(spawn_timer);
        }
    }

    private Vector2 GetRandomSpawnLocation() {
        Vector2 player_location = player.transform.position;
        float angle = Random.Range(0.0f, Mathf.PI*2);
        Vector2 offset = (spawn_distance + Random.Range(-spawn_tolerance, spawn_tolerance)) * new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        return player_location + offset;
    }

    private Vector2 GetRandomLocationInCircle(Vector2 initial_location, float radius) {
        float angle = Random.Range(0.0f, Mathf.PI*2);
        Vector2 offset = Random.Range(0f, radius) * new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        return initial_location + offset;
    }

    private void SpawnDrops(GameObject enemy) {
        Vector2 initial_location = enemy.transform.position;

        // apply DOTween sequence for items in drops and random spread within a range
        var seq = DOTween.Sequence();

        foreach (GameObject drop in drops) {
            var drop_obj = Instantiate(drop, initial_location, Quaternion.identity);
            Vector2 end_location = GetRandomLocationInCircle(initial_location, drop_radius);
            seq.Join(drop_obj.transform.DOMove(end_location, 1f));
        }

        

    }

}
