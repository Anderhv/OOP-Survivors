using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float _health { get; set; }

    private GameManager _gm;

    public GameObject bulletPrefab;

    private AudioSource _audio;
    public AudioClip shootSound;
    public AudioClip hurtSound;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;
        _audio = GetComponent<AudioSource>();
        _gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();        
    }

    private void Update()
    {
        if (_gm.isGameOver)
        {
            return;
        }
        // Shoot on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Shoot a bullet from the player towards the mouse position
        var mousePosition = Input.mousePosition;
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.y = 0.3f;
        var direction = (mouseWorldPosition - transform.position).normalized;
        var bullet = Instantiate(bulletPrefab, transform.position + direction * 0.5f, Quaternion.identity);
        var bulletBehavior = bullet.GetComponent<BulletBehavior>();
        bulletBehavior.SetDirection(direction);
        // Play audio
        _audio.PlayOneShot(shootSound);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _audio.PlayOneShot(hurtSound, 0.2f);
        EndGameIfDead();
    }

    private bool EndGameIfDead()
    {
        if (_health <= 0)
        {
            _gm.EndGame();
            return true;
        }
        return false;
    }
}
