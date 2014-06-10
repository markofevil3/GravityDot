using UnityEngine;
using System;
using System.Collections;

public class Boid : MonoBehaviour {
	
	private Vector3 velocity = Vector3.zero;
	public float mass = 30.0f;
	public SteeringBehavior steering;
	
	private bool start = false;
	
	void Awake() {
		steering.Init(this);
		start = true;
	}
	
	public virtual void Init(Vector3 pos) {
	  transform.position = pos;
		steering.Init(this);
		start = true;
		// steering 	= new SteeringManager(this);
		// x = position.x;
		// y = position.y;
	}
	
	public float GetAngle(Vector3 vector) {
		return Mathf.Atan2(vector.y, vector.x);
	}
	
	public void Update() {
	  if (start) {
	    if (isCollision) {
	      Debug.Log("Update Bounce");
	      Vector2 normal = velocity - Vector3.one;
        normal.Normalize();
	      velocity += Vector3.Reflect(velocity, normal);
	      velocity = Utils.Truncate(velocity, GetMaxVelocity());
	      transform.position += velocity;
	    } else {
	      steering.Seek(GameObject.Find("Ship").transform.position);
    		steering.DoUpdate();
	    }

  		// transform.position = position;

  		// x = position.x;
  		// y = position.y;

  		// Adjust boid rodation to match the velocity vector.
      // transform.rotation = Quaternion.Euler(new Vector3(90 + (180 * GetAngle(velocity)) / Mathf.PI, transform.rotation.y, transform.rotation.z));

  		// if (position.x < 0 || position.x > Screen.width || position.y < 0 || position.y > Screen.height) {
  		// 	Reset();
  		// }
	  }
	}
	
	private bool isCollision = false;
	
	void OnTriggerEnter() {
	  Debug.Log("OnTriggerEnter");
	  isCollision = true;
    // Invoke("StopCollision", 0.1f);
	}
	
	void OnTriggerExit() {
	  isCollision = false;
	}
	
	public void Reset() {
	}
	
	public Vector3 Velocity {
		get {return velocity;}
		set {velocity = value;}
	}
	
	public float GetMaxVelocity() {
		return 0.5f;
	}
	
	public Vector3 GetPosition() {
		return transform.position;
	}
	
	public float GetMass() {
		return mass;
	}
}
