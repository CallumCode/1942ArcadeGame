using UnityEngine;
using System.Collections;

public class Enemy : ShootingSprite
{
     // Use this for initialization

    public float moveForce = 250;
    public float startVel = 5;
    public float rotateFixSpeed = 1;

    public float damageFromHittingPlayer = 100;
    public float damageFromPlayerBullet = 50;

    public float scoreWorth = 50;
        
    public override void Init()
    {
        base.Init();
        transform.rigidbody2D.velocity = - Vector3.up * startVel;
        GameProgressHandler.instance.enemiesActive++;
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
            TakeDamage(damageFromPlayerBullet);
            ObjectPool.instance.ReturnObject(coll.gameObject);
        }


        if (coll.collider.CompareTag("Player"))
        {
            TakeDamage(damageFromHittingPlayer);
        }

    }


    void Shooting()
    {
        if (Time.time > (fireTimer + 1 / fireaRate))
        {
            fireTimer = Time.time;

            GameObject bullet = ObjectPool.instance.GetObject(bulletPrefab.name);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            bullet.rigidbody2D.velocity = rigidbody2D.velocity;

            bullet.rigidbody2D.AddForce(transform.up * bulletForce);
          
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
        GameProgressHandler.instance.enemiesActive--;

        ReturnSelf();
    }


    protected override void Death()
    {
        GameProgressHandler.instance.AddScore(scoreWorth);
 
        base.Death();
       
    }
}
