using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

  public GameObject[] listEnemyPrefabs;

	// Use this for initialization
	void Start () {
	  for (int i = 0; i < 5; i++) {
	    Vector3 screenPos = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);
      Vector3 pos = Camera.main.ScreenToWorldPoint(screenPos);
  	  GameObject go = GameObject.Instantiate(listEnemyPrefabs[i % 2]) as GameObject;
  	  go.name = "Enemy_Fast";
  	  Boid boid = go.GetComponent<Boid>();
  	  boid.Init(pos);
	  }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
