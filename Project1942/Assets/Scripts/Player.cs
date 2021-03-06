﻿using UnityEngine;
using System.Collections;

public class Player : ShootingSprite
{
     public float forceForward = 50;
    public float forceBackward = 100;
    public float forceSideways = 500;

    public Camera mainCamera;

    public float dammageFromHittingEnemy = 30;
    public float dammageFromEnemyBullet = 10;

    public float boundarySpeed = 15;
    public float rotateFixSpeed = 5;

    float maxFireRate = 10;
    float minFireRate = 1;

    AudioSource soundHurt;
    AudioSource soundPickUp;

    public float bulletVelocityScaler = 0.75f;
    // Use this for initialization
    void Start()
    {
        Init();
    }    

    public override void Init()
    {
        base.Init();

        AudioSource[] sounds = GetComponents<AudioSource>();

        soundShoot = sounds[0];
        soundHurt = sounds[1];
        soundPickUp = sounds[2];

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
        UpdateCanvas();
    }

    void Movement()
    {
        if( Input.GetAxis("Horizontal") < 0 )
        {
            rigidbody2D.AddForce(-Vector3.right * forceSideways * Time.deltaTime);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            rigidbody2D.AddForce(Vector3.right * forceSideways * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            rigidbody2D.AddForce(Vector3.up * forceForward * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            rigidbody2D.AddForce(-Vector3.up * forceBackward * Time.deltaTime);
        }

        transform.up = Vector3.RotateTowards(transform.up, Vector3.up, rotateFixSpeed *Time.deltaTime, 0);
    }

    void OnBecameInvisible()
    {
        if (mainCamera)
        {
            Vector3 dir = (mainCamera.transform.position - transform.position).normalized;
            dir.z = 0;
            rigidbody2D.velocity = dir * boundarySpeed;
        }
    }


    void Shooting()
    {
        if (Input.GetButton("Fire1")  && (Time.time > (fireTimer + 1 / fireaRate)))
        {
            fireTimer = Time.time;
            GameObject bullet = ObjectPool.instance.GetObject( bulletPrefab.name);
            if (bullet)
            {
                bullet.transform.position = transform.position;
                bullet.transform.up = Vector3.up;

                bullet.rigidbody2D.velocity = rigidbody2D.velocity * bulletVelocityScaler;  

                bullet.rigidbody2D.AddForce(Vector3.up * bulletForce);

                soundShoot.pitch = Random.Range(.9f, 1.1f);
                soundShoot.Play();
            }
        }
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("EnemyBullet"))
        {
            TakeDamage(dammageFromEnemyBullet);
            ObjectPool.instance.ReturnObject(coll.gameObject);        
        }

        if (coll.collider.CompareTag("Enemy"))
        {
            TakeDamage(dammageFromHittingEnemy);             
        }
    }
    

      public void ChangeFireRate(float change)
    {
        fireaRate += change;

        fireaRate = Mathf.Clamp(fireaRate, minFireRate, maxFireRate);
    }

     protected override void Death()
      {
          GameProgressHandler.instance.LoseGame();
          base.Death();
      }

     public override void TakeDamage(float amount)
     {
         soundHurt.Play();
         base.TakeDamage(amount);

     }

    public void PlayPickUp()
     {
         soundPickUp.Play();
     }


}
