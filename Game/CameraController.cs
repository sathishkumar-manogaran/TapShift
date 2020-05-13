using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

    public Transform player;
    public float smooth;

    private Camera m_Camera;
    private Vector3 m_TargetPosition;
    private Vector3 m_InitialPosition;


    #region Unity Functions

    #endregion

    #region Public Functions
    public void OnInit()
    {
        m_Camera = GetComponent<Camera>();
        m_TargetPosition = transform.position;
        m_InitialPosition = m_TargetPosition;
    }

    public void OnUpdate()
    {
        FollowPlayer();
    }
    #endregion

    #region Private Functions
    private void FollowPlayer()
    {
        if (!player)
        {
            Debug.LogWarning("Camera could not find a reference to the player");
            return;
        }

        if (player.transform.position.y > transform.position.y)
        {
            m_TargetPosition.y = player.position.y;
        }

        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, smooth * Time.deltaTime);
    }
    #endregion
}
