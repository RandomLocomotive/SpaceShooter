using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 0.5f;
    public float fireRate = 2f;
    private float timeSinceLastAction = 0f;

    public GameObject bulletPrefab;
    public Transform enemyGunEnd;

    public GameObject expolisionEffectPrefab;

    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.y > -2)
            Shoot();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(expolisionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "SpaceShooterPlayerShip")
        {
            GameManager.playerController.HittedByBullet();
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        timeSinceLastAction += Time.deltaTime;

        if (timeSinceLastAction >= fireRate)
        {
            Instantiate(bulletPrefab, enemyGunEnd.position, Quaternion.identity);
            timeSinceLastAction = 0;
        }
    }
}