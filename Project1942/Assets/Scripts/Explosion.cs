using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{
  
    void ReturnSelf()
    {
        ObjectPool.instance.ReturnObject(gameObject);
     }
}
