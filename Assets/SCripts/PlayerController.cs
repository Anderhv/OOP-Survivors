using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float _health { get; set; }
    private float _speed = 1.0f;

    public float xMax = 8.0f;
    public float zMax = 4.0f;

    private GameManager _gm;

    public GameObject bulletPrefab;

    private Slider _healthBarSlider;

    private AudioSource _audio;
    public AudioClip shootSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0.3f, 0);
        _health = maxHealth;
        _audio = GetComponent<AudioSource>();
        _gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        _healthBarSlider = GameObject.FindWithTag("HealthBar").GetComponent<Slider>();
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
        var newPosition = transform.position + movementDirection * _speed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -xMax, xMax);
        newPosition.z = Mathf.Clamp(newPosition.z, -zMax, zMax);
        transform.position = newPosition;
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
        _health = Mathf.Max(0, _health - damage);
        _healthBarSlider.value = _health / maxHealth;
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
