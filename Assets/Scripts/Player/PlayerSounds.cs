using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private SoundManager soundManager;
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = 0.1f;

    private void Awake() {
        player = GetComponent<Player>();        
    }
    // Start is called before the first frame update
    void Start()
    {
        soundManager = SoundManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f) {
            footstepTimer = footstepTimerMax;
            if (player.IsWalking()) {
                float volume = 1f;
                soundManager.PlayerFootstepsSounds(player.transform.position, volume);
            }
        }
    }
}
