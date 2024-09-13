using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAim : MonoBehaviour
{
    [SerializeField]private LayerMask enemyMask;
    
    private Vector2 _lookingDirection;

    private Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        RotateBox();
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1000f, enemyMask.value);
        
        if(hit.collider == null)
            return;

        hit.collider.gameObject.GetComponentInParent<Unit>().LookingAtPlayer();
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
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + _lookingDirection * 1000f);
    }
}
