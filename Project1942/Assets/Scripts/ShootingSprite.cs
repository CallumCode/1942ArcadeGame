using UnityEngine;
using System.Collections;

public class ShootingSprite : MonoBehaviour
{
    protected  float health = 100;
    public GameObject explosionPrefab;
    public GameObject localCanvasPrefab;
    public GameObject healthSliderPrefab;

    public Vector3 hpOffset = Vector3.zero;

    GameObject localCanvas;

    public GameObject bulletPrefab;
    public float bulletForce = 50;
    public float fireaRate = 1;

    protected float fireTimer = 0;

  //  Animator animator;

    public Transform bulletSpawn;

    GameObject healthObject;
	// Use this for initialization
	void Start () 
    {
        Init();
	}
	
    public virtual void Init()
    {     
   //     animator = GetComponent<Animator>();
        
         localCanvas = Instantiate(localCanvasPrefab, transform.position, transform.rotation) as GameObject;

         healthObject = Instantiate(healthSliderPrefab, transform.position + hpOffset, transform.rotation) as GameObject;
         healthObject.transform.SetParent(localCanvas.transform);
        
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

       DestroySelf();
   
   }


    protected void UpdateCanvas()
   {
      localCanvas.transform.position = transform.position;
   }


   protected void DestroySelf()
    {
        Destroy(healthObject);
        Destroy(localCanvas);

        Destroy(gameObject);    
    }
}
