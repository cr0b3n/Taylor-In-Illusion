using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class MainMenu : MonoBehaviour {

    [Header("Menu Section")]
    public RectTransform menuContainer;
    public GameObject creditsPanel;
    public GameObject guidePanel;

    private Vector3 desiredMenuposition = Vector3.zero;
    private bool hasChangePanelView;

    [Header("Transition Section")]
    public GameObject transitionIn;
    public GameObject transitionOut;
    public GameObject menuButtons;
    public float transitionTime = .6f;

    private bool isTransitioning;

    private void Start() {
        Time.timeScale = 1f;
        transitionIn.SetActive(true);
        isTransitioning = false;
        UpdateCreditsGuideVisibility(false, false);
        AudioManager.PlayBGM(BGMType.Menu);
    }

    private void Update() {
        if (menuContainer.anchoredPosition3D != desiredMenuposition)
            menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuposition, 0.1f);
        else if (hasChangePanelView) {
            hasChangePanelView = false;
            UpdateCreditsGuideVisibility(false, false);
        }
    }

    public void NavigateTo(int index) {

        if (isTransitioning) return;

        PlayButtonAudio();

        switch (index) {
            default:
            case 1:
                hasChangePanelView = true;
                desiredMenuposition = Vector3.zero;
                break;
            case 2: //credits
                UpdateCreditsGuideVisibility(false, true);
                desiredMenuposition = Vector3.left * 3000;
                break;
            case 3: //how to play               
                UpdateCreditsGuideVisibility(true, false);
                desiredMenuposition = Vector3.left * 1500;
                break;
        }
    }

    private void UpdateCreditsGuideVisibility(bool hasGuide, bool hasCredits) {
        guidePanel.SetActive(hasGuide);
        creditsPanel.SetActive(hasCredits);
    }

    //public void ShowCredits() {

    //    if (isTransitioning) return;
    //    PlayButtonAudio();
    //}

    //public void ShowGuide() {

    //    if (isTransitioning) return;
    //    PlayButtonAudio();
    //}

    public void PlayGame() {

        if (isTransitioning) return;

        PlayButtonAudio();
        menuButtons.SetActive(false);
        LoadScene(transitionTime, 1);
    }

    public void ExitGame() {
        PlayButtonAudio();
        Application.Quit();
    }

    private void PlayButtonAudio() {
        AudioManager.PlayUIAudio();
    }

    private void LoadScene(float loadDelay, int sceneIndex) {

        isTransitioning = true;
        transitionOut.SetActive(true);
        Time.timeScale = 0f;

        StartCoroutine(DelayedSceneActivation(loadDelay, sceneIndex));
    }

    private IEnumerator DelayedSceneActivation(float restartTime, int scenIndex) {

        yield return new WaitForSecondsRealtime(restartTime);

        SceneManager.LoadScene(scenIndex);
    }
}
