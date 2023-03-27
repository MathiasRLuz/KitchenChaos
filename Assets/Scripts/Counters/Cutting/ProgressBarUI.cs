using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] Image progressBarImage;
    [SerializeField] Image backgroundImage;
    [SerializeField] GameObject IProgressBarGameObject;
    private IProgressBar progressBar;

    private void Start() {
        progressBar = IProgressBarGameObject.GetComponent<IProgressBar>();
        if (progressBar == null) {
            Debug.LogWarning($"O objeto {IProgressBarGameObject} não implementa IProgressBar");
        } else {
            progressBar.OnProgressChanged += ProgressBar_OnProgressChanged;            
        }
        progressBarImage.fillAmount = 0f;
        backgroundImage.gameObject.SetActive(false);
    }

    private void ProgressBar_OnProgressChanged(object sender, IProgressBar.OnProgressChangedEventArgs e) {
        backgroundImage.gameObject.SetActive(e.showBar);
        progressBarImage.fillAmount = e.progressNormalized;
        progressBarImage.color = e.barColor;
    }
}
