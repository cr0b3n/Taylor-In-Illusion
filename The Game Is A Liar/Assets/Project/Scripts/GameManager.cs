using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour {

    #region /Singleton

    public static GameManager instance;

    private void Awake() {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public Revealer revealer;
    public Transform currentSavePoint;
    public Player player;
    public GameObject goodGraphic;
    public GameObject badGraphic;

    private int deathCount;

    private void Start() {
        deathCount = 0;
        revealer.gameObject.SetActive(true);
        Time.timeScale = 1f;
        AudioManager.PlayBGM(BGMType.Gameplay);
    }

#if UNITY_EDITOR
    private void Update() {

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            revealer.ResetRevealer();
        }
    }
#endif

    public void Respawn() {
        UpdateDeathInfo();
        revealer.gameObject.SetActive(true);
        player.RespawnRequirement();
        player.transform.position = currentSavePoint.position;
        revealer.ResetRevealer();
        EffectManager.instance.ShowPlayerSpawnEffect(new Vector3(currentSavePoint.position.x, currentSavePoint.position.y + 1f));
        FindObjectOfType<CameraConfiner>()?.CheckPlayerTransform();
    }

    public void ActivatePickUpBonus(float bonusTime) {
        revealer.gameObject.SetActive(true);
        revealer.ActivateBonusTime(bonusTime);
    }

    public void GoalAchieved() {
        revealer.gameObject.SetActive(true);
        revealer.StopActivity();
        AudioManager.PlayBGM(BGMType.Credits);
        GUIManager.instance.UpdateToGoalUI();
    }

    public void UpdateDeathInfo() {
        deathCount++;
        GUIManager.instance.UpdateDeathCountDisplay(deathCount);
    }

    public GameObject GetAltGraphic(bool isGood, Transform parent) {

        if (isGood)
            return Instantiate(badGraphic, parent.position, parent.rotation, parent);
        else
            return Instantiate(goodGraphic, parent.position, parent.rotation, parent);
    }
}
