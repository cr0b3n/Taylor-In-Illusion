using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SpawnAudio : MonoBehaviour {

    public float playGap = .2f;

    private float timer;
    private bool canPlay;

    private void OnEnable() {
        canPlay = true;
        timer = playGap;
        AudioManager.PlayAudioEffect(EffectAudio.SpawnPop);
    }

    private void Update() {

        canPlay = CheckIfCountDownElapsed();
    }

    private void OnParticleCollision(GameObject other) {

        if (!canPlay) return;

        timer = playGap;
        AudioManager.PlayAudioEffect(EffectAudio.Spawn);
    }

    private bool CheckIfCountDownElapsed() {

        timer -= Time.deltaTime;
        return timer <= 0;
    }
}
