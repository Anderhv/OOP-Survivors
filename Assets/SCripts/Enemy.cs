using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float attackRange = 1f;
    public float attackDamage = 1f;
    
    public EnemyTypes enemyType;
    public enum EnemyTypes
    {
        Ball,
        Cube,
        Snake
    }
    
    protected GameObject _player;
    protected PlayerController _playerController;
    
    protected GameManager _gm;
    
    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        OnInitialize();
    }

    // Update is called once per frame
    private void Update()
    {
        MoveTowardsPlayer();
        transform.LookAt(_player.transform.position);
        
    }

    protected virtual void OnInitialize() { }

    public float GetScore()
    {
        switch (enemyType)
        {
            case EnemyTypes.Cube:
                return 10f;
            case EnemyTypes.Snake:
                return 15f;
            default:
                return 5f;
        }
    }
    
    /*private bool InRangeToAttackPlayer()
    {
        // Check if in range to attack player
        if (Vector2.Distance(
                new Vector2(_player.transform.position.x, _player.transform.position.z), 
                new Vector2(transform.position.x, transform.position.z)
            ) <= attackRange)
        {
            return true;
        }
        return false;
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AttackPlayer();
        }
    }

    protected abstract void MoveTowardsPlayer();

    private void AttackPlayer()
    {
        // Attack player
        _playerController.TakeDamage(attackDamage);
        // Destroy self
        Destroy(gameObject);
    }
}
