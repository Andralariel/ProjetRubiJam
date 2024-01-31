using UnityEngine;
using Random = UnityEngine.Random;

public class Coffin : InteractableObj
{
    [SerializeField] private GameObject coffinMesh;
    [SerializeField] private Collider coffinCollider;
    [SerializeField] private GameObject skeletonMesh;
    
    private bool _isSkeleton;
    private Transform _target;
    
    private void FixedUpdate()
    {
        if (MonkManager.instance.famine)
        {
            if (_isSkeleton)
            {
                var direction = _target.position - transform.position;
                rbObj.AddForce(direction.normalized*10,ForceMode.Force);
                transform.LookAt(_target);
            }
            else
            {
                _isSkeleton = true;
                coffinMesh.SetActive(false);
                coffinCollider.enabled = false;
                skeletonMesh.SetActive(true);
                _target = MonkManager.instance.playerList[Random.Range(0,MonkManager.instance.playerList.Count)].transform;
            }
        }
        else
        {
            if (_isSkeleton)
            {
                _isSkeleton = false;
                coffinMesh.SetActive(true);
                coffinCollider.enabled = true;
                skeletonMesh.SetActive(false);
            }
        }
    }
}
