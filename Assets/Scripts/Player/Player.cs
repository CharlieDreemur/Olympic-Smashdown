using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    public static Player Instance { get; private set; }

    public PlayerData data;

    // weird dependency but it is probably fine
    public TransitionBlackout curtain;
    public SpriteRenderer spriteRenderer;

    // some external manager will need to set the health on game start
    // and make it remember its health across levels
    public PlayerStats playerStats;
    [SerializeField]
    public int enemyKilled;
    [FoldoutGroup("Events")]
    public UnityEvent onStart;
    [FoldoutGroup("Events")]
    public UnityEvent onUpdate;
    [FoldoutGroup("Events")]
    public UnityEvent onHurt;
    [FoldoutGroup("Events")]
    public UnityEvent<int, int> onHealthChange;
    [FoldoutGroup("Events")]
    public UnityEvent onDash;
    [FoldoutGroup("Events")]
    public UnityEvent onReflect;

    [SerializeField] 
    private string _firstSceneName = "MainScene";

    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == _firstSceneName && Instance != null) { // Make sure the player is reset on the first scene
            Destroy(Instance.gameObject); // destroy the old instance
            Instance = this; // set the new instance
            DontDestroyOnLoad(this); // don't destroy this instance
        } else {
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
    }
    private void Start()
    {
        playerStats.CurrentHealth = playerStats.MaxHealth;
        OnUpgrade();
        onStart.Invoke();
    }

    private void OnUpgrade(string jsonValue=""){
        transform.localScale = new Vector3(transform.localScale.x * playerStats.playerSizeMultiplier, transform.localScale.y * playerStats.playerSizeMultiplier, 1);
    }
    private void Update()
    {
        onUpdate.Invoke();
    }

    public void Hurt(int damage)
    {
        onHurt.Invoke();
        playerStats.CurrentHealth -= damage;
        if (playerStats.CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(curtain.LoadAsyncSceneWithFadeOut("GameOverScene"));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent<Projectile>(out Projectile proj))
        {
            if (proj.args.DamageInfo.ownerType == ProjectileOwnerType.enemy)
            {
                Destroy(col.gameObject);
                Hurt(1);             
            }
        }
    }
}
