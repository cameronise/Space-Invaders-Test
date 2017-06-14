using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float energy = 1;

    float speed = 20;

    public enum OWNER {
        PLAYER,
        ALIEN
    }

    public OWNER owner;

    void Start() {

    }

	void Update() {
        SendBullet();
    }

    void SendBullet() {
        if(owner == OWNER.PLAYER) {
            transform.Translate(transform.up * speed * Time.deltaTime);
        } else {
            transform.Translate(-transform.up * speed * Time.deltaTime);
        }
        
    }
}
