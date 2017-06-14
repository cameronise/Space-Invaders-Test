using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int enemyLevel = 1;

    public float maxHealth;
    public float currentHealth;

    bool dead = false;

    public GameObject bullet;
    public Transform bulletSpawn;

	// Use this for initialization
	void Start () {
        currentHealth = enemyLevel;
	}
	
	// Update is called once per frame
	void Update () {
        if (!dead) {
            MoveEnemy();
        }
	}

    void MoveEnemy() {
        transform.Translate(Vector3.right * EnemiesManager.Instance.enemySpeed * Time.deltaTime);

        if(transform.position.x > 19f) {
            EnemiesManager.Instance.SwitchDirection(EnemiesManager.MoveDirection.LEFT);
        }else if(transform.position.x < -19f) {
            EnemiesManager.Instance.SwitchDirection(EnemiesManager.MoveDirection.RIGHT);
        }
    }

    /// <summary>
    /// takes off specific health and also checks if alien survived or not.
    /// </summary>
    /// <param name="Amount"></param>
    void TakeDamage(float Amount) {
        currentHealth -= Amount;
        if (currentHealth <= 0) {
            StartCoroutine(Death());
        }
    }

    /// <summary>
    /// Controlled by the enemiesManager to make a random enemy fire their gun.
    /// </summary>
    public void FireGun() {
        Debug.Log("<color=orange>ENEMY FIRED!</color>");
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
    }

    /// <summary>
    /// This coroutine deals with physics based death animation of aliens as well as the gameObject destruction
    /// </summary>
    /// <returns></returns>
    IEnumerator Death() {
        dead = true;
        EnemiesManager.Instance.RemoveEnemy(gameObject);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 1);

        float rx = Random.Range(-1, 1);
        float ry = Random.Range(-1, 1);
        float rz = Random.Range(-1, 1);

        float rt = Random.Range(-250, 250);
        GetComponent<Rigidbody>().AddTorque(new Vector3(rx, ry, rz) * rt);

        GetComponent<BoxCollider>().enabled = false;

        GameFlowManager.Instance.AddScore(10);

        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }


    /// <summary>
    /// Deals with all trigger collisions
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Bullet") {
            Bullet b = other.gameObject.GetComponent<Bullet>();
            Instantiate(GameFlowManager.Instance.explosionParticles, other.transform.position, other.transform.rotation);
            Instantiate(GameFlowManager.Instance.trailParticles, transform);
            TakeDamage(b.energy);
            Destroy(other.gameObject);
        }
    }
}
