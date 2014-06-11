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
	
	Vector3 reflect = Vector3.zero;
	int reflectWallIndex = -1;
	float tempX;
	float tempY;
	
	public void LateUpdate() {
	  if (start) {
	    if (isCollision) {
	      // Vector2 normal = velocity - Vector3.one;
	      //         normal.Normalize();
	      // velocity += Vector3.Reflect(velocity, normal);
	      // velocity = Utils.Truncate(velocity, GetMaxVelocity());
				if (reflect == Vector3.zero && reflectWallIndex != -1) {
					Vector3 normalized = EnemyManager.wallsNormalized[reflectWallIndex];
					float dot = Vector3.Dot(velocity, normalized);
					reflect = velocity * -1.0f + 2 * dot * normalized;
					reflect *= 0.7f;
					velocity = reflect;
					velocity = Utils.Truncate(velocity, GetMaxVelocity());
					
				}
	      
	      transform.position += velocity;
	    } else {
	      steering.Seek(GameObject.Find("Ship").transform.position);
    		steering.DoUpdate();
	    }
	
			// Keep object inside screen
			tempX = transform.position.x;
			tempY = transform.position.y;
			if (transform.position.x - transform.renderer.bounds.extents.x < EnemyManager.screenCornersPos[0].x) {
				tempX = EnemyManager.screenCornersPos[0].x + transform.renderer.bounds.extents.x;
			}
			if (transform.position.x + transform.renderer.bounds.extents.x> EnemyManager.screenCornersPos[1].x) {
				tempX = EnemyManager.screenCornersPos[1].x - transform.renderer.bounds.extents.x;
			}
			if (transform.position.y + transform.renderer.bounds.extents.y > EnemyManager.screenCornersPos[0].y) {
				tempY = EnemyManager.screenCornersPos[0].y - transform.renderer.bounds.extents.y;
			}
			if (transform.position.y - transform.renderer.bounds.extents.y < EnemyManager.screenCornersPos[2].y) {
				tempY = EnemyManager.screenCornersPos[2].y + transform.renderer.bounds.extents.y;
			}
			transform.position = new Vector3(tempX, tempY, transform.position.z);
  		// Adjust boid rodation to match the velocity vector.
      // transform.rotation = Quaternion.Euler(new Vector3(90 + (180 * GetAngle(velocity)) / Mathf.PI, transform.rotation.y, transform.rotation.z));

  		// if (position.x < 0 || position.x > Screen.width || position.y < 0 || position.y > Screen.height) {
  		// 	Reset();
  		// }
	  }
	}
	
	private bool isCollision = false;
	
	void OnTriggerEnter(Collider other) {
	  Debug.Log("OnTriggerEnter " + other.gameObject.name);
		if (other.gameObject.name != "Ship") {
			reflectWallIndex = int.Parse(other.gameObject.name);
		  isCollision = true;
		}
	}
	
	void OnTriggerExit() {
		Debug.Log("OnTriggerExit " + reflectWallIndex);
	  
		reflect = Vector3.zero;
		reflectWallIndex = -1;
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
