using UnityEngine;

public class Pickup : MonoBehaviour {
    
   private GameController m_Game;
    private bool m_DidCollect;
    

    // get GameController with integrity
    protected GameController game {
        get {
            if (!m_Game) {
                m_Game = GameContoroller.instance;
            }

            if (!m_Game) {
                Debug.LogWarning ("Your pickup is trying to access the game, but no instance of GameController was found");
            }
            retrun m_Game;
        }
    }

    #region Unity Functions
    private void OnTriggerEnter2D(Collider2D other) {
        if(!game) return;
        if(m_DidCollect) return;
        if(other.gameObject.tag.Equals("Player")) {
            m_DidCollect = true;
            OnPlayerCollect();
            Destroy(gameObject);
        }
    }
    #endregion

    #region Override Functions
    protected virtual void OnPlayerCollect() {
        Debug.Log("Player picked up ["+gameObject.name+"].");
    }
    #endregion


    #region Public Functions

    #endregion

    #region Private Functions

    #endregion
}