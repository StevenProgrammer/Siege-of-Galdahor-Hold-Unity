using UnityEngine;
using System.Collections;

public class Priest : MonoBehaviour {

    public static bool IsDead = false;


    // movement
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    //variables
    public GameObject PriestBody;
    //bools
    public bool GreatShield = false;
    public bool HealingSpell = false;
    public bool HolyCrush = false;
    public bool Healing = false;
    //gameobjects
    public GameObject HolyShield;
    public GameObject Heal;
    public GameObject Crush;
    public GameObject Spawnprojectile;
    public GameObject Spawnprojectile2;
    //ints
    public int CastCount = 0;
    public int CastCountShield = 0;
    //floats
    public float HealDelay = 1f;
    public float ShieldDelay = 1.5f;
    public float AttackDelay = 0.8f;
    //others
    public Animator anim;
    //use this for before initialization
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        
        PriestBody = gameObject;
        Heal = Resources.Load("Holy_Heal") as GameObject;
       HolyShield = Resources.Load("Holy_Shield") as GameObject;
        Crush = Resources.Load("HolyAttack") as GameObject;
        Spawnprojectile = GameObject.FindGameObjectWithTag("SpawnPointPriest");
        Spawnprojectile2 = GameObject.FindGameObjectWithTag("SpawnPointPriest2");
    }

    // Update is called once per frame
    void Update()
    {

        CharController();
        PriestPowerUpSystem();
        PriestCombatSystem();


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

    void PriestPowerUpSystem()
    {
        //how the power ups affect the mage
    }

    void PriestCombatSystem()
    {
        //combat system

        if (Input.GetKeyDown(KeyCode.E))
        {
            HealingSpell = true;
            anim.Play("magic_heal", -1, 0f);
            
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                HolyCrush = true;
                anim.Play("Holy_Attack", -1, 0f);
                

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    GreatShield = true;
                    anim.Play("Holy_Shield", -1, 0f);
                }
            }
        }
        if(HealingSpell == true)
        {
            HealDelay -= Time.deltaTime;
        }
        else
        {
            if(GreatShield == true)
            {
                ShieldDelay -= Time.deltaTime;
            }
            else
            {
                if (HolyCrush == true)
                {
                    AttackDelay -= Time.deltaTime;
                }
            }
        }

        if(HealingSpell == true && CastCount <= 0 && HealDelay <=0)
        {
         //2 secs   
            CastCount += 1;
            GameObject HolyHeal = Instantiate(Heal) as GameObject;
            HolyHeal.transform.position = new Vector3(gameObject.transform.position.x,-1.2f,gameObject.transform.position.z);
            HolyHeal.transform.eulerAngles = new Vector3 (270, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
            //Rigidbody HH = HolyHeal.GetComponent<Rigidbody>();
            // HH.velocity = Spawnprojectile.transform.forward * 2;
            Healing = true;
            //Rigidbody HH = 
        }
        else
        {
            if(GreatShield == true && CastCountShield <= 0 && ShieldDelay <= 0)
            {
                //Instantiate(HolyShield, gameObject.transform.position, Quaternion.identity);
                GameObject Shield = Instantiate(HolyShield) as GameObject;
                Shield.transform.position = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z);
                CastCountShield += 1;
            }
            else
            {
                if(HolyCrush == true && CastCount <= 0 && AttackDelay <= 0)
                {
                    GameObject HCrush = Instantiate(Crush) as GameObject;
                    HCrush.transform.position = Spawnprojectile.transform.position;
                    HCrush.transform.rotation = Spawnprojectile.transform.rotation;
                    Rigidbody HC =HCrush.GetComponent<Rigidbody>();
                    HC.velocity = Spawnprojectile.transform.forward * 10;
                    CastCount += 1;
                }
            }
        }

        if (HealingSpell == true && HealDelay <= 0 && CastCount <=1)
        {
            HealingSpell = false;
            CastCount -= 1;
            HealDelay = 1f;
        }
        else
        {
            if (HolyCrush == true && AttackDelay <= 0 && CastCount >= 1)
            {
                HolyCrush = false;
                CastCount -= 1;
                AttackDelay = 0.8f;
                //AttackDelay
            }
            else
            {
                if (GreatShield == true && ShieldDelay <= 0 && CastCountShield >= 1)
                {
                    GreatShield = false;
                    ShieldDelay = 2;
                    CastCountShield -= 1;
                    //HolyShield.GetComponent<HolyShield>().Expire 
                }
            }
        }
    }
}


//