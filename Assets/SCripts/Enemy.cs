using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float attackRange = 1f;
    public float attackDamage = 1f;
    
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
        if (!InRangeToAttackPlayer())
        {
            MoveTowardsPlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    protected virtual void OnInitialize() { }
    
    private bool InRangeToAttackPlayer()
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
    }

    protected abstract void MoveTowardsPlayer();

    protected virtual void AttackPlayer()
    {
        // Attack player
        _playerController.TakeDamage(attackDamage);
        // Destroy self
        Destroy(gameObject);
    }
}
