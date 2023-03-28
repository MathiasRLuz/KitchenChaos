using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public class OnSoundEffectsEventArgs : EventArgs {
        public Vector3 soundPosition;
    }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    private Player player;
    private DeliveryManager deliveryManager;
    private void Awake() {
        if (Instance == null) Instance = this; else Destroy(this);
    }

    private void Start() {
        deliveryManager = DeliveryManager.Instance;
        player = Player.Instance;
        deliveryManager.OnDeliverSuccess += DeliveryManager_OnDeliverSuccess;
        deliveryManager.OnDeliverFailed += DeliveryManager_OnDeliverFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        player.OnPickedSomething += Player_OnPickedSomething;
        player.OnDropedSomething += Player_OnDropedSomething;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void Player_OnDropedSomething(object sender, EventArgs e) {
        PlaySound(audioClipRefsSO.objectDrop, player.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e) {
        PlaySound(audioClipRefsSO.objectPickUp, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;        
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }    

    private void DeliveryManager_OnDeliverFailed(object sender, OnSoundEffectsEventArgs e) {        
        PlaySound(audioClipRefsSO.deliveryFail, e.soundPosition);
    }   

    private void DeliveryManager_OnDeliverSuccess(object sender, OnSoundEffectsEventArgs e) {        
        PlaySound(audioClipRefsSO.deliverySuccess, e.soundPosition);
    }
    
    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f) {        
        AudioSource.PlayClipAtPoint(audioClips[UnityEngine.Random.Range(0, audioClips.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayerFootstepsSounds(Vector3 position, float volume) {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }
}
