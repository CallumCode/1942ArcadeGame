using UnityEngine;
using System.Collections;

public class HealthPickUP : PickUp
{
    public float healthGained = 20;

    protected override void PickUpeEfect(Player player)
    {
        base.PickUpeEfect(player);
        player.IncreaseHealth(healthGained);
    }
}
