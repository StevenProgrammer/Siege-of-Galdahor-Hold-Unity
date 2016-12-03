using UnityEngine;
using System.Collections;

public class CharacterSwitchSystem : MonoBehaviour {

    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public GameObject CharacterHolder;

    public GameObject MageBody;
    public GameObject PriestBody;
    public GameObject WarriorBody;
    public GameObject ArcherBody;

    public GameObject Smoke;
    public float delay = 1;
    public bool ArcherControl = false;
    public bool MageControl = false;
    public bool PriestControl = false;
    public bool WarriorControl = false;
    public AudioSource CharSwitch;


    // Use this for initialization
    void Start ()
    {

        Smoke = Resources.Load("SmokeSwitch") as GameObject;
        CharacterHolder = GameObject.Find("Papa");
        MageBody = GameObject.Find("Mage");
        PriestBody = GameObject.Find("Priest");
        WarriorBody = GameObject.Find("Warrior");
        ArcherBody = GameObject.Find("Archer");
        MageControl = true;



    }
	
	// Update is called once per frame
	void Update ()
    {
        // gameObject.transform.parent = GameObject.FindGameObjectWithTag("Main_Player").transform;
        gameObject.transform.parent = CharacterHolder.transform;
        CharacterSwitch();
	}


    void CharacterSwitch()
    {
        //setting bools on input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //change bools
            ArcherControl = false;
            MageControl = true;
            PriestControl = false;
            WarriorControl = false;
            //spawn smoke particle 
            Instantiate(Smoke, CharacterHolder.transform.position, Quaternion.identity);
            //play smoke audio
         
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //change bools
                ArcherControl = false;
                MageControl = false;
                PriestControl = true;
                WarriorControl = false;
                //spawn smoke particle 
                Instantiate(Smoke, CharacterHolder.transform.position, Quaternion.identity);
                //play smoke audio

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    //change bools
                    ArcherControl = false;
                    MageControl = false;
                    PriestControl = false;
                    WarriorControl = true;
                    //spawn smoke particle 
                    Instantiate(Smoke, CharacterHolder.transform.position, Quaternion.identity);
                    //play smoke audio
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        //change bools
                        ArcherControl = true;
                        MageControl = false;
                        PriestControl = false;
                        WarriorControl = false;
                        //spawn smoke particle 
                        Instantiate(Smoke, CharacterHolder.transform.position, Quaternion.identity);
                        //play smoke audio
                    }
                }
            }
        }

        //doing things to each character for each bool
        if (MageControl == true)
        {
            //make gameobject the parent object
            ArcherBody.transform.parent = MageBody.transform;
            MageBody.transform.parent = null;
            PriestBody.transform.parent = MageBody.transform;
            WarriorBody.transform.parent = MageBody.transform;
            CharacterHolder.transform.parent = MageBody.transform;
            //set to active
            ArcherBody.SetActive(false);
            MageBody.SetActive(true);
            PriestBody.SetActive(false);
            WarriorBody.SetActive(false);
            //change tags 
            ArcherBody.tag = "Archer";
            MageBody.tag = "Main_Player";
            PriestBody.tag = "Priest";
            WarriorBody.tag = "Warrior";

        }
        else
        {
            if(PriestControl == true)
            {
                //make gameobject the parent object
                ArcherBody.transform.parent = PriestBody.transform;
                MageBody.transform.parent = PriestBody.transform;
                PriestBody.transform.parent = null;
                WarriorBody.transform.parent = PriestBody.transform;
                CharacterHolder.transform.parent = PriestBody.transform;
                //set to active
                ArcherBody.SetActive(false);
                MageBody.SetActive(false);
                PriestBody.SetActive(true);
                WarriorBody.SetActive(false);
                //change tags 
                ArcherBody.tag = "Archer";
                MageBody.tag = "Mage";
                PriestBody.tag = "Main_Player";
                WarriorBody.tag = "Warrior";

            }
            else
            {
                if(WarriorControl == true)
                {
                    //make gameobject the parent object
                    ArcherBody.transform.parent = WarriorBody.transform;
                    MageBody.transform.parent = WarriorBody.transform;
                    PriestBody.transform.parent = WarriorBody.transform;
                    WarriorBody.transform.parent = null;
                    CharacterHolder.transform.parent = WarriorBody.transform;
                    //set to active
                    ArcherBody.SetActive(false);
                    MageBody.SetActive(false);
                    PriestBody.SetActive(false);
                    WarriorBody.SetActive(true);
                    //change tags 
                    ArcherBody.tag = "Archer";
                    MageBody.tag = "Mage";
                    PriestBody.tag = "Priest";
                    WarriorBody.tag = "Main_Player";
                }
                else
                {
                    if(ArcherControl == true)
                    {
                        //make gameobject the parent object
                        ArcherBody.transform.parent = null;
                        MageBody.transform.parent = ArcherBody.transform;
                        PriestBody.transform.parent = ArcherBody.transform;
                        WarriorBody.transform.parent = ArcherBody.transform;
                        CharacterHolder.transform.parent = ArcherBody.transform;
                        //set to active
                        ArcherBody.SetActive(true);
                        MageBody.SetActive(false);
                        PriestBody.SetActive(false);
                        WarriorBody.SetActive(false);
                        //change tags 
                        ArcherBody.tag = "Main_Player";
                        MageBody.tag = "Mage";
                        PriestBody.tag = "Priest";
                        WarriorBody.tag = "Warrior";
                    }
                }
            }
        }
    }
}
