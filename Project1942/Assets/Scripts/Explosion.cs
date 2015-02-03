using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{
  
	// Use this for initialization
	void Start () 
    {
   
	}


    void ReturnSelf()
    {
        ObjectPool.instance.ReturnObject(gameObject);
     }
}
