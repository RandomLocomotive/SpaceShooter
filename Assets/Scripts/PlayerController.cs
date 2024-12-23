using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int hp = 5; // max hp
    public float moveSpeed = 3f;

    public Transform minXValue;
    public Transform maxXValue;

    public GameObject bulletPrefab;
    public Transform gunEndPosition;

    public float fireRate = 0.25f;
    private float timeSinceLastAction = 0f;

    private bool isInvulnerable = false;
    public float invulnerabilityDuration = 0.5f; // czas nietykalnosci po trafieniu
    private MeshRenderer objectRenderer; // komponent MeshRenderer dla 3D

    void Start()
    {
        GameManager.playerController = this;
        GameManager.uiManager.ReduceHealth(0); // Ustaw serca zgodnie z obecnym HP
        objectRenderer = GetComponent<MeshRenderer>(); // pobiersz MeshRenderer
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
            Application.Quit();
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
            Instantiate(bulletPrefab, gunEndPosition.position, Quaternion.identity);
            timeSinceLastAction = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("EnemyBullet") && !isInvulnerable)
        {
            HittedByBullet();
            Destroy(collider.gameObject); // zniszcz pocisk wroga
        }
    }

    public void HittedByBullet()
    {
        if (hp > 0)
        {
            hp -= 1; // Zmniejsz zdrowie gracza
            GameManager.uiManager.ReduceHealth(1); // ukryj jedno serduszko w UI
            Debug.Log("Trafiono! HP: " + hp);

            StartCoroutine(InvulnerabilityCooldown()); // uruchom efekt nietykalnosci
        }
    }

    private IEnumerator InvulnerabilityCooldown()
    {
        isInvulnerable = true;
        StartCoroutine(BlinkEffect());
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false; // moze otrzymac obrazenia?
    }

    private IEnumerator BlinkEffect()
    {
        float elapsedTime = 0f;
        while (elapsedTime < invulnerabilityDuration)
        {
            objectRenderer.enabled = !objectRenderer.enabled;
            elapsedTime += 0.1f; // wait 0.1 sekundy
            yield return new WaitForSeconds(0.1f);
        }
        objectRenderer.enabled = true;
    }
}