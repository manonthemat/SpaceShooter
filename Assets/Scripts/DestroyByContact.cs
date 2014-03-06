using UnityEngine;
using System.Collections;


public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	public int hitPoints;
	private GameController gameController;
	public GameObject bolt;


	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		else {
			Debug.Log("Can not find gameControllerObject");
		}
	}


	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary") {
			return;
		}
		Instantiate(explosion, transform.position, transform.rotation);
		if (other.tag == "Player") {
		 	Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
		else if (other.tag == "bolt") {
			Debug.Log("damage!");
			hitPoints -= other.GetComponent<DoDamage>().damage;
		}
		if (hitPoints <= 0) {
			gameController.AddScore (scoreValue);
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
