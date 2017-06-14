using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public enum UIState {
        HOME,
        ENDROUND,
        NEWROUND,
        LOSEROUND,
        GAME
    }

    public UIState uiState = UIState.HOME;

    public static UIManager Instance {
        get; private set;
    }

    public GameObject homePage, endOfRoundPage, newRoundPage, losePage;
    public Text livesText, roundText, scoreText, highScoreText, maxRoundsText;

    void Awake() {
        if(Instance == null) {
            Instance = this;
        }
    }

    void Start() {
        switchPage(UIState.HOME);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// These methods set the text on canvas to the current player data
    /// </summary>
    /// <param name="v"></param>
    public void SetLives(int v) {
        livesText.text = v.ToString();
    }

    public void SetRound(int v) {
        roundText.text = v.ToString();
    }

    public void SetScore(int v) {
        scoreText.text = v.ToString();
    }


    public void SetHighScore(int v) {
        highScoreText.text = "Highscore: " + v.ToString();
    }

    public void SetMaxRounds(int v) {
        maxRoundsText.text = "Rounds played: " + v.ToString();
    }

    /// <summary>
    /// A quick solution for switching canvas pages
    /// </summary>
    /// 

    public void switchPage(UIState state) {
        uiState = state;
        switch (state) {
            case UIState.HOME:
                GoToHomePage();
                break;

            case UIState.ENDROUND:
                GoToEndRoundPage();
                break;

            case UIState.NEWROUND:
                GoToNewRoundPage();
                break;


            case UIState.LOSEROUND:
                GoToLosePage();
                break;

            case UIState.GAME:
                GoToGame();
                break;
        }

    }

    public void GoToEndRoundPage() {
        losePage.SetActive(false);
        homePage.SetActive(false);
        newRoundPage.SetActive(false);
        endOfRoundPage.SetActive(true);
    }

    public void GoToNewRoundPage() {
        losePage.SetActive(false);
        homePage.SetActive(false);
        newRoundPage.SetActive(true);
        endOfRoundPage.SetActive(false);
    }

    public void GoToHomePage() {
        losePage.SetActive(false);
        homePage.SetActive(true);
        newRoundPage.SetActive(false);
        endOfRoundPage.SetActive(false);
    }

    public void GoToLosePage() {
        losePage.SetActive(true);
        homePage.SetActive(false);
        newRoundPage.SetActive(false);
        endOfRoundPage.SetActive(false);
    }

    public void GoToGame() {
        losePage.SetActive(false);
        homePage.SetActive(false);
        newRoundPage.SetActive(false);
        endOfRoundPage.SetActive(false);
    }
}
