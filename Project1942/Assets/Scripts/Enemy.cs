﻿using UnityEngine;
using System.Collections;

public class Enemy : ShootingSprite
{
     // Use this for initialization

    public float moveForce = 250;
    public float startVel = 5;
    public float rotateFixSpeed = 1;
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
        Movement();
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
          
            soundShoot.pitch = Random.Range(.9f, 1.1f);
            soundShoot.Play();

        }
    }
    void Movement()
    {
        transform.up = Vector3.RotateTowards(transform.up, -Vector3.up, rotateFixSpeed * Time.deltaTime, 0);

        transform.rigidbody2D.AddForce(transform.up * moveForce * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        DestroySelf();
    }

}
