using UnityEngine;
using System.Collections;

public class ShadowDemon : MonoBehaviour
{

    public bool FightBegin;
    public bool Invulnerable = false;

    public bool Phase1;
    public bool Phase2;

    bool MaxRelic;
    bool LessRelic;
    bool NoRelic;

    public int CastCount;
    public GameObject[] Relics;
    public float Health = 2000;
    public float weakened = 45;
    public float weakenedv2 = 45;
    //phase1
    public GameObject Demons;
    public GameObject ShadowTrap;
    //constant
    public GameObject DarkMist;
    //phase2
    public GameObject ShadowCyclone;
    public GameObject ShadowBall;

    Rigidbody Rb;
    public Animator anim;
    public float TargetDistance;
    public float LookDistance;
    public float ComabatDistance;
    public Transform Target;


    // Use this for initialization
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Main_Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Relics = GameObject.FindGameObjectsWithTag("relic");

        if (Relics.Length == 4)
        {

            MaxRelic = true;
            Invulnerable = true;


        }
        else
        {
            if(Relics.Length == 3)
            {
                weakened = Time.deltaTime;

                if(weakened >= 0)
                {

                    Invulnerable = false;
                }
                else
                {

                    Invulnerable = true;
                }
            }
            else
            {
                if (Relics.Length == 2)
                {
                    LessRelic = true;

                    weakenedv2 = Time.deltaTime;

                    if (weakenedv2 >= 0)
                    {

                        Invulnerable = false;
                    }
                    else
                    {

                        Invulnerable = true;
                    }

                }
                else
                {
                    if (Relics.Length == 0)
                    {
                        NoRelic = false;

                        Invulnerable = false;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {

        TargetDistance = Vector3.Distance(Target.position, transform.position);

        if (TargetDistance < LookDistance)
        {
            FightBegin = true;

            //blocking system
            //CaveIn.SetActive(true);

            Vector3 direction = Target.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }

        if (TargetDistance < ComabatDistance && FightBegin)
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
