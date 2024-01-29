using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private int _playerIndex;
    private InteractableObj _currentObj;
    private bool _interacting;

    [SerializeField] private Rigidbody rbMonk;
    [SerializeField] private float accel = 100;
    [SerializeField] private float maxSpeed = 8;
    
    public void Move(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        
        Vector3 direction = ctx.ReadValue<Vector2>();
        direction = new Vector3(direction.x, 0, direction.y);
        rbMonk.AddForce(direction*accel,ForceMode.Impulse);
        if (rbMonk.velocity.magnitude > maxSpeed) rbMonk.velocity = rbMonk.velocity.normalized * maxSpeed;
        
        transform.rotation = Quaternion.LookRotation(direction);
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
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3))
        {
            if (hit.transform.gameObject.layer != 6) return; //layer 6 = interaction
            _currentObj = hit.transform.GetComponent<InteractableObj>();
            _currentObj.PressAction();
            _interacting = true;
        }
    }
    
    private void Release()
    {
        if (!_interacting) return;
        _currentObj.ReleaseAction();
        _interacting = false;
    }
}
