using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gimmickControl : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private Animator Anim_door;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _anim.SetTrigger("Open");
            StartCoroutine("cutScene");
        }
        else
        {
            _anim.ResetTrigger("Open");
            _anim.ResetTrigger("Open_door");
        }
    }

    IEnumerator cutScene()
    {
        yield return new WaitForSeconds(1f);
        startCutscene.instance.StartcutScene();
        yield return new WaitForSeconds(3f);
        Anim_door.SetTrigger("Open_door");
        Anim_door.GetComponent<BoxCollider2D>().enabled = true;
        startCutscene.instance.StopcutScene();
    }
}
