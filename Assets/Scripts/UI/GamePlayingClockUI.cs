using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.Instance;
        gameManager.OnStateChanged += GameManager_OnStateChanged;
        gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        gameObject.SetActive(gameManager.IsGamePlaying());
    }

    private void Update() {
        timerImage.fillAmount = gameManager.GetGameTimerNormalized();
    }
}
