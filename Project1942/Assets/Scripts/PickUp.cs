using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{

    public float scoreGained = 100;

     void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(  other);
    }

    void HandleCollision(Collider2D other)
    {
         if(other.CompareTag("Player"))
         {
             PickUpeEfect( other.gameObject.GetComponent<Player>() ) ;
             ReturnSelf();
         }
    }

    protected void ReturnSelf()
    {
        ObjectPool.instance.ReturnObject(gameObject);
    }

    protected virtual void PickUpeEfect(Player player)
    {
        GameProgressHandler.instance.AddScore(scoreGained);
        player.PlayPickUp();

    }

    void OnBecameInvisible()
    {
        ReturnSelf();
    }

}
