using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	public float speedFactor;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	private Vector3 zeroAcceleration;
	private Vector3 currentAcceleration;
	private float HorizontalAxis = 0;
	private float VerticalAxis = 0;

	void Start() {
		zeroAcceleration = Input.acceleration;
		currentAcceleration = Vector3.zero;
	}

	void Update() {
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play();
		}
	}

	void FixedUpdate () {
		currentAcceleration = Vector3.Lerp(currentAcceleration, Input.acceleration - zeroAcceleration, Time.deltaTime);
		float moveHorizontal = Input.acceleration.x;
//		float moveVertical = Mathf.Clamp (currentAcceleration.x * 10, -1, 1);
		float moveVertical = Input.acceleration.y;
//		float moveVertical = currentAcceleration.y;

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speedFactor;

		rigidbody.position = new Vector3 (
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
		);

		rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}
}
