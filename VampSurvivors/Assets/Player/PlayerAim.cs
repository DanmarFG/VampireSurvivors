using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAim : MonoBehaviour
{
    private Vector2 _lookingDirection;

    private Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        RotateBox();
    }

    private void RotateBox()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _mainCamera.transform.position.z - transform.position.z;
        mousePos = _mainCamera.ScreenToWorldPoint(mousePos);
        
        _lookingDirection = mousePos - transform.position;
        
        transform.up = _lookingDirection;
    }

    public Vector2 GetAimDirection()
    {
        return _lookingDirection;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + _lookingDirection.normalized);
    }
}
