using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EffectManager : MonoBehaviour {

    #region /Singleton

    public static EffectManager instance;

    private void Awake() {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public ObjectPooler footStepPool;
    public ObjectPooler landJumpPool;
    public ObjectPooler badPickEffectPool;
    public ObjectPooler goodPickEffectPool;
    public GameObject deathEffect;
    public GameObject spawnEffect;
    public GameObject goalEffect;

    private void Start() {
        spawnEffect.SetActive(false);
        deathEffect.SetActive(false);
    }

    public void ShowPlayerSpawnEffect(Vector3 pos) {
        //Debug.Log("Showing player spawing effect");
        spawnEffect.SetActive(false);
        spawnEffect.transform.position = pos;
        spawnEffect.SetActive(true);
    }

    public void ShowDeathEffect(Vector3 pos) {
        //Debug.Log("Showing death Effect");
        deathEffect.SetActive(false);
        deathEffect.transform.position = pos;
        deathEffect.SetActive(true);
    }

    public void ShowGoalEffect(Vector3 pos) {
        //Instantiate(goalEffect, pos, Quaternion.identity);
        //Debug.Log("Showing goal Effect");
    }

    public void ShowPickUpEffect(bool isGood, Vector3 pos) {
        //Debug.Log("Showing Pick Effect");
        if (isGood)
            goodPickEffectPool.GetPooledObject(pos, Quaternion.identity);
        else
            badPickEffectPool.GetPooledObject(pos, Quaternion.identity);
    }

    public void FootStepEffect(Vector3 pos) {
        //Debug.Log("Showing Footstep Effect");
        footStepPool.GetPooledObject(pos, Quaternion.identity);
    }

    public void ShowJumpLandEffect(Vector3 pos) {
        //Debug.Log("Showing jump Effect");
        landJumpPool.GetPooledObject(pos, Quaternion.identity);
    }
}
