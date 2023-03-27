using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgressBar {
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized;
        public bool showBar;
        public Color barColor;
    }
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
}
