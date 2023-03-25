using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;    

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // Vazio 
            if (player.HasKitchenObject()) {
                // o player está carregando algo
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            // Ocupado
            if (!player.HasKitchenObject()) {
                // o player não está carregando algo
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        throw new System.NotImplementedException();
    }
}
