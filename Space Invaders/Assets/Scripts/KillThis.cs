using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillThis : MonoBehaviour {

    /// <summary>
    /// Time set for gameObject to self destruct;
    /// </summary>
    public float killTime = 5;

	void Start () {
        StartCoroutine(DestroyGameObject());
	}
	
    /// <summary>
    /// Self Destruct coroutine.
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyGameObject() {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
