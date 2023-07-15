using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;

    private float _spawnDelay = 0.2f;

    private GameObject player;
    
    public List<GameObject> enemyPrefabs;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(SpawnEnemyCoroutine());
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        while (!isGameOver)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private void SpawnEnemy()
    {
        // Pick random enemy prefab
        var randomIndex = Random.Range(0, enemyPrefabs.Count);
        var randomEnemyPrefab = enemyPrefabs[randomIndex];
        // Spawn enemy at random position along a circle around the player
        var playerPosition = player.transform.position;
        var randomAngle = Random.Range(0, 2 * Mathf.PI);
        var randomRadius = Random.Range(7, 9);
        var randomPosition = new Vector3(
            playerPosition.x + randomRadius * Mathf.Cos(randomAngle),
            0.3f,
            playerPosition.z + randomRadius * Mathf.Sin(randomAngle)
        );
        Instantiate(randomEnemyPrefab, randomPosition, Quaternion.identity);
    }

    public void EndGame()
    {
        Debug.Log("Game over!");
        isGameOver = true;
    }
}
