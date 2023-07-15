using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private float _speed = 5f;
    private Vector3 _direction;
    
    private float _timeToLive = 5f;
    private float _timeAlive = 0f;
    
    void Update()
    {
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
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
