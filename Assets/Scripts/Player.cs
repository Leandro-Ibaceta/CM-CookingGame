using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]GameInput gameInput;
    private float moveSpeed = 7f;
    private bool isWalking;
    private Vector3 lastInteractDirection;
    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        HandleInteraction();
    }
    private void HandleInteraction() 
    {
        Vector2 input = gameInput.GetMovementNormalize();

        Vector3 moveDir = new Vector3(input.x, 0, input.y);
        if (moveDir != Vector3.zero)
            lastInteractDirection = moveDir;

        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDirection,interactDistance))
        {
            Debug.Log("Touch something");
        }
    }

    private void MoveCharacter()
    {
        Vector2 input = gameInput.GetMovementNormalize();

        Vector3 moveDir = new Vector3(input.x, 0, input.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if(!canMove)
        {
            //try move on X axis
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if(canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                //try to move on Z axis
                Vector3 moveDirZ = new Vector3(0,0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position +=  moveDistance * moveDir;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 8f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
