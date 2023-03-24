 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    private bool isWalking;
    InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        // Ler inputs
        Vector2 inputVector = inputManager.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        // Olhar para a direção
        float rotateSpeed = 10;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        // Checar colisões
        float playerRadius = 0.7f;
        float playerHeight = 1.8f;
        float moveDistance = Time.deltaTime * moveSpeed;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // Se não puder mover diagonalmente, checar se pode mover em apenas uma direção (horizontal ou vertical)
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            } else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                } else
                {
                    // Realmente não consegue se mover em nenhuma direção
                }              
            }
        }

        // Mover
        if (canMove)
            transform.position += moveDir * moveDistance;

        // Atualizar isWalking
        isWalking = moveDir != Vector3.zero;

    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
