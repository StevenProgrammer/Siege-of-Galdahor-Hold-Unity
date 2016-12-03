using UnityEngine;
using System.Collections;

public class WaterDemon : MonoBehaviour
{

    int MovePos;
    public float Health = 2000;
    public bool FightStart;
    public GameObject[] RisePoints;
    public GameObject Rain;
    public GameObject LightningStorm;
    public GameObject LightningStrike;
    public GameObject LightningPillar;
    public GameObject WaterOrb;
    public GameObject CaveIn;


    public GameObject Water;
    public int CastCount;
    public int LightningCount;
    public int PillarCount;
    public float UpTime = 15f;
    public float CastDelay = 2;
    public float WaterRiseTimer = 4;
    public Transform Target;
    public GameObject WaterBall;
    Rigidbody Rb;
    public Animator anim;

    public bool WaterOrbAttack;
    public bool LightningStormAttack;
    public bool LightningStrikeAttack;
    public bool LightningPillarAttack;
    public bool Raining;

    GameObject StormSpawn;
    public float TargetDistance;
    public float LookDistance;
    public float ComabatDistance;

    // Use this for initialization
    void Start()
    {
        WaterBall = GameObject.Find("WaterBall_Spawn");
        RisePoints = GameObject.FindGameObjectsWithTag("WaterBossSpawn");
        ComabatDistance = 85;
        StormSpawn = GameObject.Find("StormSpawn");
        Water = GameObject.Find("WaterBossWater");
    }



    void Awake()
    {


        Target = GameObject.FindGameObjectWithTag("Main_Player").transform;
        CaveIn.SetActive(false);


        Rain = Resources.Load("Rain Storm") as GameObject;
        //CaveIn = GameObject.Find("Rock19_A (1)");
        LightningStorm = Resources.Load("LightningStorm") as GameObject;
        LightningStrike = Resources.Load("LightningStrike") as GameObject;
        LightningPillar = Resources.Load("LightningPillar") as GameObject;
        WaterOrb = Resources.Load("WaterOrb") as GameObject;


        anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Health <= 0)
        {
            Destroy(CaveIn);
            Destroy(gameObject);
        }
        if (WaterOrbAttack)
        {

            CastDelay = 2;
            GameObject OrbAttack = Instantiate(WaterOrb) as GameObject;
            OrbAttack.transform.position = WaterBall.transform.position;
            Rigidbody BS = OrbAttack.GetComponent<Rigidbody>();
            BS.velocity = gameObject.transform.forward * 40;
            CastCount += 1;
            WaterOrbAttack = false;

        }

        if (LightningPillarAttack)
        {

            GameObject Pillar = Instantiate(LightningPillar) as GameObject;
            //Pillar.transform.position =

            PillarCount += 1;
            CastDelay = 2;
            LightningPillarAttack = false;
        }

        if (LightningStormAttack)
        {
            CastCount += 1;
            CastDelay = 2;
            Instantiate(LightningStorm, StormSpawn.transform.position, Quaternion.identity);
            LightningStormAttack = false;
        }

        if (LightningStrikeAttack)
        {
            CastDelay = 2;
            Instantiate(LightningStrike, gameObject.transform.position + gameObject.transform.forward * 2, Quaternion.identity);

            CastCount += 1;
            LightningStrikeAttack = false;
        }

        if (Raining)
        {


            CastDelay = 2;
            Instantiate(Rain, StormSpawn.transform.position, Quaternion.identity);
            //affects the water
            Water.transform.position = new Vector3(Water.transform.position.x, 11, Water.transform.position.z);
            CastCount += 1;
            WaterRiseTimer -= Time.deltaTime;

        }

        if (WaterRiseTimer <= 0)
        {
            Raining = false;
            Water.transform.position = new Vector3(Water.transform.position.x, 8.45f, Water.transform.position.z);

        }

    }

    void FixedUpdate()
    {

        TargetDistance = Vector3.Distance(Target.position, transform.position);

        if (TargetDistance < LookDistance)
        {
            FightStart = true;
            CaveIn.SetActive(true);

            Vector3 direction = Target.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }

        if (TargetDistance < ComabatDistance && FightStart)
        {
            UpTime -= Time.deltaTime;
            CastDelay -= Time.deltaTime;

            //as long as he hasnt attacked 4 times he can do this

            if (CastCount < 5)
            {
                int n = Random.Range(0, 10);

                if (n >= 9 && PillarCount <= 1 && CastDelay <= 0)
                {
                    LightningPillarAttack = true;
                }
                else
                {
                    if (n == 2 && CastDelay <= 0)
                    {
                        Raining = true;
                        WaterRiseTimer = 4;
                    }
                    else
                    {
                        if (n >= 7 && n <= 9 && CastDelay <= 0)
                        {
                            WaterOrbAttack = true;
                        }
                        else
                        {
                            if (n < 7 && n >= 2 && CastDelay <= 0)
                            {
                                LightningStormAttack = true;
                            }
                            else
                            {
                                if (n <= 1 && CastDelay <= 0)
                                {
                                    LightningStrikeAttack = true;
                                }

                            }
                        }
                    }
                }
            }
        }

        if (UpTime <= 0)
        {
            int Spawn = Random.Range(0, (RisePoints.Length - 1));
            transform.position = RisePoints[Spawn].transform.position;
            UpTime = 15;
            CastCount = 0;
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
