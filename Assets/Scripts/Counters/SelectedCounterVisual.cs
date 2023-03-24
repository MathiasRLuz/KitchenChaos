using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    private Player player;

    private void Start() {
        player = Player.Instance;
        player.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEvebtArgs e) {
        Show(e.selecterCounter == clearCounter);
    }

    private void Show(bool show) {
        visualGameObject.SetActive(show);
    }
}
