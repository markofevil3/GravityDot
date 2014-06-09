using UnityEngine;
using System.Collections;

public class SteeringBehavior : MonoBehaviour {

	public const float MAX_FORCE = 0.5f;
	
	// Wander
	public const float CIRCLE_DISTANCE = 6.0f;
	public const float CIRCLE_RADIUS = 8.0f;
	public const float ANGLE_CHANGE = 1.0f;
	
	// Seek / flee
	public Vector3 desired;
	
	// Wander
	private float wanderAngle;
	
	// Pursuit and evade
	public Vector3 distance = Vector3.zero;
	public Vector3 targetFuturePosition = Vector3.zero;
	
	// Manager itself
	public Vector3 steering;
	public Boid host;
	
	public void Init(Boid host) {
		this.host			= host;
		this.desired		= new Vector3(0, 0); 
		this.steering 		= new Vector3(0, 0); 
		this.wanderAngle 	= 0; 
		host.Velocity = Utils.Truncate(host.Velocity, host.GetMaxVelocity());
	}
	
	public void Seek(Vector3 target, float slowingRadius = 0) {
		steering += DoSeek(target, slowingRadius);
	}
	
	public void Flee(Vector3 target) {
		steering += DoFlee(target);
	}
	
	public void Wander() {
		steering += DoWander();
	}
	
	public void Evade(Boid target) {
		steering += DoEvade(target);
	}
	
	public void pursuit(Boid target) {
		steering += DoPursuit(target);
	}
	
	private Vector3 DoSeek(Vector3 target, float slowingRadius = 0) {
		Vector3 force;
		float distance;
		
		desired = target - host.GetPosition();
		
		distance = desired.magnitude;
		desired = desired.normalized;
		
		if (distance <= slowingRadius) {
			desired *= host.GetMaxVelocity() * distance/slowingRadius;
		} else {
			desired *= host.GetMaxVelocity();
		}
		
		force = desired - host.Velocity;
		return force;
	}
	
	private Vector3 DoFlee(Vector3 target) {
		Vector3 force;
		
		desired = host.GetPosition() - target;
		desired = desired.normalized;
		desired *= host.GetMaxVelocity();
		
		force = desired - host.Velocity;
		
		return force;
	}
	
	private Vector3 DoWander() {
		Vector3 wanderForce;
		Vector3 circleCenter;
		Vector3 displacement;
		
		circleCenter = host.Velocity;
		circleCenter = circleCenter.normalized;
		circleCenter *= CIRCLE_DISTANCE;
		
		displacement = new Vector3(0, -1);
		displacement *= CIRCLE_RADIUS;
		
		SetAngle(displacement, wanderAngle);
		wanderAngle += Random.value * ANGLE_CHANGE - ANGLE_CHANGE * 0.5f;

		wanderForce = circleCenter + displacement;
		return wanderForce;
	}
	
	private Vector3 DoEvade(Boid target) {
		distance = target.GetPosition() - host.GetPosition();
		
		float updatesNeeded = distance.magnitude / host.GetMaxVelocity();
		
		Vector3 tv = target.Velocity;
		tv *= updatesNeeded;
		
		targetFuturePosition = target.GetPosition() + tv;
		
		return DoFlee(targetFuturePosition);
	}
	
	private Vector3 DoPursuit(Boid target) {
		distance = target.GetPosition() - host.GetPosition();
		
		float updatesNeeded = distance.magnitude / host.GetMaxVelocity();
		
		Vector3 tv = target.Velocity;
		tv *= updatesNeeded;
		
		targetFuturePosition = target.GetPosition() - tv;
		
		return DoSeek(targetFuturePosition);
	}
	
	public float GetAngle(Vector3 vector) {
		return Mathf.Atan2(vector.y, vector.x);
	}
	
	public void SetAngle(Vector3 vector, float value) {
		float len = vector.magnitude;
		vector.x = Mathf.Cos(value) * len;
		vector.y = Mathf.Sin(value) * len;
	}
	
	public void DoUpdate() {
		Vector3 velocity = host.Velocity;
		Vector3 position = host.GetPosition();
		steering = Utils.Truncate(steering, MAX_FORCE);
		steering *= 1 / host.GetMass();		
		velocity += steering;
		velocity = Utils.Truncate(velocity, host.GetMaxVelocity());
		host.Velocity = velocity;
		
		transform.position += velocity;

	}
	
	public void Reset() {
		desired = Vector3.zero;			
		steering = Vector3.zero;			
	}
}
