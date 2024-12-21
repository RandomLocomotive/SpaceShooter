using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float speed = 2.5f;
    public Transform playerTransform;
    public Rigidbody2D rb;
    private Vector3 direction;
    public GameObject bulletExplosionEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("SpaceShooterPlayerShip").GetComponent<Transform>();
        direction = (playerTransform.position - transform.position).normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.playerController.HittedByBullet();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}