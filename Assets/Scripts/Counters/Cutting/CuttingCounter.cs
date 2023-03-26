using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter {

    public event EventHandler OnCut;
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized;
        public bool showBar;
    }

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSos;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // Vazio 
            if (player.HasKitchenObject()) {
                // o player est� carregando algo, colocar na mesa
                player.GetKitchenObject().SetKitchenObjectParent(this);
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO();
                if (cuttingRecipeSO != null) {
                    UpdateProgressBar((float)GetKitchenObject().GetCuttingProgress() / cuttingRecipeSO.cuttingProgressMax, true);
                }
            }
        } else {
            // Ocupado
            if (!player.HasKitchenObject()) {
                // o player n�o est� carregando algo, dar objeto pro player
                GetKitchenObject().SetKitchenObjectParent(player);
                UpdateProgressBar(0f, false);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            // a mesa est� com objeto, pode cortar se tiver uma receita que use como entrada 
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO();
            if (cuttingRecipeSO != null) {
                OnCut?.Invoke(this, EventArgs.Empty);
                GetKitchenObject().IncreaseCuttingProgress();
                UpdateProgressBar((float)GetKitchenObject().GetCuttingProgress() / cuttingRecipeSO.cuttingProgressMax, true);
                if (GetKitchenObject().GetCuttingProgress() >= cuttingRecipeSO.cuttingProgressMax)
                    Cut(cuttingRecipeSO.output);
            }
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSO() {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSos) {
            if (GetKitchenObject().GetKitchenObjectSO() == cuttingRecipeSO.input) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

    private void Cut(KitchenObjectSO kitchenObjectSO) {
        // Destr�i o inteiro
        GetKitchenObject().DestroySelf();
        UpdateProgressBar(0f, false);
        // Spawna o cortado
        KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
    }

    private void UpdateProgressBar(float progressNormalized, bool showBar) {
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
            progressNormalized = progressNormalized,
            showBar = showBar
        });
    }
}
