using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlateIconTemplate : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    public void SetImage(Sprite sprite) {
        iconImage.sprite = sprite;
    }
}
