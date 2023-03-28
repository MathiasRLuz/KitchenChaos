using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    GameManager gameManager;

    private void Start() {
        gameManager = GameManager.Instance;
        gameManager.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        countdownText.gameObject.SetActive(gameManager.IsCountdownToStartActive());
    }

    private void Update() {
        if (gameManager.IsCountdownToStartActive()) {            
            countdownText.text = Mathf.Ceil(gameManager.GetCountdownToStartTimer()).ToString();
        }
    }
}
