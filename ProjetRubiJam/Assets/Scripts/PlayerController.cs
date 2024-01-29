using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MonkInputAction _monkInputAction;
    private int _playerIndex;

    [SerializeField] private Rigidbody rbMonk;
    [SerializeField] private PlayerInput inputMonk;
    [SerializeField] private float accel = 100;
    
    // private void Awake()
    // {
    //     _monkInputAction = new MonkInputAction();
    //
    //     _playerIndex = inputMonk.playerIndex;
    // }
    //
    // private void OnEnable()
    // {
    //     switch (_playerIndex)
    //     {
    //         case 0:
    //             _monkInputAction.Player1.Enable();
    //             _monkInputAction.Player1.Move.performed += context => Move(context.ReadValue<Vector2>());
    //             break;
    //         case 1:
    //             _monkInputAction.Player2.Enable();
    //             _monkInputAction.Player2.Move.performed += context => Move(context.ReadValue<Vector2>());
    //             break;
    //         default:
    //             break;
    //     }
    // }
    //
    // private void OnDisable()
    // {
    //     _monkInputAction.Disable();
    // }
    //
    // private void Move(Vector2 direction)
    // {
    //     rbMonk.AddForce(direction*accel,ForceMode.Impulse);
    // }
    
    public void Move(InputAction.CallbackContext ctx)
    {
        rbMonk.AddForce(ctx.ReadValue<Vector2>()*accel,ForceMode.Impulse);
    }
}
