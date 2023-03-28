using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    private GameManager gameManager;
    private DeliveryManager deliveryManager;

    private void Start() {
        gameManager = GameManager.Instance;
        deliveryManager = DeliveryManager.Instance;
        gameManager.OnStateChanged += GameManager_OnStateChanged;
        gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        gameObject.SetActive(gameManager.IsGameOver());
        if (gameManager.IsGameOver())
            recipesDeliveredText.text = deliveryManager.GetSuccessfulRecipesDelivered().ToString();
    }
}
