using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void Destroy()
    {
        // this will do animation death etc
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    { 
        Destroy(gameObject);
    }
}
