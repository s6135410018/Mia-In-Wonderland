using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startCutscene : MonoBehaviour
{
    [SerializeField] private Animator Anim;
    public bool isCutsceneOn;
    public static startCutscene instance;
    private void Start()
    {
        instance = this;
    }
    // Start is called before the first frame update
    public void StartcutScene()
    {
        isCutsceneOn = true;
        Anim.SetBool("cutscene1", true);
    }

    public void StopcutScene()
    {
        isCutsceneOn = false;
        Anim.SetBool("cutscene1", false);
        Destroy(GetComponent<BoxCollider2D>());
    }
}
