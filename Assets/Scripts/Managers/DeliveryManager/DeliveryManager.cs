using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler<SoundManager.OnSoundEffectsEventArgs> OnDeliverSuccess;
    public event EventHandler<SoundManager.OnSoundEffectsEventArgs> OnDeliverFailed;
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject, DeliveryCounter deliveryCounter) {
        // verificar todas as receitas pedidas (sendo aguardadas)
        foreach (RecipeSO waitingRecipeSO in waitingRecipeSOList) {
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // mesmo numero de ingredientes
                // verificar se todos os ingredientes da receita waitingRecipeSO estão presentes no prato
                bool recipeMatches = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {                  
                    bool ingredientFound = false;
                    // verificar todos os ingredientes do prato
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        if (recipeKitchenObjectSO == plateKitchenObjectSO) {
                            // ingrediente encontrado
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        // Algum ingrediente da receita está faltando no prato
                        recipeMatches = false;                        
                    }
                }
                if (recipeMatches) {                    
                    waitingRecipeSOList.Remove(waitingRecipeSO);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnDeliverSuccess?.Invoke(this, new SoundManager.OnSoundEffectsEventArgs {
                        soundPosition = deliveryCounter.gameObject.transform.position
                    });
                    return;
                }
            }            
        }
        OnDeliverFailed?.Invoke(this, new SoundManager.OnSoundEffectsEventArgs {
                        soundPosition = deliveryCounter.gameObject.transform.position
                    });
    }

    private void Awake() {
        if (Instance == null) Instance = this; else Destroy(this);
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer < 0) {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
