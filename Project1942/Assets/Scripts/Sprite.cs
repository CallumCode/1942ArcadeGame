using UnityEngine;
using System.Collections;

public class Sprite : MonoBehaviour
{
    float health = 100;


    Animator animator;

	// Use this for initialization
	void Start () 
    {
        Init();
	}
	
    public virtual void Init()
    {     
        animator = GetComponent<Animator>();
    }


	// Update is called once per frame
    void Update()
    {
        
    }

   public void TakeDamage( float amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, 100);

        if (health < 0)
        {
            Death();
        }
    }

   void Death()
   {
       Destroy(gameObject);   
   }

    
}
