using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int lives = 3;

    //public float maxHealth = 100;
   // public float currentHealth;

    public float playerSpeed;

    public float maxWeaponEnergy = 100;
    public float currentWeaponEnergy;

    public GameObject bullet;

    public Transform bulletSpawn;


    /// <summary>
    /// initialise health
    /// </summary>
	void Start () {
        //currentHealth = maxHealth;
	}


	/// <summary>
    /// check methods every frame
    /// </summary>
	void Update () {
        MovePlayer();
        FireGun();
	}

    /// <summary>
    /// takes away player lives.
    /// checks to see if there are any lives left, if not then its game over.
    /// </summary>
    void TakeDamage() {
        
        lives--;
        UIManager.Instance.SetLives(lives);
        if(lives <= 0) {
            GameFlowManager.Instance.Lose();
        }
    }

    /// <summary>
    /// Fires the gun based on the keycode spacebar input. Then Instantiates a bullet.
    /// </summary>
    void FireGun() {
        if (Input.GetKey(KeyCode.Space)) {
            currentWeaponEnergy = Mathf.MoveTowards(currentWeaponEnergy, maxWeaponEnergy, 1);
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            if(currentWeaponEnergy > 0) {
                Debug.Log("<color=orange>FIRE!</color>");
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

                currentWeaponEnergy = 0;
            }
        }
    }

    /// <summary>
    /// Moves the player based on the Horizontal axis input
    /// </summary>
    void MovePlayer() {
        float speed = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        transform.Translate(Vector3.right * speed);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "EnemyBullet") {
            
            TakeDamage();
            Instantiate(GameFlowManager.Instance.explosionParticles, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Enemy") {
            GameFlowManager.Instance.Lose();
        }
    }
}
