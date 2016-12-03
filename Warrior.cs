using UnityEngine;
using System.Collections;

public class Warrior : MonoBehaviour {

    public bool IsDead = false;

    // movement
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;


    //variables

    public GameObject WarriorBody;

    public bool BaseWarrior = true;
    public bool ISPaladin  = false;
    public bool ISEnraged  = false;
    public bool ISFatigued = false;

    public Animator anim;

    public float LightAttackTimer = 1f;
    public float HeavyAttackTimer = 1.43f;
    public float AoeAttackTimer = .50f;
    
    public bool LightAttack = false;
    public bool HeavyAttack = false;
    public bool AoeAttack = false;

    public bool LightSlash = false;
    public bool HeavySlash = false;
    public bool AoeSlash = false;

    public int AttackCount = 0;
    //use this for before initialization
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        
        WarriorBody = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        CharController();
        WarriorPowerUpSystem();
        WarriorCombatSystem();


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

    void WarriorPowerUpSystem()
    {
        //how the power ups affect the warrior
    }

    void WarriorCombatSystem()
    {
        //combat system


//---------determine the bools on key press----------------------------------------------------------------------------------------------------------
        //and play animations
        if (Input.GetKeyDown(KeyCode.E))
        {
            LightAttack = true;
            anim.Play("Light_slash", -1, 0f);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                HeavyAttack = true;
                anim.Play("Heavy_Slash", -1, 0f);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.T))
                { 
                    AoeAttack = true;
                    anim.Play("Aoe_Slash", -1, 0f);
                }
            }
        }

        if(LightAttack == true)
        {
            LightAttackTimer -= Time.deltaTime;
        }
        else
        {
            if(HeavyAttack == true)
            {
                HeavyAttackTimer -= Time.deltaTime;
            }
            else
            {
                if(AoeAttack == true)
                {
                    AoeAttackTimer -= Time.deltaTime;
                }
            }
        }
        //-----------------------Using the combat bools to use animations and start timers for all the animations---------------------------------------------------------------------
        if (LightAttack == true &&  LightAttackTimer <= 0 && AttackCount <=0)
        {
            LightSlash = true;
            AttackCount += 1;
        }
        else
        {
            if (HeavyAttack == true && HeavyAttackTimer <=0 && AttackCount <= 0)
            {
                HeavySlash = true;
                AttackCount += 1;
            }
            else
            {
                if(AoeAttack == true && AoeAttackTimer <=0 && AttackCount <= 0)
                {
                    AoeSlash = true;
                    AttackCount += 1;
                }
            }
        }
        if(LightAttack == true && AttackCount >=1 && LightAttackTimer <=0)
        {
            LightAttack = false;            AttackCount -= 1;
            LightAttackTimer = 1f;
            LightSlash = false;
            AttackCount -= 1;
        }
        else
        {
            if(HeavyAttack == true && AttackCount >=1 && HeavyAttackTimer <= 0)
            {
                HeavyAttack = false;
                HeavyAttackTimer= 1.43f;
                HeavySlash = false;
                AttackCount -= 1;
            }
            else
            {
                if(AoeAttack == true && AttackCount >=1 && AoeAttackTimer <= 0)
                {
                    AoeAttack = false;
                    AoeAttackTimer = .50f;
                    AoeSlash = false;
                    AttackCount -= 1;
                }
            }
        }
    }
}
