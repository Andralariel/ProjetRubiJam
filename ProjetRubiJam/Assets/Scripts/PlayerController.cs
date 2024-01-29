using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private int _playerIndex;

    [SerializeField] private Rigidbody rbMonk;
    [SerializeField] private float accel = 100;
    [SerializeField] private float maxSpeed = 8;
    
    public void Move(InputAction.CallbackContext ctx)
    {
        var direction = ctx.ReadValue<Vector2>();
        rbMonk.AddForce(new Vector3(direction.x,0,direction.y)*accel,ForceMode.Impulse);
        if (rbMonk.velocity.magnitude > maxSpeed) rbMonk.velocity = rbMonk.velocity.normalized * maxSpeed;
    }
}
