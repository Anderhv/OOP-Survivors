using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public float Score { get; private set; }
    
    private float _spawnDelay = 0.4f;

    private GameObject player;
    
    private GameObject _gameOverScreen;
    
    public TMPro.TextMeshProUGUI scoreText;
    
    public List<GameObject> enemyPrefabs;
    
    void Start()
    {
        Time.timeScale = 1f;
        player = GameObject.FindWithTag("Player");
        _gameOverScreen = GameObject.FindWithTag("GameOverScreen");
        _gameOverScreen.SetActive(false);
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

    public void IncreaseScore(float amount)
    {
        Score += amount;
        scoreText.text = $"Score: {Score}";
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
        isGameOver = true;
        Time.timeScale = 0f;
        _gameOverScreen.SetActive(true);
    }
    
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
