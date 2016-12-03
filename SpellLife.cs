using UnityEngine;
using System.Collections;

public class SpellLife : MonoBehaviour {

    public float LifeTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        LifeTime -= Time.deltaTime;

        if(LifeTime <= 0)
        {
            Destroy(gameObject);
        }
	}
}
