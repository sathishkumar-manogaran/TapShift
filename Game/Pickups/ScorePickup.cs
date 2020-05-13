using UnityEngine;

public class ScorePickup : Pickup
{

    public float duration;
    public int multiplier;
    
   #region Override Functions
    protected override void OnPlayerCollect() {
        base.OnPlayerCollect();
        //specific logic
        game.HandleScorePickup(multiplier, duration);

    }
   #endregion
}
