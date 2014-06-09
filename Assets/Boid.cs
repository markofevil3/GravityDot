using UnityEngine;
using System;
using System.Collections;

public class Boid : MonoBehaviour {
	
	public Vector3 position = Vector3.zero;
	public Vector3 velocity = new Vector3(-1, -2);
	private float mass = 5.0f;
	public SteeringManager steering;
	
	void Awake() {
		steering.Init(this);
		position = transform.position;
	}
	
	public void Init(float posX, float posY, float totalMass) {
		position 	= new Vector3(posX, posY);
		velocity 	= new Vector3(-1, -2);
		mass	 	= totalMass;
		// steering 	= new SteeringManager(this);
		transform.position = position;
		// x = position.x;
		// y = position.y;
	}
	
	public float GetAngle(Vector3 vector) {
		return Mathf.Atan2(vector.y, vector.x);
	}
	
	public void Think() {
		steering.Wander();
	}
	
	public void Update() {
		// Think();
		steering.Seek(GameObject.Find("Ship").transform.position);
		steering.DoUpdate();
		
		// transform.position = position;
		
		// x = position.x;
		// y = position.y;
		
		// Adjust boid rodation to match the velocity vector.
		// transform.rotation = 90 + (180 * getAngle(velocity)) / Mathf.PI;
		
		// if (position.x < 0 || position.x > Screen.width || position.y < 0 || position.y > Screen.height) {
		// 	Reset();
		// }
	}
	
	public void Reset() {
		transform.position = position = new Vector3(Screen.width / 2, Screen.height / 2);
	}
	
	public Vector3 GetVelocity() {
		return velocity;
	}
	
	public float GetMaxVelocity() {
		return 1;
	}
	
	public Vector3 GetPosition() {
		return transform.position;
	}
	
	public float GetMass() {
		Debug.Log("GetMass " + mass);
		return mass;
	}
}
