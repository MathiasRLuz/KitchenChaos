using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                Deliver(player.GetKitchenObject(), plateKitchenObject);        
            }
        }
    }

    private void Deliver(KitchenObject kitchenObject, PlateKitchenObject plateKitchenObject) {
        DeliveryManager.Instance.DeliverRecipe(plateKitchenObject, this);
        kitchenObject.DestroySelf();
    }
}
