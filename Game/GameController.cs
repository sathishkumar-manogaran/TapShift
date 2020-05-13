using UnityEngine;
using UnityCore.Session;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public CameraController camera;
    public PlayerController player;
    public ObstacleController obstacles;

    private SessionController m_Session;
    private int m_Progress = -1;
    private int m_ScoreMultiplier = 1;
    private bool m_Invincible;
    private float m_ScoreMultiplierDuration;
    private float m_InvincibilityDuration;
    private bool m_DidDropPickup;
    public int pickupDropRate;


    // get session with integrity
    private SessionController session
    {
        get
        {
            if (!m_Session)
            {
                m_Session = SessionController.instance;
            }
            if (!m_Session)
            {
                Debug.LogWarning("Game is trying to access the session, but no instance of SessionController was found");
            }
            return m_Session;
        }
    }

    #region Unity Functions
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (!session) return;
        session.InitializeGame(this);
    }
    #endregion

    #region Public Functions
    public void OnInit()
    {
        // initialize all of our game systems
        player.OnInit();
        camera.OnInit();
        obstacles.AddObstacles(m_Progress);
    }

    public void OnUpdate()
    {
        // update all of our game systems
        player.OnUpdate();
        camera.OnUpdate();
        CheckPlayerProgress();
    }

    public void HandleInvincibilityPickup(float _duration)
    {
        m_InvincibilityDuration = _duration;
        m_Invincible = true;

        // cancel score pickup
        m_ScoreMultiplier = 1;
        m_ScoreMultiplierDuration = 0;
    }

    public void HandleScorePickup(int _multiplier, float _duration)
    {
        m_ScoreMultiplier = _multiplier;
        m_ScoreMultiplierDuration = _duration;

        // cancel invincibility pickup
         m_InvincibilityDuration = 0;
        m_Invincible = false;
    }

    #endregion

    #region Private Functions
    private void CheckPlayerProgress()
    {
        if (player.transform.position.y / obstacles.interval > (m_Progress + 1))
        {
            m_Progress++;
            obstacles.AddObstacles(m_Progress);
        }

        if(m_Progress > 0 && m_Progress % pickupDropRate == 0) {
            if(!m_DidDropPickup) {
                m_DidDropPickup = true;
                obstacles.AddPickups(m_Progress);
            } else {
                 m_DidDropPickup = false;
            }
        }
    }
    #endregion
}
