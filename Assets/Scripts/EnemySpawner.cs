using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Enemy spawner. Creates a new enemy once every few seconds, assuming we're not in a game over state.
/// </summary>
public class EnemySpawner : MonoBehaviour {

    public float spawnRadius;
	public float launchRangeMinTime;
	public float launchRangeMaxTime;
	public GameObject enemyPrefab;
    public Transform target;
    public bool gameStarted;

    public int waveNum;
    public int enemiesInWave;
    public float spawnRate;
    public int enemiesIncrease;
    public float spawnRateDecrease;
    public float timeBetweenWaves = 3f;
    public Text wavesText;

    public int HealthHealedPerWave;
    public PlayerHealth playerHealth;

    private int enemiesKilled;
    private int startEnemiesInWave;
    private float startSpawnRate;

	private float _nextLaunchTime;


	public GameController gameController;
    public ObjectPoolScript objectPool;

	void Start () {
        gameStarted = false;
        wavesText.text = " ";

        startEnemiesInWave = enemiesInWave;
        startSpawnRate = spawnRate;
	}

    public void StartWaves() {
        gameStarted = true;
        waveNum = 1;
        enemiesKilled = 0;
        enemiesInWave = startEnemiesInWave;
        spawnRate = startSpawnRate;

        StartCoroutine(NextWave());
    }

    public IEnumerator NextWave() {
        Invoke("ShowWaveText", 1f);
        Invoke("FadeWaveText", 2.5f);

        yield return new WaitForSeconds(timeBetweenWaves);

        for (int i = 0; i < enemiesInWave; i++) {              
            SpawnEnemy();
            yield return new WaitForSeconds(spawnRate);
            if (gameController.isGameOver) {
                break;
            }
        }
    }

    void WaveFinished() {
        waveNum += 1;
        enemiesInWave += enemiesIncrease;
        enemiesKilled = 0;
        if (spawnRate > 0.6) {
            spawnRate -= spawnRateDecrease;
        }

        playerHealth.HealPlayer(HealthHealedPerWave);
    }

    void SpawnEnemy() {
        Vector3 launchPosition = RandomCirclePosition(spawnRadius);
        Quaternion launchRotation = Quaternion.Euler(Vector3.zero);
        GameObject enemy = GetRandomEnemy();

        enemy.transform.position = launchPosition;
        enemy.transform.LookAt(new Vector3(0, 0, 0));

        enemy.SetActive(true);
    }

    void FadeWaveText() {
        wavesText.GetComponentInParent<FadeText>().Fade();
    }

    void ShowWaveText() {
        wavesText.GetComponentInParent<FadeText>().Show();
        wavesText.text = "Wave " + waveNum;
    }

    public void EnemyKilled() {
        enemiesKilled += 1;
        if(enemiesKilled == enemiesInWave && !gameController.isGameOver) {
            WaveFinished();
            StartCoroutine(NextWave());
        }
    }

    public void DeactivateAllEnemies() {
        objectPool.DisableAllEnemies();
    }

    Vector3 RandomCirclePosition(float radius) {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = 0;
        pos.z = radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    GameObject GetRandomEnemy() {
        GameObject enemy;
        if (waveNum > 2) {
            if (Random.Range(1, 7) < 5) {
                enemy = objectPool.GetPooledEnemy();
            } else {
                enemy = objectPool.GetFlyingEnemy();
            }
        } else {
            enemy = objectPool.GetPooledEnemy();
        }

        return enemy;
    }
}
