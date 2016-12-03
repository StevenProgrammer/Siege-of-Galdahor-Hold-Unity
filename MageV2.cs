using UnityEngine;
using System.Collections;

public class MageV2 : MonoBehaviour {

    //mage movement
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    //variables
    public GameObject MageBody;
    public GameObject SpellBook;
    public GameObject MagePosition;
    //gameobjects
    public GameObject LightSpell;
    public GameObject HeavySpell;
    public GameObject AoeSpell;
    //ints
    public int CastCount = 0;
    public int AoeCastCount = 0;
    //floats
    public float LightAttackDelay = 1.3f;
    public float HeavyAttackDelay = 1.4f;
    public float AoeAttackDelay = 3;
    //power up bools
    public bool IceElement = true;
    public bool FireElement = false;
    public bool AirElement = false;
    public bool EarthElement = false;
    public bool SuperMage = false;
    //bool 
    public bool LightAttack = false;
    public bool HeavyAttack = false;
    public bool AoeAttack = false;
    //others
    public Animator anim;


    // happens before start
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start ()
    {
        MagePosition = GameObject.FindGameObjectWithTag("MageSpawn");
        MageBody = gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CharController();
        MagePowerUpSystem();
        MageCombatSystem();
    }

    void CharController()
    {


        //basic movement system
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;




        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

    }

    void MagePowerUpSystem()
    {
        if(IceElement == true)
        {
            LightSpell = Resources.Load("Ice_Blast") as GameObject;
            HeavySpell = Resources.Load("Ice_Heavy") as GameObject;
            AoeSpell = Resources.Load("Frost_Storm") as GameObject;
        }
        else
        {
            if(FireElement== true)
            {
                LightSpell = Resources.Load("") as GameObject;
                HeavySpell = Resources.Load("") as GameObject;
                AoeSpell = Resources.Load("") as GameObject;
            }
            else
            {
                if(AirElement == true)
                {
                    LightSpell = Resources.Load("") as GameObject;
                    HeavySpell = Resources.Load("") as GameObject;
                    AoeSpell = Resources.Load("") as GameObject;
                }
                else
                {
                    if(EarthElement == true)
                    {
                        LightSpell = Resources.Load("") as GameObject;
                        HeavySpell = Resources.Load("") as GameObject;
                        AoeSpell = Resources.Load("") as GameObject;
                    }
                    else
                    {
                        if(SuperMage == true)
                        {

                        }
                    }
                }
            }
        }
    }

    void MageCombatSystem()
    {
        //on key press stuff
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.Play("Light_Spell", -1, 0f);
            LightAttack = true;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                anim.Play("Heavy_Spell", -1, 0f);
                HeavyAttack = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    anim.Play("Aoe_Spell", -1, 0f);
                    AoeAttack = true;
                }
            }
        }


        if(LightAttack == true)
        {
            LightAttackDelay -= Time.deltaTime;
        }
        else
        {
            if(HeavyAttack == true)
            {
                HeavyAttackDelay -= Time.deltaTime;
            }
            else
            {
                if(AoeAttack == true)
                {
                    AoeAttackDelay -= Time.deltaTime;
                }
            }
        }

        // base actions
        if(LightAttack == true && CastCount <= 0 && LightAttackDelay <= 0)
        {
            //smmon open boook to hold the spell

            //summon the spell
            GameObject BasicSpell = Instantiate(LightSpell) as GameObject;
            BasicSpell.transform.position = MagePosition.transform.position;
            Rigidbody BS = BasicSpell.GetComponent<Rigidbody>();
            BS.velocity = MagePosition.transform.forward * 40;
            CastCount += 1;
        }
        else
        {
            if(HeavyAttack == true && CastCount <=0 && HeavyAttackDelay <= 0)
            {
                //smmon open boook to hold the spell

                //summon the spell
                GameObject IceWave = Instantiate(HeavySpell) as GameObject;
                IceWave.transform.position = MagePosition.transform.position;
                CastCount += 1;
            }
            else
            {
                if(AoeAttack == true && AoeCastCount <=0 && AoeAttackDelay <= 0)
                {

                    //smmon open boook to hold the spell

                    //summon the spell
                    GameObject IStorm = Instantiate(AoeSpell) as GameObject;
                    IStorm.transform.position = MagePosition.transform.position + MagePosition.transform.up * 7.5f;
                    CastCount += 1;
                }
            }
        }


        //part 4 of attack that lets thinsg loop
        if (LightAttack == true && CastCount <= 1 && LightAttackDelay <= 0)
        {

            LightAttackDelay = 1.3f;
            LightAttack = false;
            CastCount -= 1;
        }
        else
        {
            if(HeavyAttack == true && CastCount <=1 && HeavyAttackDelay <= 0)
            {
                HeavyAttackDelay = 1.4f;
                HeavyAttack = false;
                CastCount -= 1;
            }
            else
            {
                if(AoeAttack == true && AoeCastCount <= 1 && AoeAttackDelay <= 0)
                {
                    AoeAttackDelay = 3f;
                    AoeAttack = false;

                }
            }
        }
    }
}
