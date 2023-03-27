using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                Deliver(player.GetKitchenObject());        
            }
        }
    }

    private void Deliver(KitchenObject kitchenObject) {
        kitchenObject.DestroySelf();
    }
}
