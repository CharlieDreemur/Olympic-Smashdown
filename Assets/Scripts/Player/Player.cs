using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : Entity
{

    private bool _canTakeDamage = true;
    public static Player Instance { get; private set; }

    public PlayerData data;
    public int score;
    public List<GameObject> rackets = new List<GameObject>();
    [SerializeField]
    private int _racketCounter = 0;
    // weird dependency but it is probably fine
    public TransitionBlackout curtain;
    public bool isKilled = false;
    public SpriteRenderer spriteRenderer;
    public List<UpgradeData> upgrades = new List<UpgradeData>();

    // some external manager will need to set the health on game start
    // and make it remember its health across levels
    public PlayerStats playerStats;
    [SerializeField]
    [FoldoutGroup("Special Stats")]
    public int enemyKilled;
    [FoldoutGroup("Events")]
    public SpecialEvent onStart = new SpecialEvent();
    [FoldoutGroup("Events")]
    public SpecialEvent onUpdate = new SpecialEvent();
    [FoldoutGroup("Events")]
    public SpecialEvent onHurt = new SpecialEvent();
    [FoldoutGroup("Events")]
    public UnityEvent<int, int> onHealthChange;
    [FoldoutGroup("Events")]
    public SpecialEvent onHeal = new SpecialEvent();
    [FoldoutGroup("Events")]
    public SpecialEvent onReflect = new SpecialEvent();
    [FoldoutGroup("Events")]
    public SpecialEvent onDeath = new SpecialEvent();
    [FoldoutGroup("Events")]
    public SpecialEvent onKillEnemy = new SpecialEvent();
    [SerializeField]
    private string _firstSceneName = "MainScene";

    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == _firstSceneName && Instance != null)
        { // Make sure the player is reset on the first scene
            Destroy(Instance.gameObject); // destroy the old instance
            Instance = this; // set the new instance
            DontDestroyOnLoad(this); // don't destroy this instance
        }
        else
        {
            // If there is an instance, and it's not me, delete myself.
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        // for single scene setup, let's just set it to default value on start
        // note that this will need to be changed as we add more levels
        // probably some don't destroy on load setup
        playerStats.Init(data);
        spriteRenderer = GetComponent<SpriteRenderer>();
        onHealthChange.Invoke(playerStats.CurrentHealth, playerStats.MaxHealth);
        EventManager.AddListener("UpgradeEvent", new UnityAction<string>(OnUpgrade));
        EventManager.AddListener("PickUpgradeEvent", new UnityAction<string>(AddUpgrade));
    }
    private void Start()
    {
        playerStats.CurrentHealth = playerStats.MaxHealth;
        if (onStart.count > 0)
        {
            onStart.Invoke();
        }
        OnUpgrade();
    }

    private void OnUpgrade(string jsonValue = "")
    {
        transform.localScale = new Vector3(transform.localScale.x * playerStats.playerSizeMultiplier, transform.localScale.y * playerStats.playerSizeMultiplier, 1);
    }
    private void Update()
    {
        if (onUpdate.count > 0)
        {
            onUpdate.Invoke();
        }
    }
    private void Heal(int healAmount)
    {
        playerStats.CurrentHealth += healAmount;
    }
    public void AddRacket()
    {
        if (_racketCounter >= rackets.Count)
        {
            return;
        }
        rackets[_racketCounter].SetActive(true);
        _racketCounter++;
    }

    private void AddUpgrade(string jsonValue)
    {
        UpgradeArgs args = JsonUtility.FromJson<UpgradeArgs>(jsonValue);
        upgrades.Add(args.upgradeData);
    }
    private void AddUpgrade(UpgradeData upgradeData)
    {
        upgrades.Add(upgradeData);
    }
    public void Hurt(int damage)
    {
        if (isKilled) return;
        SFXManager.PlayMusic("playerHurt");
        playerStats.CurrentHealth -= damage;
        if (playerStats.CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void OnKillEnemy()
    {
        if (onKillEnemy.count > 0)
        {
            enemyKilled++;
            score++;
            onKillEnemy.Invoke();
        }

    }
    public void Die()
    {
        if (onDeath.count > 0)
        {
            onDeath.Invoke();
        }

        StartCoroutine(curtain.LoadAsyncSceneWithFadeOut("GameOverScene"));
        isKilled = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent<Projectile>(out Projectile proj))
        {
            if (proj.args.DamageInfo.ownerType == ProjectileOwnerType.enemy)
            {
                Destroy(col.gameObject);
                if (_canTakeDamage)
                {
                    StartCoroutine(InvincibilityCoro(0.6f));
                    Hurt(1);
                }
            }
        }
    }

    IEnumerator InvincibilityCoro(float time)
    {
        _canTakeDamage = false;
        spriteRenderer.DOFade(0.4f, 0.1f)
                .SetLoops(3, LoopType.Yoyo)
                .OnComplete(() =>
                {
                    spriteRenderer.color = Color.white;
                });
        yield return new WaitForSeconds(time);
        _canTakeDamage = true;
    }

}

public class SpecialEvent : UnityEvent
{

    public int count { get; private set; }

    public new void AddListener(UnityAction call)
    {
        base.AddListener(call);
        count++;
    }

    public new void RemoveListener(UnityAction call)
    {
        base.RemoveListener(call);
        count--;
    }

}