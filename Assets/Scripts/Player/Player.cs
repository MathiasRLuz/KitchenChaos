using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {
    public class OnSelectedCounterChangedEvebtArgs : EventArgs {
        public ClearCounter selecterCounter;
    }

    public event EventHandler<OnSelectedCounterChangedEvebtArgs> OnSelectedCounterChanged;
    
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private InputManager inputManager;
    private ClearCounter selectedCounter;
    private KitchenObject kitchenObject;
    
    private void Awake() {
        if (Instance == null) Instance = this; else Destroy(this);
        inputManager = InputManager.Instance;
    }

    private void Start() {
        inputManager.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if (selectedCounter) {
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleMovement()
    {
        // Ler inputs
        Vector2 inputVector = inputManager.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        // Olhar para a direção
        float rotateSpeed = 10;
        if (moveDir != Vector3.zero)
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        // Checar colisões
        float playerRadius = 0.7f;
        float playerHeight = 1.8f;
        float moveDistance = Time.deltaTime * moveSpeed;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // Se não puder mover diagonalmente, checar se pode mover em apenas uma direção (horizontal ou vertical)
        if (!canMove) {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                moveDir = moveDirX;
            } else {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove) {
                    moveDir = moveDirZ;
                } else {
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

    private void HandleInteractions() {
        // Ler inputs
        Vector2 inputVector = inputManager.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float interactDistance = 2;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance, countersLayerMask)){
            // Checar se tem o componente ClearCounter
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                if (selectedCounter != clearCounter) {
                    SetSelectedCounter(clearCounter);                    
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter (ClearCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEvebtArgs {
            selecterCounter = selectedCounter
        });
    }

    public bool IsWalking() {
        return isWalking;
    }

    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
