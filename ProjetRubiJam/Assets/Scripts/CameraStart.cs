using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStart : MonoBehaviour
{
    [SerializeField] private List<Transform> posCamera = new(2);
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float duration = 2f;
    [SerializeField] private float elapsedTime = 0f;
    public bool doMove;
    
    void Awake()
    {
        Debug.Log(posCamera[0].position);
        Debug.Log(posCamera[0].rotation);
        transform.position = posCamera[0].position;
        transform.rotation = posCamera[0].rotation;
    }
    
   
    void Update()
    {
        if (!doMove) return;
        MoveCameraOnStart();
    }

    void MoveCameraOnStart()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float curveValue = animCurve.Evaluate(t);
            
            transform.position = Vector3.Lerp(posCamera[0].position, posCamera[1].position, curveValue);
            transform.rotation = Quaternion.Slerp(posCamera[0].rotation, posCamera[1].rotation, curveValue);
        }
    }

    public void DoMove()
    {
        doMove = true;
    }
}
