using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager Instance;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public static event EventHandler OnPauseAction;

    private PlayerInputActions playerInputActions;

    private void Awake() {
        if (Instance == null) Instance = this; else Destroy(this);
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {        
        // Checar se o evento tem listeners. para ent�o disparar o evento
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        // Ler inputs
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // Normalizar para a velocidade diagonal n�o ser maior que a horizontal/vertical
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
