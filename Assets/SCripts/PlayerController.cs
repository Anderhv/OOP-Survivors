using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float _health { get; set; }

    private GameManager _gm;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;
        _gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();        
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        EndGameIfDead();
    }

    private bool EndGameIfDead()
    {
        Debug.Log(_health);
        if (_health <= 0)
        {
            _gm.EndGame();
            return true;
        }
        return false;
    }
}
