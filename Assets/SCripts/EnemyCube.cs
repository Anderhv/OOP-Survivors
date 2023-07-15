using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCube : Enemy
{
    private float _minimumTimeInSameDirection = 1f;
    private float _timeInSameDirection = 10.0f;

    private bool _movingHorizontally = false;
    
    protected override void MoveTowardsPlayer()
    {
        _timeInSameDirection += Time.deltaTime;
        if (_timeInSameDirection < _minimumTimeInSameDirection)
        {
            // Keep moving in same direction
            if (_movingHorizontally)
            {
                MoveHorizontally();
            }
            else
            {
                MoveVertically();
            }
            return;
        }
        // Otherwise, change direction
        if (_movingHorizontally)
        {
            MoveVertically();
            _movingHorizontally = false;
            _timeInSameDirection = 0f;
        }
        else
        {
            MoveHorizontally();
            _movingHorizontally = true;
            _timeInSameDirection = 0f;
        }
    }

    private void MoveHorizontally()
    {
        // Move left/right
        var playerPosition = _player.transform.position;
        var playerX = playerPosition.x;
        var myPosition = transform.position;
        var myX = myPosition.x;
        var myZ = myPosition.z;
        if (playerX > myX)
        {
            // Move right
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(playerX, 0.3f, myZ),
                speed * Time.deltaTime
            );
        }
        else
        {
            // Move left
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(playerX, 0.3f, myZ),
                speed * Time.deltaTime
            );
        }
    }

    private void MoveVertically()
    {
        // Move up/down
        var playerPosition = _player.transform.position;
        var playerZ = playerPosition.z;
        var myPosition = transform.position;
        var myX = myPosition.x;
        var myZ = myPosition.z;
        if (playerZ > myZ)
        {
            // Move up
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(myX, 0.3f, playerZ),
                speed * Time.deltaTime
            );
        }
        else
        {
            // Move down
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(myX, 0.3f, playerZ),
                speed * Time.deltaTime
            );
        }
    }
}
