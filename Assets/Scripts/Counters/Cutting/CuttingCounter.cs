using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter, IProgressBar {

    public event EventHandler OnCut;
    public event EventHandler<IProgressBar.OnProgressChangedEventArgs> OnProgressChanged;
    

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSos;
    [SerializeField] private Color cuttingProgressBarColor;
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // Vazio 
            if (player.HasKitchenObject()) {
                // o player está carregando algo, colocar na mesa
                player.GetKitchenObject().SetKitchenObjectParent(this);
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO();
                if (cuttingRecipeSO != null) {
                    UpdateProgressBar((float)GetKitchenObject().GetCuttingProgress() / cuttingRecipeSO.cuttingProgressMax, true);
                }
            }
        } else {
            // Ocupado
            if (!player.HasKitchenObject()) {
                // o player não está carregando algo, dar objeto pro player
                GetKitchenObject().SetKitchenObjectParent(player);
                UpdateProgressBar(0f, false);
            } else {
                // Player carregando algo, verificar se é prato
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // Player carregando um prato
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            // a mesa está com objeto, pode cortar se tiver uma receita que use como entrada 
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
        // Destrói o inteiro
        GetKitchenObject().DestroySelf();
        UpdateProgressBar(0f, false);
        // Spawna o cortado
        KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
    }

    private void UpdateProgressBar(float progressNormalized, bool showBar) {
        OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs {
            progressNormalized = progressNormalized,
            showBar = showBar,
            barColor = cuttingProgressBarColor
        });
    }
}
