using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    public override void Interact(Player player) {
        // Verificar se o player j� tem algo na m�o
        if (player.HasKitchenObject()) {
            // Player segurando algo
            // Verificar se � o objeto desse container
            if (player.GetKitchenObject().GetKitchenObjectSO() == kitchenObjectSO) {
                // "Guardar" o objeto
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
                player.GetKitchenObject().DestroySelf();
            }
        } else { 
            // Player de m�os vazias
            // Criar o objeto e passar para o player
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        }
    }
}
