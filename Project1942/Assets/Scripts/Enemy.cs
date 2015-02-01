using UnityEngine;
using System.Collections;

public class Enemy : ShootingSprite
{
     // Use this for initialization

    public float moveForce = 250;
    public float startVel = 5;
    void Start()
    {
        // first pass implimentation
 
        Init();
    }

    public override void Init()
    {
        base.Init();
        transform.rigidbody2D.velocity = - Vector3.up * startVel;
    
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
     //   Movement();
        UpdateCanvas();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("PlayerBullet"))
        {
            TakeDamage(50);
            Destroy(coll.gameObject); 
        }
    }


    void Shooting()
    {
        if (Time.time > (fireTimer + 1 / fireaRate))
        {
            fireTimer = Time.time;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, transform.rotation) as GameObject;

            bullet.rigidbody2D.velocity = rigidbody2D.velocity;

            bullet.rigidbody2D.AddForce(- Vector3.up * bulletForce);

        }
    }
    void Movement()
    {
         transform.rigidbody2D.AddForce(-  Vector3.up * moveForce * Time.time);
    }

    void OnBecameInvisible()
    {
        DestroySelf();
    }

}
