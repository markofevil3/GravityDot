using UnityEngine;
using System;
using System.Collections;

public class Utils : MonoBehaviour {
	
	public static float Truncate(float number, float maxValue) {
		return (float)Math.Truncate((double)Math.Min(number, maxValue));
	}
	
	public static Vector3 Truncate(Vector3 vector, float max) {
		float i;
		i = max / vector.magnitude;
		i = i < 1.0f ? i : 1.0f;
		
		return vector * i;
	}
}
