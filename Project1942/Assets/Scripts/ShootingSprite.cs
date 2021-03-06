﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShootingSprite : MonoBehaviour
{
    protected float health = 100;
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
    protected Slider healthSlider;

    protected AudioSource soundShoot;

    // Use this for initialization
   

    public virtual void Init()
    {
        //     animator = GetComponent<Animator>();

        localCanvas = ObjectPool.instance.GetObject(localCanvasPrefab.name);
        localCanvas.transform.position = transform.position;

        healthObject = ObjectPool.instance.GetObject(healthSliderPrefab.name);
         healthObject.transform.position = transform.position += hpOffset;
        healthObject.transform.SetParent(localCanvas.transform);

        healthSlider = healthObject.GetComponent<Slider>();

        fireTimer = Random.Range(0, 1 / fireaRate);
        soundShoot = GetComponent<AudioSource>();

        IncreaseHealth(100);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, 100);

        if (healthSlider)
        {
            healthSlider.value = health;
        }

        if (health <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        GameObject explosion = ObjectPool.instance.GetObject(explosionPrefab.name);
        explosion.transform.position = transform.position;
        explosion.transform.rotation = transform.rotation;  
        explosion.rigidbody2D.velocity = rigidbody2D.velocity;

        ReturnSelf();
    }


    protected void UpdateCanvas()
    {
        if (localCanvas)
        {
            localCanvas.transform.position = transform.position;
        }
    }

    protected void ReturnSelf()
    {
     
        ObjectPool.instance.ReturnObject(healthObject);
        ObjectPool.instance.ReturnObject(localCanvas);
        ObjectPool.instance.ReturnObject(gameObject);

    }

    public void IncreaseHealth(float healthGained)
    {
        health += healthGained;
        health = Mathf.Clamp(health, 0, 100);

        if (healthSlider)
        {
            healthSlider.value = health;
        }
    }


}
