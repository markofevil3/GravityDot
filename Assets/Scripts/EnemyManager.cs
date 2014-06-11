using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public enum WALL {
		TOP = 0,
		BOTTOM,
		LEFT,
		RIGHT
	}

	public static EnemyManager Instance;

  public GameObject[] listEnemyPrefabs;
	public Camera gameCamera;
	// public Transform[] walls;
	public Vector3[] wallsNormalized = new Vector3[4];

	void Start () {
		Instance = this;
		wallsNormalized[(int)WALL.TOP] = (gameCamera.ViewportToWorldPoint(new Vector3(0,1,0)) - gameCamera.ViewportToWorldPoint(new Vector3(1,1,0))).normalized;
		wallsNormalized[(int)WALL.BOTTOM] = (gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)) - gameCamera.ViewportToWorldPoint(new Vector3(1,0,0))).normalized;
		wallsNormalized[(int)WALL.LEFT] = (gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)) - gameCamera.ViewportToWorldPoint(new Vector3(0,1,0))).normalized;
		wallsNormalized[(int)WALL.RIGHT] = (gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)) - gameCamera.ViewportToWorldPoint(new Vector3(1,1,0))).normalized;
		
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
