using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField] private enemyStatSO _stats;
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private int _attack;
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private Animator _anim;
    private bool _stunWhenAttackPlayer = false;
    private float _delay = 2.0f;
    public bool OnStun = false;
    public float OnStunTimer = 0.0f;
    private float _afterDamagedTimer = 0.0f;
    private float _whenAttacked = 1.0f;
    public static enemyController instance;
    public bool _moveright;
    private float _movement = 1f;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        _health = _stats.Health;
        _speed = _stats.Speed;
        _attack = _stats.Attack;
    }
    void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _sprite = gameObject.GetComponent<SpriteRenderer>();
        _stats = (enemyStatSO)ScriptableObject.CreateInstance("enemyStatSO");
        // GetComponent<enemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_stunWhenAttackPlayer == false)
        {
            moving();
        }
        else
        {
            _afterDamagedTimer += Time.deltaTime;
            if (_afterDamagedTimer >= _delay)
            {
                _stunWhenAttackPlayer = false;
                _afterDamagedTimer = 0.0f;
            }
        }
        update_stun();
        CheckFaliing();
    }

    void moving()
    {
        if (OnStun == false)
        {
            if (_moveright)
            {
                transform.Translate(_movement * _speed * Time.deltaTime, 0, 0);
                _sprite.flipX = false;
            }
            else
            {
                transform.Translate(-_movement * _speed * Time.deltaTime, 0, 0);
                _sprite.flipX = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Turn")
        {
            if (_moveright)
            {
                _moveright = false;
            }
            else
            {
                _moveright = true;
            }

        }
    }
    void update_stun()
    {
        if (OnStun == true)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            OnStunTimer += Time.deltaTime;
            if (OnStunTimer >= _whenAttacked)
            {
                GetComponent<CapsuleCollider2D>().enabled = true;
                OnStun = false;
                OnStunTimer = 0.0f;
                _anim.SetBool("Hurt", false);
            }
        }
    }
    public void enemy_UpdateHealth(int damage, GameObject fromobject)
    {
        _health /= damage;
        // _stats._health /= damage;
        Vector3 enemy = gameObject.transform.position;
        Vector3 player = fromobject.gameObject.transform.position;
        if (player.y >= (enemy.y + 1.3f))
        {
            _rb.isKinematic = true;
            _rb.constraints = RigidbodyConstraints2D.FreezePosition;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            OnStun = true;
            OnStunTimer = 0.0f;
            _anim.SetBool("Hurt", true);
        }
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void CheckFaliing()
    {
         if (gameObject.transform.position.y < -15.0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            playerHealth playerHealth = target.gameObject.GetComponent<playerHealth>();
            Vector3 enemy = gameObject.transform.position;
            Vector3 player = playerHealth.transform.position;
            if ((player.y - 1.3f) < enemy.y && playerHealth.instance.TakeDamage == false)
            {
                _stunWhenAttackPlayer = true;
                playerHealth.playerUpdateHealth(_attack, gameObject);
            }
        }
    }

}
