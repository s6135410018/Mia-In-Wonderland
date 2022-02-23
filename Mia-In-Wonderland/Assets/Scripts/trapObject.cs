using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            playerHealth playerHealth = other.gameObject.GetComponent<playerHealth>();
            if (playerHealth.instance.TakeDamage == false)
            {
                playerHealth.playerUpdateHealth(5, gameObject);
            }
        }
    }

    // Update is called once per frame

}
