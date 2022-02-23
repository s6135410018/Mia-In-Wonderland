using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    [SerializeField] public int _currentHealth;
    [SerializeField] public GameObject[] _healthIcon;
    public bool TakeDamage = false;
    private int _maxHealth = 15;
    private Rigidbody2D _rb;
    private Animator _anim;
    public UnityAction<int> m_clip;

    public static playerHealth instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentHealth = _maxHealth;
        _anim = GetComponent<Animator>();
    }
    public void playerUpdateHealth(int damage, GameObject fromobject)
    {
        _currentHealth -= damage;
        Vector3 currentLocation = gameObject.transform.position;
        Vector3 LocationFrom = fromobject.gameObject.transform.position;

        if (enemyController.instance.OnStun == false && TakeDamage == false)
        {
            if (currentLocation.x > LocationFrom.x)
            {
                _rb.AddForce(new Vector2(300, 500));
                _anim.SetBool("Hurt", true);
                m_clip?.Invoke(5);
            }
            else
            {
                _rb.AddForce(new Vector2(-300, 500));
                _anim.SetBool("Hurt", true);
                m_clip?.Invoke(5);
            }
            if (currentLocation.y < LocationFrom.y)
            {
                _rb.AddForce(new Vector2(-300, 500));
                _anim.SetBool("Hurt", true);
                m_clip?.Invoke(5);
            }
        }
        playerController.instance.OnStun = true;
        TakeDamage = true;
        playerController.instance.OnStunTimer = 0.0f;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _anim.SetBool("Hurt", false);
            _anim.SetTrigger("Die");
            StartCoroutine("wait");
        }
        int numberOfHealthToShow = _currentHealth / 5;
        for (int index = 0; index < _healthIcon.Length; index++)
        {
            if (index < numberOfHealthToShow)
            {
                _healthIcon[index].SetActive(true);
            }
            else
            {
                _healthIcon[index].SetActive(false);
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.active();
    }

}
