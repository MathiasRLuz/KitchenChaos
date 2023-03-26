using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] Image progressBarImage;
    [SerializeField] Image backgroundImage;
    [SerializeField] CuttingCounter cuttingCounter;

    private void Start() {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
        progressBarImage.fillAmount = 0f;
        backgroundImage.gameObject.SetActive(false);
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e) {
        backgroundImage.gameObject.SetActive(e.showBar);
        progressBarImage.fillAmount = e.progressNormalized;
    }
}
