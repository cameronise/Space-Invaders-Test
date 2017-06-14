using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour {

    public static GameFlowManager Instance { get; private set; }

    public GameRecord gameRecord;

    public GameObject player;

    public int score;
    public int currentRound = 0;

    public GameObject explosionParticles, trailParticles;

    void Awake() {
        if(Instance == null) {
            //Creates a static instance of this class so that it can be accessed globally.
            Instance = this;

            //Ensures that the gameObject this class is attached to isn't destroyed when transferring through scenes.
            DontDestroyOnLoad(gameObject);
        }
    }

	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Begin() {
        StartCoroutine(NewRound());
        /*EnemiesManager.Instance.SpawnEnemies();
        gameRecord.numberOfGamesPlayed = gameRecord.numberOfGamesPlayed++;*/
    }

    public void Win() {
        AddScore(100);
        gameRecord.wins++;
    }

    public void Lose() {
        gameRecord.losses++;
        JSONData.Instance.SaveData(gameRecord);
        StartCoroutine(LoseGame());
    }

    public void Reset() {
        currentRound = 0;
        score = 0;
        player.GetComponent<Player>().lives = 3;
        UIManager.Instance.SetLives(3);
        UIManager.Instance.SetRound(0);
        UIManager.Instance.SetScore(0);
        EnemiesManager.Instance.Purge();
        
    }

    public IEnumerator LoseGame() {
        UIManager.Instance.switchPage(UIManager.UIState.LOSEROUND);
        Reset();
        yield return new WaitForSeconds(2);
        UIManager.Instance.switchPage(UIManager.UIState.HOME);
        
    }

    public IEnumerator EndRound() {
        UIManager.Instance.switchPage(UIManager.UIState.ENDROUND);
        JSONData.Instance.SaveData(gameRecord);
        yield return new WaitForSeconds(5);
        StartCoroutine(NewRound());
    }

    public IEnumerator NewRound() {
        UIManager.Instance.switchPage(UIManager.UIState.NEWROUND);
        yield return new WaitForSeconds(5);
        currentRound++;
        UIManager.Instance.SetRound(currentRound);
        EnemiesManager.Instance.enemySpeed = currentRound * 2;
        EnemiesManager.Instance.enemyFirepowerDelay -= 0.2f;
        EnemiesManager.Instance.SpawnEnemies();
        UIManager.Instance.switchPage(UIManager.UIState.GAME);
    }

    public void AddScore(int s) {
        score += s;
        UIManager.Instance.SetScore(score);
        if(score > gameRecord.highScore) {
            gameRecord.highScore = score;
            JSONData.Instance.SaveData(gameRecord);
        }
    }
}


/// <summary>
/// Data Object which will be turned into a json file used to keep track of various game variable.
/// </summary>

[System.Serializable]
public class GameRecord {

    public int numberOfGamesPlayed;
    public int highScore;
    public int wins;
    public int losses;

    public GameRecord() {

    }

}

