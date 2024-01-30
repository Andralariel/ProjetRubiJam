using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private int _playerIndex;
    private float _dashTimer;
    private InteractableObj _currentObj;
    private bool _interacting;
    private bool _dashing;
    private Vector3 _currentDirection;
    
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Rigidbody rbMonk;
    [SerializeField] private float stickDeadZone = 0.125f;
    [SerializeField] private float accel = 100;
    [SerializeField] private float maxSpeed = 8;
    [SerializeField] private float dashAccel = 20;
    [SerializeField] private float dashCooldown = 20;
    
    public void Move(InputAction.CallbackContext ctx)
    {
         _currentDirection = ctx.ReadValue<Vector2>();
         _currentDirection = new Vector3(_currentDirection.x, 0, _currentDirection.y);
         if(_currentDirection.magnitude<stickDeadZone) _currentDirection = Vector3.zero;
    }
    
    public void Interact(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.performed);
        if (ctx.performed) Press();
        else Release();
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        _dashing = true;
        rbMonk.AddForce(transform.forward*dashAccel,ForceMode.Impulse);
    }

    private void Press()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward.normalized*3,Color.blue);
        
        if(Physics.BoxCast(transform.position,new Vector3(0,1,0.4f),transform.forward,out hit,Quaternion.LookRotation(transform.forward),3,layerMask))
        {
            _currentObj = hit.transform.GetComponent<InteractableObj>();
            _currentObj.PressAction(this);
            _interacting = true;
        }
    }
    
    private void Release()
    {
        if (!_interacting) return;
        _currentObj.ReleaseAction();
        _interacting = false;
    }

    private void FixedUpdate()
    {
        if (!_dashing)
        {
            rbMonk.AddForce(_currentDirection*accel,ForceMode.Impulse);
            if (rbMonk.velocity.magnitude > maxSpeed) rbMonk.velocity = rbMonk.velocity.normalized * maxSpeed;
        }
        else if (rbMonk.velocity.magnitude < maxSpeed) _dashing = false;
        
        if(_currentDirection!=Vector3.zero) transform.rotation = Quaternion.LookRotation(_currentDirection);
    }
}
