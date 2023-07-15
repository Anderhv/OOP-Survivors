using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCapsule : Enemy
{
    private float _amplitude;
    private float _frequency = 1f;
    
    public float movementDuration = 10f;
    private float _startTime;
    protected override void OnInitialize()
    {
        _startTime = Time.time;
        _amplitude = Random.Range(2.0f, 5.0f) * (Random.value > 0.5f ? 1f : -1f);
        _frequency = Random.Range(0.5f, 3.0f);
    }
    protected override void MoveTowardsPlayer()
    {
        // Move in a sine-wave motion towards player
        
        var vectorTowardsPlayer = (_player.transform.position - transform.position).normalized;
        var vectorTowardsPlayerNormal = new Vector3(vectorTowardsPlayer.z, 0f, -vectorTowardsPlayer.x);
        
        // Calculate the sinusoidal motion based on time and movement parameters
        float elapsedTime = Time.time - _startTime;
        float sinusoidalValue = _amplitude * Mathf.Sin(2f * Mathf.PI * _frequency * elapsedTime / movementDuration);
        Vector3 movement = vectorTowardsPlayer + (sinusoidalValue * vectorTowardsPlayerNormal);

        // Update position
        transform.position += movement * speed * Time.deltaTime;
    }
}
