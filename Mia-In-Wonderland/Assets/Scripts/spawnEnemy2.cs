using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy2 : MonoBehaviour
{
[SerializeField] private GameObject _spawn;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _check;
    private GameObject clonePrefab;
    private bool complete = false;

    private void Start() {
    }
    private void FixedUpdate()
    {
        RaycastHit2D _hit2D;
        float _range = 3f;
        _hit2D = Physics2D.Raycast(_check.transform.position, _check.transform.TransformDirection(Vector3.left), _range, LayerMask.GetMask("Player"));

        if (_hit2D.collider != null)
        {
            if (clonePrefab == null && complete == false)
            {
                clonePrefab = Instantiate(_enemy, _spawn.transform.position, _spawn.transform.rotation);
                _enemy.SetActive(true);
                complete = true;
            }
        }

    }
}

