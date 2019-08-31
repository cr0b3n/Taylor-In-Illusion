using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class GUIManager : MonoBehaviour {

    #region /Singleton

    public static GUIManager instance;

    private void Awake() {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion


    public TextMeshProUGUI deathCountText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI progressText;
    public Image progressFill;
    public float goalXOffset;
    public float goalXPosition = 10f;
    public GameObject menu;
    public GameObject caretDownButton;
    public GameObject caretUpButton;
    public GameObject transitionIn;
    public GameObject transitionOut;
    public GameObject creditsPanel;
    public float creditTime = 25f;

    private Transform playerTransform;
    private float gameTime = 0f;
    private bool isActive;
    private bool isTransitioning;

    private void Start() {
        isTransitioning = false;
        goalXPosition += goalXOffset;
        isActive = true;
        playerTransform = GameManager.instance.player.transform;
        transitionIn.SetActive(true);
        transitionOut.SetActive(false);
        UpdateMenuVisibility(false, true, false);
        creditsPanel.SetActive(false);
    }

    private void LateUpdate() {

        if (!isActive)
            return;

        SetDynamicUI();
    }

    private void SetDynamicUI() {
        //Set Timer Display
        gameTime += Time.deltaTime;
        timerText.text = TimeSpan.FromSeconds(gameTime).ToString(@"hh\:mm\:ss");

        //Set Goal Display
        float progress = (playerTransform.position.x + goalXOffset) / goalXPosition;
        progressFill.fillAmount = progress;
        progressText.text = string.Format("Goal: {0:N}%", Mathf.Clamp(progress * 100f, 0f, 100f));
    }

    private void UpdateMenuVisibility(bool hasMenu, bool hasCaretDown, bool hasCaretUp) {
        menu.SetActive(hasMenu);
        caretDownButton.SetActive(hasCaretDown);
        caretUpButton.SetActive(hasCaretUp);
    }

    public void UpdateToGoalUI() {
        UpdateMenuVisibility(false, false, false);
        creditsPanel.SetActive(true);
        isActive = false;
        isTransitioning = true;
        Invoke("DisableButtonTransition", creditTime);
    }

    private void DisableButtonTransition() {
        isTransitioning = false;
    }

    public void ShowHideMenu() {

        if (!isActive) return;

        AudioManager.PlayCarretAudio();
        //PlayButtonAudio();
        UpdateMenuVisibility(!menu.activeSelf, !caretDownButton.activeSelf, !caretUpButton.activeSelf);
    }

    public void UpdateDeathCountDisplay(int amount) {
        deathCountText.gameObject.SetActive(false);
        deathCountText.text = "X" + amount.ToString();
        deathCountText.gameObject.SetActive(true);
    }

    public void GoToHome() {

        if (isTransitioning) return;

        PlayButtonAudio();
        LoadScene(0.7f, 0);
    }

    public void RestartGame() {

        if (isTransitioning) return;

        PlayButtonAudio();
        LoadScene(0.7f, SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadScene(float loadDelay, int sceneIndex) {

        isTransitioning = true;
        UpdateMenuVisibility(false, false, false);
        transitionOut.SetActive(true);
        Time.timeScale = 0f;

        StartCoroutine(DelayedSceneActivation(loadDelay, sceneIndex));
    }

    private IEnumerator DelayedSceneActivation(float restartTime, int scenIndex) {

        yield return new WaitForSecondsRealtime(restartTime);

        SceneManager.LoadScene(scenIndex);
    }

    public void BackToCheckPoint() {
        GameManager.instance.Respawn();
    }

    private void PlayButtonAudio() {
        AudioManager.PlayUIAudio();
    }
}
