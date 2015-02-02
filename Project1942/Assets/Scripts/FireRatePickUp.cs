using UnityEngine;
using System.Collections;

public class FireRatePickUp : PickUp
{
    public float fireRateIncrease = 1;

    protected override void PickUpeEfect(Player player)
    {
        base.PickUpeEfect(player);

        player.ChangeFireRate(fireRateIncrease);
    }
	 
}
