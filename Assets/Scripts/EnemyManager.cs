using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public enum WALL {
		TOP = 0,
		BOTTOM,
		LEFT,
		RIGHT
	}

  public GameObject[] listEnemyPrefabs;
	public Camera gameCamera;
	// public Transform[] walls;
	public static Vector3[] wallsNormalized = new Vector3[4];
	public static Vector3[] screenCornersPos = new Vector3[4];

	void Start () {
		Vector3 topLeft = screenCornersPos[0] = gameCamera.ViewportToWorldPoint(new Vector3(0,1,0));
		Vector3 topRight = screenCornersPos[1] = gameCamera.ViewportToWorldPoint(new Vector3(1,1,0));
		Vector3 bottomLeft = screenCornersPos[2] = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0));
		Vector3 bottomRight = screenCornersPos[3] = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0));
		
		wallsNormalized[(int)WALL.TOP] = (topLeft - topRight).normalized;
		wallsNormalized[(int)WALL.BOTTOM] = (bottomLeft - bottomRight).normalized;
		wallsNormalized[(int)WALL.LEFT] = (bottomLeft - topLeft).normalized;
		wallsNormalized[(int)WALL.RIGHT] = (bottomRight - topRight).normalized;
		
	  for (int i = 0; i < 1; i++) {
			// recheck spawn pos with object radius
	    Vector3 screenPos = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);
      Vector3 pos = Camera.main.ScreenToWorldPoint(screenPos);
  	  GameObject go = GameObject.Instantiate(listEnemyPrefabs[i % 2]) as GameObject;
  	  go.name = "Enemy_Fast";
  	  Boid boid = go.GetComponent<Boid>();
  	  boid.Init(pos);
	  }
	}

}
