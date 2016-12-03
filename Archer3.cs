using UnityEngine;
using System.Collections;

public class Archer3 : MonoBehaviour {


    // public bool HeavyDelayStart = false;

    public float Health = 315;
    public float Shield = 0;
    public float Energy = 100;
    //others
    public Animator anim;
    // movement
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    //variables
    public GameObject ArcherBody;
    // power up bools
    public bool BasicArrows = true;
    public bool BluntArrows = false;
    public bool ExplosiveArrows = false;
    //ability bools
    public bool LightAttack = false;
    public bool HeavyAttack = false;
    public bool AoeAttack = false;
    //gameobjects
    public GameObject Arrow;
    public GameObject[] AoeArrowPosition;
    //ints
    public int AoeAttackCount = 0;
    public int ArrowSpawnCount = 0;
    //floats
    public float LightAttackDelay = 0.1f;
    public float HeavyAttackDelay = 1f;
    public float AoeAttackDelay = 3.30f;



    // Use this for initialization
    void Start()
    {

        AoeArrowPosition = GameObject.FindGameObjectsWithTag("ArrowAoeSpawn");
    }

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ArcherBody = gameObject;

        CharController();
        ArcherPowerUpSystem();
        ArcherCombatSystem();
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

    void ArcherPowerUpSystem()
    {
        //how the power ups affect the Archer


        if (BasicArrows == true)
        {
            Arrow = Resources.Load("Arrow") as GameObject;
        }
        else
        {
            if (BluntArrows == true)
            {
                Arrow = Resources.Load("ArrowBlunt") as GameObject;
            }
            else
            {
                if (ExplosiveArrows == true)
                {
                    Arrow = Resources.Load("ArrowExplosive") as GameObject;
                }
            }
        }

    }

    void ArcherCombatSystem()
    {

        //set basics to key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            LightAttack = true;
            anim.Play("Light_Attack_Bow", -1, 0f);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                HeavyAttack = true;
                anim.Play("Heavy_Attack_Bow", -1, 0f);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    AoeAttack = true;
                    anim.Play("Aoe_Attack_Bow", -1, 0f);
                }
            }
        }


        if (LightAttack == true)
        {
            /*
            HeavyAttack = false;
            AoeAttack = false;
            */
            LightAttackDelay -= Time.deltaTime;
        }
        else
        {
            if (HeavyAttack == true)
            {
                /*
                LightAttack = false;
                AoeAttack = false;
                */
                HeavyAttackDelay -= Time.deltaTime;
            }
            else
            {
                if (AoeAttack == true)
                {
                    /*
                    LightAttack = false;
                    HeavyAttack = false;
                    */
                    AoeAttackDelay -= Time.deltaTime;
                }
            }
        }

        if (LightAttack == true && ArrowSpawnCount <= 0 && LightAttackDelay <= 0)
        {
            GameObject BasicArrow = Instantiate(Arrow) as GameObject;
            BasicArrow.transform.position = gameObject.transform.position + gameObject.transform.up + gameObject.transform.forward;
            BasicArrow.transform.rotation = gameObject.transform.rotation;
            Rigidbody BA = BasicArrow.GetComponent<Rigidbody>();
            BA.velocity = gameObject.transform.forward * 40;
            BA.useGravity = false;
            ArrowSpawnCount += 1;
        }
        else
        {
            if (HeavyAttack == true && ArrowSpawnCount <= 0 && HeavyAttackDelay <= 0)
            {

                GameObject HeavyArrow = Instantiate(Arrow) as GameObject;
                HeavyArrow.transform.position = gameObject.transform.position + gameObject.transform.up;
                HeavyArrow.transform.transform.rotation = gameObject.transform.rotation;
                // HeavyArrow.transform.rotation = 
                Rigidbody HA1 = HeavyArrow.GetComponent<Rigidbody>();
                HA1.velocity = gameObject.transform.forward * 40;
                // HA1.useGravity = false;


                GameObject HeavyArrow2 = Instantiate(Arrow) as GameObject;
                HeavyArrow2.transform.position = gameObject.transform.position + gameObject.transform.up + gameObject.transform.right;
                HeavyArrow2.transform.transform.rotation = gameObject.transform.rotation;
                // HeavyArrow.transform.rotation =
                Rigidbody HA2 = HeavyArrow2.GetComponent<Rigidbody>();
                HA2.velocity = gameObject.transform.position * 40;
                // HA2.useGravity = false;

                GameObject HeavyArrow3 = Instantiate(Arrow) as GameObject;
                HeavyArrow3.transform.position = gameObject.transform.position + gameObject.transform.up - gameObject.transform.right;
                HeavyArrow3.transform.transform.rotation = gameObject.transform.rotation;
                //HeavyArrow.transform.rotation = 
                Rigidbody HA3 = HeavyArrow3.GetComponent<Rigidbody>();
                HA3.velocity = gameObject.transform.forward * 40;
                // HA3.useGravity = false;

                ArrowSpawnCount += 1;
            }
            else
            {


                if (AoeAttack == true && AoeAttackCount <= 36 && AoeAttackDelay <= 0)
                {
                    int ArrowAoes = Random.Range(0, (AoeArrowPosition.Length - 1));


                    GameObject AoeArrow = Instantiate(Arrow) as GameObject;
                    AoeArrow.transform.position = AoeArrowPosition[ArrowAoes].transform.position;
                    AoeArrow.transform.rotation = Quaternion.Euler(0, 90, 90);
                    Rigidbody AoeRB = Arrow.GetComponent<Rigidbody>();
                    AoeRB.useGravity = true;
                    AoeAttackCount += 1;
                }
            }
        }

        if (LightAttack == true && LightAttackDelay <= 0 && ArrowSpawnCount >= 1)
        {
            LightAttack = false;
            ArrowSpawnCount -= 1;
            LightAttackDelay = 0.1f;
        }
        else
        {
            if (HeavyAttack == true && HeavyAttackDelay <= 0 && ArrowSpawnCount >= 1)
            {
                HeavyAttack = false;
                ArrowSpawnCount -= 1;
                HeavyAttackDelay = 1;
            }
            else
            {
                if (AoeAttack == true && AoeAttackDelay <= 0 && AoeAttackCount >= 36)
                {
                    AoeAttack = false;
                    AoeAttackCount = 0;
                    AoeAttackDelay = 3.30f;
                }
            }
        }

    }
}
