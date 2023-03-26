using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // Vazio 
            if (player.HasKitchenObject()) {
                // o player está carregando algo, colocar no lixo
                player.GetKitchenObject().DestroySelf();
            }
        } 
    }
}
