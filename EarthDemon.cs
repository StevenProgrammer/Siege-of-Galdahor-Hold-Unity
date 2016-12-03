using UnityEngine;
using System.Collections;

public class EarthDemon : MonoBehaviour {

    public float Health = 2000;

    public float TargetDistance;
    public float LookDistance;
    public float AttackDistance;
    public float ChaseDistance;
    public float Speed;
    public float Damping;
    public Transform Target;
    Rigidbody Rb;
    public Animator anim;

    public bool FightBegin;
    public bool AttackPhase;
    public bool MinionPhase;

    public GameObject Minion;
    public GameObject MinionStrong;
    public GameObject Dome;

    public float Uptime = 75f;
    public float DomeTimer = 50f;

    public int DomeCount;
    public int CastCount;
    public int SpawnCount;

    public int MinionCount;
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(MinionPhase == true && DomeCount <=0)
        {
            Instantiate(Dome, gameObject.transform.position, Quaternion.identity);
            DomeTimer -= Time.deltaTime;
            
        }


        if (DomeTimer <= 0 || MinionCount <= 0)
        {
            Destroy(Dome);
            MinionPhase = false;
            AttackPhase = true;
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

        if (TargetDistance < ChaseDistance && TargetDistance > AttackDistance && MinionPhase == false)
        {
            FightBegin = true;
            transform.Translate(0, 0, 0.5f * Speed);
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsAttacking", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsDead", false);
            //anim.Play("run", -1, 0f);
            //Rb.AddForce(transform.forward * Speed);
            AttackPhase = true;
        }

        else
        {
            if (TargetDistance < AttackDistance && AttackPhase)
            {
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsAttacking", true);
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsDead", false);

                Uptime -= Time.deltaTime;
                DomeTimer = 50f;
                Speed = 5;

                //do attacks

                if (AttackPhase && Uptime <= 0)
                {
                    AttackPhase = false;
                    MinionPhase = true;
                }
            }
            else
            {
                if (MinionPhase && DomeCount <=0)
                {
                    anim.SetBool("IsRunning", false);
                    anim.SetBool("IsAttacking", false);
                    anim.SetBool("IsIdle", false);
                    anim.SetBool("IsDead", false);

                    Instantiate(Dome, gameObject.transform.position, Quaternion.identity);


                    DomeTimer -= Time.deltaTime;
                    Uptime = 75f;
                    Speed = 0;

                    if  ((MinionPhase && DomeTimer <= 0 )||( MinionPhase && MinionCount <= 0))
                    {
                        Destroy(Dome);

                        AttackPhase = true;
                        MinionPhase = false;
                    }
                }
            }



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
