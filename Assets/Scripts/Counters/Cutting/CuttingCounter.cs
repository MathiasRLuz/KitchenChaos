using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSos;

    public override void Interact(Player player) {
    if (!HasKitchenObject()) {
        // Vazio 
        if (player.HasKitchenObject()) {
            // o player est� carregando algo
            player.GetKitchenObject().SetKitchenObjectParent(this);
        }
    } else {
        // Ocupado
        if (!player.HasKitchenObject()) {
            // o player n�o est� carregando algo
            GetKitchenObject().SetKitchenObjectParent(player);
        }
    }
}

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            // a mesa est� com objeto, pode cortar            
            foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSos) {
                if (GetKitchenObject().GetKitchenObjectSO() == cuttingRecipeSO.input) {
                    // Destr�i o inteiro
                    GetKitchenObject().DestroySelf();
                    // Spawna o cortado
                    KitchenObject.SpawnKitchenObject(cuttingRecipeSO.output, this);
                    break;
                }
            }            
        }
    }
}
