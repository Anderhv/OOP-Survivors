using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : Enemy
{
    protected override void MoveTowardsPlayer()
    {
        // Move in a straight line towards player
        transform.position = Vector3.MoveTowards(
            transform.position,
            _player.transform.position, 
            speed * Time.deltaTime
        );
    }
}
