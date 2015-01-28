using UnityEngine;
using System.Collections;

public class Player : Sprite {

    float forceForward = 50;
    float forceBackward = 100;
    float forceSideways = 500;

    public   Camera mainCamera;
    public GameObject bulletPrefab;

    float bulletForce = 100;

    AudioSource soundShoot;

    float boundarySpeed = 10;    
	// Use this for initialization
	void Start () 
    {
        Init();
	}

    public override void Init()
    {
        base.Init();

        soundShoot = GetComponent<AudioSource>();
    }


	// Update is called once per frame
	void Update () 
    {
        Movement();
        Shooting();
	}

    void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2D.AddForce( - Vector3.right * forceSideways * Time.deltaTime );
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet =  Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;

            bullet.rigidbody2D.velocity = rigidbody2D.velocity;
            
            bullet.rigidbody2D.AddForce(Vector3.up  * bulletForce);

            soundShoot.pitch = Random.Range(.9f, 1.1f);
            soundShoot.Play();
        }
    }
}
