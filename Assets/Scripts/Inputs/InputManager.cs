using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(this);
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        // Ler inputs
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // Normalizar para a velocidade diagonal não ser maior que a horizontal/vertical
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
