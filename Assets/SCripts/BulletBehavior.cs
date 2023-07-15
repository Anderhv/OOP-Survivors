using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private float _speed = 5f;
    private Vector3 _direction;
    
    private readonly float _timeToLive = 5f;
    private float _timeAlive = 0f;

    private bool _isActive = true;
    private MeshRenderer _meshRenderer;
    
    private AudioSource _audioSource;
    
    private GameManager _gm;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
    
    void Update()
    {
        if (!_isActive)
        {
            return;
        }
        
        // Move in direction
        transform.position += _direction * _speed * Time.deltaTime;
        
        // Destroy after time to live
        _timeAlive += Time.deltaTime;
        if (_timeAlive > _timeToLive)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetDirection(Vector3 direction)
    {
        _direction = direction.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive)
        {
            return;
        }
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            _audioSource.Play();
            Destroy(other.gameObject);
            StartCoroutine(DestroyAfterDelay());
            var enemy = other.GetComponent<Enemy>();
            if (enemy is not null)
            {
                _gm.IncreaseScore(enemy.GetScore());
            }
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        _isActive = false;
        _meshRenderer.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
