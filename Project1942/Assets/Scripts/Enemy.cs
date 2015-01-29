using UnityEngine;
using System.Collections;

public class Enemy : Sprite
{
   GameObject playerObject;
    // Use this for initialization



    void Start()
    {
        // first pass implimentation
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
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
}
