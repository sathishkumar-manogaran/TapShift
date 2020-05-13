
using UnityEngine;

public class InvisibilityPickup : Pickup
{
    public float duration;

   #region Override Functions
    protected override void OnPlayerCollect() {
        base.OnPlayerCollect();
        //specific logic
        game.HandleInvincibilityPickup(duration);

    }
   #endregion
}
