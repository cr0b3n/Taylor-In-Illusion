using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AudioManager : MonoBehaviour {

    static AudioManager current;

    [Header("Gameplay Audio Clips")]
    public AudioClip stepsClip;
    public AudioClip jumpClip;
    public AudioClip landClip;
    public AudioClip bonusClip;
    public AudioClip penaltyClip;
    public AudioClip spawnClip;
    public AudioClip spawnPopClip;
    public AudioClip deathClip;
    public AudioClip goalClip;

    [Header("UI Audio Clips")]
    public AudioClip buttonAudio;
    public AudioClip carretAudio;

    [Header("BGM")]
    public AudioClip gamePlayClip;
    public AudioClip menuBGMClip;
    public AudioClip endCreditClip;

    private AudioSource bgmSource;
    private AudioSource uiSource;
    private AudioSource playerSource;
    private AudioSource effectSource;

    private void Awake() {

        if (current != null && current != this) {
            Destroy(gameObject);
            return;
        }

        current = this;
        DontDestroyOnLoad(gameObject);

        bgmSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        uiSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        effectSource = gameObject.AddComponent<AudioSource>() as AudioSource;

        uiSource.ignoreListenerPause = true;
        uiSource.volume = .8f;        
    }

    public static void PlayBGM(BGMType bgmType) {

        switch (bgmType) {

            case BGMType.Gameplay:
                current.bgmSource.clip = current.gamePlayClip;
                current.bgmSource.volume = 1f;
                break;
            case BGMType.Menu:
                current.bgmSource.clip = current.menuBGMClip;
                current.bgmSource.volume = 1f;
                break;
            case BGMType.Credits:
                current.bgmSource.volume =1f;
                current.bgmSource.clip = current.endCreditClip;
                break;
        }

        //current.bgmSource.clip = (isMenu) ? current.menuBGMClip : current.gamePlayClip;
        current.bgmSource.loop = true;
        //
        current.bgmSource.Play();
    }

    public static void PlayAudioEffect(EffectAudio effectAudio) {

        //if (current.effectSource.isPlaying)
        //    current.effectSource.Stop();

        switch (effectAudio) {

            case EffectAudio.Bonus:
                current.effectSource.PlayOneShot(current.bonusClip);
                break;
            case EffectAudio.Penalty:
                current.effectSource.PlayOneShot(current.penaltyClip);
                break;
            case EffectAudio.SpawnPop:
                current.effectSource.clip = current.spawnPopClip;

                if (current.effectSource.isPlaying)
                    current.effectSource.Stop();

                current.effectSource.Play();
                //current.effectSource.PlayOneShot(current.spawnPopClip);
                break;
            case EffectAudio.Spawn:
                current.effectSource.PlayOneShot(current.spawnClip);
                break;
            case EffectAudio.Goal:
                current.effectSource.PlayOneShot(current.goalClip);
                break;
        }
    }

    public static void PlayPlayerAudio(PlayerAudio playerAudio) {

        if (current.playerSource.isPlaying)
            current.playerSource.Stop();

        switch (playerAudio) {
            case PlayerAudio.Step:
                current.effectSource.PlayOneShot(current.stepsClip);
                break;
            case PlayerAudio.Jump:
                current.effectSource.PlayOneShot(current.jumpClip);
                break;
            case PlayerAudio.Land:
                current.effectSource.PlayOneShot(current.landClip);
                break;
            case PlayerAudio.Death:
                current.effectSource.PlayOneShot(current.deathClip);
                break;
        }
    }

    public static void PlayGameOverAudio() {
        //current.uiSource.PlayOneShot(current.gameOverAudio);

    }

    public static void PlayUIAudio() {

        if (current.uiSource.isPlaying)
            current.uiSource.Stop();

        current.uiSource.clip = current.buttonAudio;
        //current.uiSource.PlayOneShot(current.buttonAudio);
        current.uiSource.Play();
    }

    public static void PlayCarretAudio() {

        if (current.uiSource.isPlaying)
            current.uiSource.Stop();

        current.uiSource.clip = current.carretAudio;
        current.uiSource.Play();
    }
}

public enum PlayerAudio {
    Step,
    Jump,
    Land,
    Death
}

public enum EffectAudio {
    Bonus,
    Penalty,
    SpawnPop,
    Spawn,
    Goal
}

public enum BGMType {
    Gameplay,
    Menu,
    Credits
}