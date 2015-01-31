using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{
 

     void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(  other);
    }

    void HandleCollision(Collider2D other)
    {
         if(other.CompareTag("Player"))
         {
             PickUpeEfect( other.gameObject.GetComponent<Player>() ) ;
             // will have animation or sound here
             DestroySelf();
         }
    }

    protected void DestroySelf()
    {
        Destroy(gameObject);
    }

    protected virtual void PickUpeEfect(Player player)
    {
        Debug.Log("pickup");
    }
}
