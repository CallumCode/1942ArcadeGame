using UnityEngine;
using System.Collections;

public class Player : ShootingSprite
{
     public float forceForward = 50;
    public float forceBackward = 100;
    public float forceSideways = 500;

    public Camera mainCamera;

    public float dammageFromHittingEnemy = 30;
    public float dammageFromEnemyBullet = 10;

    float boundarySpeed = 10;
    float rotateFixSpeed = 5;

    float maxFireRate = 10;
    float minFireRate = 1;

    // Use this for initialization
    void Start()
    {
        Init();
    }    

    public override void Init()
    {
        base.Init();

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
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2D.AddForce(-Vector3.right * forceSideways * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidbody2D.AddForce(Vector3.right * forceSideways * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidbody2D.AddForce(Vector3.up * forceForward * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
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
        if (Input.GetKey(KeyCode.Space) && (Time.time > (fireTimer + 1 / fireaRate)))
        {
            fireTimer = Time.time;
            GameObject bullet = ObjectPool.instance.GetObject( bulletPrefab.name);
            if (bullet)
            {
                bullet.transform.position = transform.position;
                bullet.transform.up = Vector3.up;

                bullet.rigidbody2D.velocity = rigidbody2D.velocity;

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

}
