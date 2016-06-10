using UnityEngine;
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
	
	private float _nextLaunchTime;

	private GameController _gameController;

	void Start () {
        gameStarted = false;
		_gameController = this.GetComponent<GameController>();
	}

    public void StartWaves() {
        gameStarted = true;
        SetNextLaunch();
    }
	
	void SetNextLaunch() {
		float launchInterval = Random.Range(launchRangeMinTime, launchRangeMaxTime);
        if (GvrViewer.Instance.VRModeEnabled) {
            launchInterval *= 1.1f;
        }
        _nextLaunchTime = Time.time + launchInterval;
	}
	
	void Update () {
		if (gameStarted && Time.time > _nextLaunchTime && !_gameController.isGameOver) {
            Vector3 launchPosition = RandomCirclePosition(spawnRadius);

            Quaternion launchRotation = Quaternion.Euler(Vector3.zero);
            Instantiate(enemyPrefab, launchPosition, launchRotation);
			SetNextLaunch();
		}
	}

    Vector3 RandomCirclePosition(float radius) {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = 1;
        pos.z = radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
