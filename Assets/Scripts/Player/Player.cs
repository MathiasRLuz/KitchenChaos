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

        // Mover
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDir * Time.deltaTime * moveSpeed;

        // Atualizar isWalking
        isWalking = moveDir != Vector3.zero;

        // Olhar para a direção
        float rotateSpeed = 10;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
