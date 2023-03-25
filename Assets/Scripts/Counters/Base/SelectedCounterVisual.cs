using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjects;

    private Player player;

    private void Start() {
        player = Player.Instance;
        player.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        Show(e.selecterCounter == baseCounter);
    }

    private void Show(bool show) {
        foreach (GameObject visualGameObject in visualGameObjects) {
            visualGameObject.SetActive(show);
        }        
    }
}
