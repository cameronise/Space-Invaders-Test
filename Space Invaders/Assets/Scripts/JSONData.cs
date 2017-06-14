using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONData : MonoBehaviour {

    public static JSONData Instance { get; private set; }

    public string path;

    void Awake() {

        if(Instance == null) {
            Instance = this;
        }

        path = Application.dataPath + "/Data/gameRecord.json";
    }

	void Start () {
        InitialiseData();
	}

    /// <summary>
    /// Check if data already exists, if not create new json file
    /// </summary>
    void InitialiseData() {
        if (DataExists()) {
            string data = File.ReadAllText(path);
            GameRecord gr = JsonUtility.FromJson<GameRecord>(data);
            GameFlowManager.Instance.gameRecord = gr;

            UIManager.Instance.SetHighScore(gr.highScore);
            UIManager.Instance.SetMaxRounds(gr.wins);
        } else {
            //File.Create(path);
            GameRecord gr = new GameRecord();
            SaveData(gr);

            UIManager.Instance.SetHighScore(0);
            UIManager.Instance.SetMaxRounds(0);

        }
    }

    public void SaveData(GameRecord gr) {
        string data = JsonUtility.ToJson(gr);
        File.WriteAllText(path, data);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    bool DataExists() {
        if(File.Exists(path)){
            return true;
        } else {
            return false;
        }
    }

}
