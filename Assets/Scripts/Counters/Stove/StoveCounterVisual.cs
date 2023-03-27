using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnVisual;
    [SerializeField] private GameObject sizzlingParticles;

    private void Start() {
        stoveCounter.OnCooking += StoveCounter_OnCooking;
        stoveCounter.OnStoveOn += StoveCounter_OnStoveOn;
    }

    private void StoveCounter_OnStoveOn(object sender, StoveCounter.OnStoveOnEventArgs e) {
        stoveOnVisual.SetActive(e.stoveOn);
    }

    private void StoveCounter_OnCooking(object sender, StoveCounter.OnCookingEventArgs e) {
        sizzlingParticles.SetActive(e.cooking);
    }
}
