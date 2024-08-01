using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

        [Header("Setup")] 
        [SerializeField] private PlayerInput pInput;
        [SerializeField] private Rigidbody2D rb;

        [Header("Character Physics")] 
        [SerializeField] private Vector2 currentMovement;
        [SerializeField] private float moveSpeed;

    #endregion

    // Start is called before the first frame update

    private void OnEnable()
    {
        pInput.actions.Enable();
    }

    private void OnDisable()
    {
        pInput.actions.Disable();
    }

    void FixedUpdate()
    {
        DoPlayerMovement();
    }

    private void DoPlayerMovement()
    {
        rb.MovePosition(rb.position+currentMovement*(moveSpeed*Time.deltaTime));
    }

    private void OnMove()
    {
        currentMovement = pInput.actions.FindAction("Move").ReadValue<Vector2>();
    }
}