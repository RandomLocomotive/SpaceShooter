using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public Rigidbody2D rb;
    public float destroyYValue = 6.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        DestroyAfterLeftScreen();
    }

    void DestroyAfterLeftScreen()
    {
        if (transform.position.y > destroyYValue)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

            GameManager.uiManager.AddScore(100); // Dodaj 100 punktów
            Debug.Log("Wrog Zniszczony!");
        }
    }
}