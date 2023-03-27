using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
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
                    Debug.Log($"Entrega de {waitingRecipeSO.recipeName} realizada com sucesso");
                    waitingRecipeSOList.Remove(waitingRecipeSO);
                    return;
                }
            }            
        }

        Debug.Log("Entrega incorreta");
    }

    private void Awake() {
        if (Instance == null) Instance = this; else Destroy(this);
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer += Time.deltaTime;
        if (spawnRecipeTimer >= spawnRecipeTimerMax) {
            spawnRecipeTimer = 0f;
            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                Debug.Log(waitingRecipeSO.recipeName);
            }
        }
    }
}
