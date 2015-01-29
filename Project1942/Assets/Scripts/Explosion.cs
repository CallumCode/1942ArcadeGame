using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{

 
	// Use this for initialization
	void Start () 
    {
        AudioSource explosion = GetComponent<AudioSource>();
        // will need to check lengh of animaiton too
        Invoke("Destroy",  explosion.clip.length );
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void Destroy()
    {
        Destroy(gameObject);
    }
}
