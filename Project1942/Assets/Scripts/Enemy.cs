using UnityEngine;
using System.Collections;

public class Enemy : ShootingSprite
{
   //GameObject playerObject;
    // Use this for initialization

    public float moveForce = 100;

    void Start()
    {
        // first pass implimentation
      //  playerObject = GameObject.FindGameObjectWithTag("Player");
        transform.rigidbody2D.AddForce(-Vector3.up * moveForce );
            
    
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
     //   Movement();

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
        Destroy(gameObject);
    }

}
