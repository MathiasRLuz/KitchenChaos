using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer = 0;
    private float spawnPlateTimerMax = 4f;
    private int platesNumberMax = 4;
    private int platesNumber = 0;
    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= spawnPlateTimerMax) {
            spawnPlateTimer = 0;
            // Spawna o prato se couber
            if (platesNumber < platesNumberMax) {
                platesNumber++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }            
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // Player sem nada na mão
            if (platesNumber > 0) {
                platesNumber--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
