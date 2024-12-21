using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int hp = 5;
    public float moveSpeed = 3f;

    public Transform minXValue;
    public Transform maxXValue;

    public GameObject bulletPrefab;
    public Transform gunEndPosition;

    public float fireRate = 0.25f;
    private float timeSinceLastAction = 0f;

    void Start()
    {
        GameManager.playerController = this;
        GameManager.uiManager.UpdateHealthUI(hp);
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

    public void HittedByBullet()
    {
        hp -= 1;
        GameManager.uiManager.UpdateHealthUI(hp);
        Debug.Log("Trafiono! HP: " + hp);
    }
}