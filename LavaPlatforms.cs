using UnityEngine;
using System.Collections;

public class LavaPlatforms : MonoBehaviour {

    public GameObject Boss;
    public Material BasicRock;
    public Material DeadRock;

    public float RockHealth = 5;

    Vector3 OriginalSize = new Vector3 (0.9992147f, 0.4748742f, 1.001193f);
    Vector3 IncreaseSize = new Vector3(0.9992147f, 1.36f, 1.001193f);
	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<Renderer>().material = BasicRock;

    }


    void Awake()
    {
        Boss = GameObject.Find("FlameDemon");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Boss.GetComponent<FlameDemon>().LavaWrath)
        {
            //if true
            transform.localScale = IncreaseSize;
        }
        else
        {
            //if false
            transform.localScale = OriginalSize;
        }


        if(RockHealth<=0)
        {
            gameObject.GetComponent<Renderer>().material = DeadRock;
        }
	}


    void OnColliderEnter(Collider colli)
    {
        if(colli.gameObject.tag == "Magma")
        {
            RockHealth -= 1;
        }

        if(RockHealth <=0 && colli.gameObject.tag == "Main_Player")
        {

        }
    }
}
