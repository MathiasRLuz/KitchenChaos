using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // Vazio 
            if (player.HasKitchenObject()) {
                // o player está carregando algo, colocar no lixo
                player.GetKitchenObject().DestroySelf();
                OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
            }
        } 
    }
}
