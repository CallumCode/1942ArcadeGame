using UnityEngine;
using System.Collections;

public class Sprite : MonoBehaviour
{
    float health = 100;
    public GameObject explosionPrefab;

    public GameObject bulletPrefab;
    public float bulletForce = 10;
    public float fireaRate = 1;

    protected float fireTimer = 0;

  //  Animator animator;

    public Transform bulletSpawn;


	// Use this for initialization
	void Start () 
    {
        Init();
	}
	
    public virtual void Init()
    {     
   //     animator = GetComponent<Animator>();
    }


	// Update is called once per frame
    void Update()
    {
        
    }

   public void TakeDamage( float amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, 100);

        if (health <= 0)
        {
            Death();
        }
    }

   void Death()
   {
       GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
       explosion.rigidbody2D.velocity = rigidbody2D.velocity;
        Destroy(gameObject);   
   }


}
