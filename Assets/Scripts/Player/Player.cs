using UnityEngine;
using StarterAssets;

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float currentHealth;
    [SerializeField] private float speed = 5f;

    [Header("Bullet")]
    private float shootReset = 0.1f;
    public float reloadMissle = 10f;
    [SerializeField] private Transform spawnBullet;
    [SerializeField] private GameObject secondProjectitlePrefab;
    [SerializeField] private GameObject firstProjectitlePrefab;
    private GameObject currentProjectitlePrefab;
    [SerializeField] private GameObject explosionFX;

    [Header("Sound effect")]
    public AudioClip destroySound;  

    [Header("Event")]
    [SerializeField] private GameEvent onPlayerDie;
    [SerializeField] private GameEvent onEnterScene;

    private AudioSource audioSource;
    private StarterAssetsInputs _input;
    private Rigidbody2D rb;
    private bool isEnterScene;   
    private float timer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        isEnterScene = false;
        timer = reloadMissle;
    }

    private void Update()
    {   
        if (!isEnterScene)
        {
            MoveIntoScene();
        }
        if (isEnterScene)
        {
            PlayerMove();
            PlayerShoot();
        }

        if (currentHealth <= 0)
        {
            audioSource.PlayOneShot(destroySound);
            Instantiate(explosionFX, transform.position, transform.rotation);
            onPlayerDie?.Invoke();
            Destroy(gameObject);
        }
    }
    private void PlayerMove()
    {
        Vector2 inputDirection = new Vector2(_input.move.x, _input.move.y).normalized;

        if (_input.move == Vector2.zero)
        {
            rb.velocity = new Vector2(0, 0);
        }
        if (_input.move != Vector2.zero)
        {
            rb.velocity = inputDirection.normalized * speed;
        }
    }

    private void PlayerShoot()
    {       
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            currentProjectitlePrefab = secondProjectitlePrefab;
        }
        if (timer <= -0.5)
        {
            timer = reloadMissle;
        }
        if (timer > 0)
        {
            currentProjectitlePrefab = firstProjectitlePrefab;
        }

        shootReset -= Time.deltaTime;
        if(shootReset <= 0)
        {
            Instantiate(currentProjectitlePrefab, spawnBullet.transform.position, spawnBullet.transform.rotation);
            shootReset = 0.5f;
        }
    }

    private void MoveIntoScene()
    {
        if (!isEnterScene)
        {
            if (transform.position.y < -4)
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed / 2);
            }
            if (transform.position.y >= -4)
            {
                transform.Translate(Vector3.zero);
                isEnterScene = true;
            }
        }
        if (isEnterScene)
        {
            onEnterScene?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
