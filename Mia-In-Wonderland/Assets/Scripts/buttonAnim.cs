using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonAnim : MonoBehaviour
{
    public void Onpress() {
        GetComponent<Animator>().Play("button_press");
    }

    
}
