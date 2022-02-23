using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class playerController : MonoBehaviour
{
    public UnityAction<int> m_clip; 
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpPower = 10f;
    private Rigidbody2D _rb;
    private Animator _anim;
    private int _jumpTime = 0;
    private int _maxJumpTime = 2;
    public bool OnStun = false;
    public float OnStunTimer = 0.0f;
    private float _stunWhenAttacked = 1.5f;
    public bool attack = true;
    public static playerController instance;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (!OnStun && !startCutscene.instance.isCutsceneOn && !GameManager._isPause)
        {
            if (Input.GetKey(KeyCode.D))
            {
                _rb.velocity = new Vector2(_speed, _rb.velocity.y);
                transform.eulerAngles = new Vector3(0, 0, 0);
                _anim.SetBool("Walk", true);
                if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
                {
                    _rb.velocity = new Vector2(_speed * 2f, _rb.velocity.y);
                    _anim.SetBool("Run", true);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                _rb.velocity = new Vector2(-_speed, _rb.velocity.y);
                transform.eulerAngles = new Vector3(0, 180, 0);
                _anim.SetBool("Walk", true);
                if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
                {
                    _rb.velocity = new Vector2(-_speed * 2f, _rb.velocity.y);
                    _anim.SetBool("Run", true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_jumpTime < _maxJumpTime)
                {
                    m_clip?.Invoke(1);
                    _rb.velocity = Vector2.up * _jumpPower;
                    _anim.SetBool("Jump", true);
                    _jumpTime = _jumpTime + 1;
                }
            }

        }
        if ((Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false))
        {
            _anim.SetBool("Walk", false);
        }
        if (Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.LeftShift) == false)
        {
            _anim.SetBool("Run", false);
        }
        else if (Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.LeftShift) == false)
        {
            _anim.SetBool("Run", false);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _anim.SetBool("Jump", false);
        }
        update_stun();
        CheckFalling();
        ReadSign();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Vector3 playerPosition = transform.position;
            Vector3 groundPosition = collision.gameObject.transform.position;
            if (playerPosition.y > groundPosition.y)
            {
                _anim.SetBool("Jump", false);
                _jumpTime = 0;
            }
        }
        if (collision.gameObject.tag == "Enemy")
        {
            enemyController enemyController = collision.gameObject.GetComponent<enemyController>();
            Vector3 player = transform.position;
            Vector3 enemy = enemyController.transform.position;
            if (enemyController.OnStun == false && attack == true)
            {
                attack = false;
                if (player.y >= (enemy.y + 1.3f))
                {
                    if (OnStun == false)
                    {
                        _rb.AddForce(new Vector2(0, 200));
                        enemyController.enemy_UpdateHealth(3, gameObject);
                        m_clip?.Invoke(2);
                    }
                }
            }
            attack = true;
        }

    }
    void update_stun()
    {
        if (OnStun == true)
        {
            _anim.SetBool("Walk", false);
            _anim.SetBool("Run", false);
            OnStunTimer += Time.deltaTime;
            if (OnStunTimer >= _stunWhenAttacked)
            {
                OnStun = false;
                playerHealth.instance.TakeDamage = false;
                _anim.SetBool("Hurt", false);
                OnStunTimer = 0.0f;
            }
        }
    }
    void CheckFalling()
    {
        if (gameObject.transform.position.y < -15.0f)
        {
            playerHealth.instance.playerUpdateHealth(100, gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            coin.instance.addCoin();
            Destroy(other.gameObject);
            m_clip?.Invoke(4);
        }
    }
    private void ReadSign()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rb.position , new Vector2(1,0), 0.5f, LayerMask.GetMask("Sign"));
        if (hit.collider != null)
        {
            readSign read = hit.collider.GetComponent<readSign>();
             if (read != null)
                {
                    read.DisplayDialog();
                }
        }
    }
}
