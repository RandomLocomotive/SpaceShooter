using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public int hp = 5;
    public float moveSpeed = 3f;

    public Transform minXValue;
    public Transform maxXValue;

    public GameObject bulletPrefab;
    public Transform gunEndPosition;

    public float fireRate = 0.25f;
    private float timeSinceLastAction = 0f;

    private bool isInvulnerable = false;
    public float invulnerabilityDuration = 2.5f;
    private MeshRenderer objectRenderer;

    void Start()
    {
        if (GameManager.uiManager == null)
        {
            Debug.Log("GameManager.uiManager nie został zainicjalizowany");
            return;
        }

        GameManager.playerController = this;
        GameManager.uiManager.ReduceHealth(0);
        objectRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        PlayerMovement();

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }

        if (hp <= 0)
        {
            Debug.Log("Koniec gry");
            SceneManager.LoadScene(2);
        }
    }

    void PlayerMovement()
    {
        float horizontalInputValue = Input.GetAxis("Horizontal");
        Vector2 movementVector = new Vector2(-horizontalInputValue, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movementVector);

        if (transform.position.x > maxXValue.position.x)
        {
            transform.position = new Vector2(maxXValue.position.x, transform.position.y);
        }

        if (transform.position.x < minXValue.position.x)
        {
            transform.position = new Vector2(minXValue.position.x, transform.position.y);
        }
    }

    void Shoot()
    {
        timeSinceLastAction += Time.deltaTime;

        if (timeSinceLastAction >= fireRate)
        {
            audioManager.PlaySFX(audioManager.playershoot, 0.15f);
            Instantiate(bulletPrefab, gunEndPosition.position, Quaternion.identity);
            timeSinceLastAction = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("EnemyBullet") && !isInvulnerable)
        {
            HittedByBullet();
            Destroy(collider.gameObject);
        }
    }

    public void HittedByBullet()
    {
        if (hp > 0)
        {
            hp -= 1;
            GameManager.uiManager.ReduceHealth(1);
            Debug.Log("Trafiono! HP: " + hp);
            audioManager.PlaySFX(audioManager.playerhit, 0.22f);
            StartCoroutine(InvulnerabilityCooldown());
        }
    }

    private IEnumerator InvulnerabilityCooldown()
    {
        isInvulnerable = true;
        StartCoroutine(BlinkEffect());
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    private IEnumerator BlinkEffect()
    {
        float elapsedTime = 0f;
        while (elapsedTime < invulnerabilityDuration)
        {
            objectRenderer.enabled = !objectRenderer.enabled;
            elapsedTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        objectRenderer.enabled = true;
    }
}