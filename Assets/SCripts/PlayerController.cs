using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float _health { get; set; }
    private float _speed = 1.0f;

    private GameManager _gm;

    public GameObject bulletPrefab;

    private AudioSource _audio;
    public AudioClip shootSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    
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
        HandleMovementInput();
        HandleMouseInput();
    }
    
    private void HandleMovementInput()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        transform.position += movementDirection * _speed * Time.deltaTime;
    }

    private void HandleMouseInput()
    {
        // Rotate player towards mouse position
        var mousePosition = Input.mousePosition;
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.y = 0.3f;
        transform.LookAt(mouseWorldPosition);
        
        // Shoot on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(mouseWorldPosition);
        }
    }

    private void Shoot(Vector3 mouseWorldPosition)
    {
        // Shoot a bullet from the player towards the mouse position
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
        _audio.PlayOneShot(hurtSound, 1f);
        if (PlayerIsDead())
        {
            _audio.PlayOneShot(deathSound, 0.5f);
            _gm.EndGame();
        }
    }

    private bool PlayerIsDead()
    {
        if (_health <= 0)
        {
            return true;
        }
        return false;
    }
}
