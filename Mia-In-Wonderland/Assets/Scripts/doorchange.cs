using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorchange : MonoBehaviour
{

    private void Start() {
        GetComponent<BoxCollider2D>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            GameManager.instance.winActive();
        }
    }
}
