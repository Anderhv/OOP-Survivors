using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float _health { get; set; }

    private GameManager _gm;

    public GameObject bulletPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;
        _gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();        
    }

    private void Update()
    {
        // Shoot on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Shoot a bullet from the player to the mouse position
        var mousePosition = Input.mousePosition;
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.y = 0.3f;
        var direction = (mouseWorldPosition - transform.position).normalized;
        var bullet = Instantiate(bulletPrefab, transform.position + direction * 0.5f, Quaternion.identity);
        var bulletBehavior = bullet.GetComponent<BulletBehavior>();
        bulletBehavior.SetDirection(direction);
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
