using _Template.Script.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Farmer
{
    [SerializeField] CharacterController _charController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Vector3 direction;
    [SerializeField] Joystick joystick;
    public bool canMove;

    void Update()
    {
        if(canMove)
        {
            Move();
        }
    }
    [SerializeField] LayerMask groundMask;
    public void Move()
    {
        groundedPlayer = _charController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = joystick.Direction;
        bool moving = (move != Vector3.zero);
        _animator.SetBool("walk", moving);

        direction = Vector3.Lerp(direction, move, 0.8f);
        Vector3 nextPos = transform.position + direction * Time.deltaTime * _playerSpeed;
        if(Physics.Raycast(nextPos+Vector3.up,Vector3.down,5,groundMask))
        {
            _charController.Move(direction * Time.deltaTime * _playerSpeed);
        }

        if (direction != Vector3.zero)
        {
            gameObject.transform.forward = direction;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        if(groundedPlayer)
        {
        _charController.Move(playerVelocity * Time.deltaTime);

        }
    }
}
