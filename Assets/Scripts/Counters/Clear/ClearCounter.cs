using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // Vazia 
            if (player.HasKitchenObject()) {
                // o player está carregando algo, colocar na mesa
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            // Ocupada
            if (player.HasKitchenObject()) {
                // O player está carregando algo, verificar se é um prato
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // Player carregando um prato
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    // Player carregando outra coisa que não é um prato, testar se a mesa tem um prato e colocar objeto da mão do player no prato
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else { 
                // o player não está carregando algo, dar objeto pro player
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
