using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(_cameraTransform);
    }
}
