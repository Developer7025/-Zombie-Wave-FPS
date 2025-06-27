using UnityEngine;

public class PlayerCam : MonoBehaviour
{   public float cameraSensitivity ;
    public Transform player ;
    public Vector2 turn ;

    public PlayerHealth health;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked ;
        Cursor.visible = false ;
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.died)
        {
            turn.y += Input.GetAxis("Mouse X") * Time.deltaTime * cameraSensitivity;
            turn.x -= Input.GetAxis("Mouse Y") * Time.deltaTime * cameraSensitivity;
            turn.x = Mathf.Clamp(turn.x, -90f, 90f);


            transform.rotation = Quaternion.Euler(turn.x, turn.y, 0);
            player.rotation = Quaternion.Euler(0, turn.y, 0);
        }
    }
}
