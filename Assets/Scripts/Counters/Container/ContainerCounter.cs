using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    public override void Interact(Player player) {
        // Verificar se o player já tem algo na mão
        if (player.HasKitchenObject()) {
            // Player segurando algo
            // Verificar se é o objeto desse container
            if (player.GetKitchenObject().GetKitchenObjectSO() == kitchenObjectSO) {
                // "Guardar" o objeto
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
                Destroy(player.GetKitchenObject().gameObject);
            }
        } else { 
            // Player de mãos vazias
            // Criar o objeto e passar para o player
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        }
    }
}
