using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSos;

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            // a mesa está com objeto, pode cortar se tiver uma receita que use como entrada 
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
