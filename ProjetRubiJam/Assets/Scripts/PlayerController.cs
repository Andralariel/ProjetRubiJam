using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MonkInputAction _monkInputAction;

    [SerializeField] private Rigidbody rbMonk;
    [SerializeField] private float accel = 100;
    
    private void Awake()
    {
        _monkInputAction = new MonkInputAction();
    }

    void OnEnable()
    {
        _monkInputAction.Enable();

        _monkInputAction.MonkActionMap.Move.performed += context => Move(context.ReadValue<Vector2>());
    }
    
    void OnDisable()
    {
        _monkInputAction.Disable();
    }

    private void Move(Vector2 direction)
    {
        rbMonk.AddForce(direction*accel,ForceMode.Impulse);
    }
}
