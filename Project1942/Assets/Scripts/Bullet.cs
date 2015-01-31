using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    public float lifeTime = 25;
    float timer;
	// Use this for initialization
	void Start () 
    {
        timer = Time.time;
	}

    // Update is called once per frame
    void Update()
    {
        if (Time.time > (timer + lifeTime))
        {
            Destroy();
        }
    }

    void Destroy()
    {
         Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("PlayerBullet") || coll.collider.CompareTag("EnemyBullet") )
        {
            Destroy();
        }
    }
}
