using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private int _playerIndex;
    private InteractableObj _currentObj;
    private bool _interacting;
    private Vector3 _currentDirection;
    
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Rigidbody rbMonk;
    [SerializeField] private float accel = 100;
    [SerializeField] private float maxSpeed = 8;
    
    public void Move(InputAction.CallbackContext ctx)
    {
         _currentDirection = ctx.ReadValue<Vector2>();
         _currentDirection = new Vector3(_currentDirection.x, 0, _currentDirection.y);
    }
    
    public void Interact(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.performed);
        if (ctx.performed) Press();
        else Release();
    }

    private void Press()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward.normalized*3,Color.blue);
        
        if(Physics.BoxCast(transform.position,new Vector3(0,1,0.5f),transform.forward,out hit,Quaternion.LookRotation(transform.forward),3,layerMask))
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
        rbMonk.AddForce(_currentDirection*accel,ForceMode.Impulse);
        if (rbMonk.velocity.magnitude > maxSpeed) rbMonk.velocity = rbMonk.velocity.normalized * maxSpeed;
        
        if(_currentDirection!=Vector3.zero) transform.rotation = Quaternion.LookRotation(_currentDirection);
    }
}
