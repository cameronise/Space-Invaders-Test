using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {

    public static EnemiesManager Instance { get; private set; }

    /// <summary>
    /// alien prefab for to spawn
    /// </summary>
    public GameObject alien;

    public float enemySpeed;
    public float enemyFirepowerDelay = 5;

    public List<GameObject> aliens = new List<GameObject>();


    public int enemyStartCount = 20;
    public float startX = -12;
    public float startY = 10.8f;

    public enum MoveDirection {
        RIGHT,
        LEFT
    }

    public MoveDirection moveDirection = MoveDirection.RIGHT;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void EnemyFirepower() {
        if(aliens.Count > 0) {
            if(enemyFirepowerDelay < 0.3f) {
                enemyFirepowerDelay = 0.3f;
            }

            StartCoroutine(FireEnemyGuns());
        }
    }

    IEnumerator FireEnemyGuns() {
        Debug.Log("enemy firepower routine");
        yield return new WaitForSeconds(enemyFirepowerDelay);
        if(aliens.Count > 0) {
            int r = (int)Random.Range(0, aliens.Count - 1);
            aliens[r].GetComponent<Enemy>().FireGun();
            EnemyFirepower();
        }
        

    }

    /// <summary>
    /// shifts all aliens down one level.
    /// </summary>
    void ShiftAliensDown() {
        foreach (GameObject go in aliens) {
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y - 0.5f, go.transform.position.z);
        }
    }


    /// <summary>
    /// public Switch function called by an alien when they cross a boundary to switch the direction of all aliens individual movement
    /// </summary>
    /// <param name="d"></param>
    public void SwitchDirection(MoveDirection d) {
        switch (d) {
            case MoveDirection.RIGHT:
                ShiftAliensDown();
                enemySpeed = Mathf.Abs(enemySpeed);
                break;

            case MoveDirection.LEFT:
                ShiftAliensDown();
                enemySpeed = -enemySpeed;
                break;
        }
    }

    //Removes dead enemy from current list.
    public void RemoveEnemy(GameObject enemy) {
        aliens.Remove(enemy);

        //Checks to see if all enemies have been destroyed.
        if(aliens.Count == 0) {
            StartCoroutine(GameFlowManager.Instance.EndRound());
            GameFlowManager.Instance.Win();
        }
    }

    /// <summary>
    /// A method for resetting the game
    /// </summary>
    public void Purge() {
        if(aliens.Count > 0) {
            foreach(GameObject go in aliens) {
                Destroy(go);
            }
        }
        aliens.Clear();
    }

    /// <summary>
    /// Spawns Enemies in a grid. The start coordinate is the top left spawn point.
    /// </summary>
    public void SpawnEnemies() {

        float x = startX;
        float y = startY;

        for(int i = 0; i < enemyStartCount; i++) {
            GameObject a = Instantiate(alien, new Vector3(x, y, 0), Quaternion.identity);
            aliens.Add(a);
            x += 2;
            if(x > 12) {
                x = startX;
                y -= 1;
            }

        }

        EnemyFirepower();
    }
}
