using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public GameObject bigHazard;
	public GameObject fastHazard;
	public GameObject ronda;
	public Vector3 spawnValue;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText levelText;
	private int level;
	private bool gameOver;
	private bool restart;
	private int score;


	void Start() {
		gameOver = false;
		restart = false;
		gameOverText.text = "";
		restartText.text = "";
		levelText.text = "";
		level = 0;
		score = 0;
		UpdateScore ();
		StartCoroutine(SpawnWaves());
	}

	void Update() {
		if (restart) {
			if (Input.GetButton("Fire1")) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}


	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds (startWait);
		while(true) {
			UpdateLevel();
			for(int i = 0; i < hazardCount * level; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
				Quaternion spawnRotation = Quaternion.identity;
				float r = Random.value;
				if (r <= 0.6) {
					Instantiate (hazard, spawnPosition, spawnRotation);
				}
				else if (r > 0.6 && r < 0.8) {
					Instantiate (fastHazard, spawnPosition, spawnRotation);
				}
				else if (r >= 0.8 && r < 0.95) {
					Instantiate (ronda, spawnPosition, spawnRotation);
				}
				else {
					Instantiate (bigHazard, spawnPosition, spawnRotation);
				}
				if (gameOver) {
					yield return new WaitForSeconds(3);
					restartText.text = "Press 'FIRE' for Restart";
					restart = true;
					break;
				}
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}


	public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}


	void UpdateLevel() {
		level++;
		levelText.text = "Level: " + level;
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	public void GameOver() {
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

	public int GetLevel() {
		return level;
	}

}
