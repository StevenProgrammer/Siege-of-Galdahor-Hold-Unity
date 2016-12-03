using UnityEngine;
using System.Collections;

public class FlameDemon : MonoBehaviour
{




    public float Health = 2000;

    public Material Base;
    public Material Dead;

    public bool FightBegin;
    public bool FirstPhase;
    public bool SecondPhase;
    public bool LavaWrath;
    public bool EruptingRage;
    public bool FinalPhase;




    public GameObject BlockingArea1;
    public GameObject BlockingArea2;
    public GameObject Lava;
    public GameObject Eruption;
    public GameObject Meteor;
    public GameObject MeteorStorm;
    public GameObject FlameBreath;
    public GameObject LavaBreath;
    public GameObject RockPath;

    public Transform NewPos;

    public float TargetDistance;
    public float LookDistance;
    public float AttackDistance;
    public float ChaseDistance;
    public float Speed;
    public float Damping;

    //public Transform newspot;
    //public Transform[] Phase2;
    //public int CurrentPoint;

    public int cast = 0;
    public float lavaLife = 12f;

    public Transform Target;
    Rigidbody Rb;
    public Animator anim;
    public float vel;
    Vector3 dire;
    //timer for lava wrath
    //timer for erupting rage
    //timer for transitions

    // Use this for initialization
    void Start()
    {
        BlockingArea1.SetActive(false);
       BlockingArea2.SetActive(false);
        RockPath.SetActive(false);

    }

    void Awake()
    {
        BlockingArea1 = GameObject.Find("Flame Block");
        BlockingArea2 = GameObject.Find("Flame Block2");
        RockPath = GameObject.Find("RockPath");

        Lava = Resources.Load("MagamaTest") as GameObject;
        //Eruption = Resources.Load("") as GameObject;
        Meteor = Resources.Load("Meteo") as GameObject;
        MeteorStorm = Resources.Load("MeteorStorm") as GameObject;
        FlameBreath = Resources.Load("FireBreath") as GameObject;
        LavaBreath = Resources.Load("LavaBreath") as GameObject;


        anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();


    


    }



    // Update is called once per frame
    void Update()
    {
        if (FightBegin)
        {
            BlockingArea1.SetActive(true);
            BlockingArea2.SetActive(true);
        }



        if (Health <= 0)
        {
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsAttacking", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsDead", true);

            Destroy(BlockingArea1);
            Destroy(BlockingArea2);
            Destroy(gameObject, 1);

            //gameObject.GetComponent<Renderer>().material = Dead;
        }
        else

        {
            //gameObject.GetComponent<Renderer>().material = Base;
        }

    }


    void FixedUpdate()
    {
        Target = GameObject.FindGameObjectWithTag("Main_Player").transform;


        //calculates the distance between the ai and the active player target
        TargetDistance = Vector3.Distance(Target.position, transform.position);

        if (TargetDistance < LookDistance)
        {
            Vector3 direction = Target.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsAttacking", false);
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsDead", false);
        }

        if (TargetDistance < ChaseDistance && TargetDistance > AttackDistance && SecondPhase == false)
        {
            FightBegin = true;
            transform.Translate(0, 0, 0.5f * Speed);
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsAttacking", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsDead", false);
            //anim.Play("run", -1, 0f);
            //Rb.AddForce(transform.forward * Speed);
            FirstPhase = true;
        }
        else
        {
            if (TargetDistance < AttackDistance && Health >= 350 && FirstPhase)
            {
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsAttacking", true);
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsDead", false);
            }
            else
            {
                if (TargetDistance < AttackDistance && Health <= 200)
                {
                    anim.SetBool("IsRunning", false);
                    anim.SetBool("IsAttacking", false);
                    anim.SetBool("IsIdle", false);
                    anim.SetBool("IsDead", false);

                    FirstPhase = false;
                    SecondPhase = true;
                    vel = 5 * Time.deltaTime;
                    AttackSecond();
                }
            }



        }

    }


    void AttackSecond()
    {
        if (SecondPhase == true)
        {
            //move the boss to the new location
            Speed = 5;

            anim.SetBool("IsRunning", true);


            transform.position = new Vector3(12.73f, -14.4f, 76.18f);
            


            //health check
            if (Health <= 150)
            {
                SecondPhase = false;
                AttackThird();
                FinalPhase = true;

                
            }
        }

    }

    void AttackThird()
    {
        if (FinalPhase == true)
        {

        }
    }



    void OnTriggerEnter(Collider colli)
    {
        if (colli.gameObject.tag == "LightSpell")
        {
            Health -= 1.5f;
        }
        else
        {
            if (colli.gameObject.tag == "HeavySpell")
            {
                Health -= 5;
            }
            else
            {
                if (colli.gameObject.tag == "AoeSpell")
                {
                    Health -= 10;
                }
                else
                {
                    if (colli.gameObject.tag == "LightArrow")
                    {
                        Health -= 2.3f;
                    }
                    else
                    {
                        if (colli.gameObject.tag == "HolyAttack")
                        {
                            Health -= 1;
                        }
                        else
                        {
                            if (colli.gameObject.tag == "LightAttack")
                            {
                                Health -= 6.5f;
                            }
                        }
                    }
                }
            }
        }
    }
}








//when player is within range the combat shall begin

//as long as boss health is greater than 80% he will basically just punch the player with a few other basic things

//once boss health is between 80% and 40% the boss will roar and switch to his basic lava phase and occasionally spawn a meteor 
//once the lava spawns he will try to shoot the player with some fire breath

//once health is less than 40 then the boss will enter his final phase

//in his final phase the  lava will stay active phase and now he will  spew lava at the player instead of fire and his occational metor will now turn into a meteor storm type of ability
//the pillars will start to die now but now there will be some stuff to use as a walkway and occasionally there will be erruptions in the lava