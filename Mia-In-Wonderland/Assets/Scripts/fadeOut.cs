using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeOut : MonoBehaviour
{
    private bool _fadeOut = false;
    private bool _fadeIn = true;
    // Start is called before the first frame update

    void FadeOut()
    {
        if (_fadeOut == true)
        {
            Component[] _color = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer c in _color)
            {
                c.color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
    }

    void FadeIn()
    {
        if (_fadeIn == true)
        {
            Component[] _color = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer c in _color)
            {
                c.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _fadeOut = true;
            FadeOut();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _fadeIn = true;
            FadeIn();
        }
    }
}
