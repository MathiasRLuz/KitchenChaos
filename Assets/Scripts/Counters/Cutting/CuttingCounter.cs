using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSos;

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
        if (HasKitchenObject()) {
            // a mesa está com objeto, pode cortar            
            foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSos) {
                if (GetKitchenObject().GetKitchenObjectSO() == cuttingRecipeSO.input) {
                    // Destrói o inteiro
                    GetKitchenObject().DestroySelf();
                    // Spawna o cortado
                    KitchenObject.SpawnKitchenObject(cuttingRecipeSO.output, this);
                    break;
                }
            }            
        }
    }
}
