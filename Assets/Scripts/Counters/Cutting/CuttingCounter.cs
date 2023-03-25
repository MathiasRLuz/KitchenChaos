using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSos;
    private int cuttingProgress;

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            // a mesa está com objeto, pode cortar se tiver uma receita que use como entrada 
            foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSos) {
                if (GetKitchenObject().GetKitchenObjectSO() == cuttingRecipeSO.input) {
                    if (GetKitchenObject().GetCuttingProgress() < cuttingRecipeSO.cuttingProgressMax - 1)
                        GetKitchenObject().IncreaseCuttingProgress();
                    else
                        Cut(cuttingRecipeSO.output);
                    break;
                }
            }            
        }
    }

    private void Cut(KitchenObjectSO kitchenObjectSO) {
        // Destrói o inteiro
        GetKitchenObject().DestroySelf();
        // Spawna o cortado
        KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
    }
}
