using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider colli)
    {
        // magic attacks
        if (colli.gameObject.tag == "LightSpell")
        {
            gameObject.GetComponent<WaterDemon>().Health -= 1.5f;
            gameObject.GetComponent<FlameDemon>().Health -= 1.5f;
            gameObject.GetComponent<EarthDemon>().Health -= 1.5f;
            gameObject.GetComponent<ShadowDemon>().Health -= 1.5f;
            gameObject.GetComponent<GoblinBasic>().Health -= 1.5f;
            gameObject.GetComponent<Skeleton>().Health    -= 1.5f;
            gameObject.GetComponent<DemonSoldier>().Health-= 1.5f;
        }
        else
        {
            if (colli.gameObject.tag == "AoeSpell")
            {
                gameObject.GetComponent<WaterDemon>().Health -= 5;
                gameObject.GetComponent<FlameDemon>().Health -= 5;
                gameObject.GetComponent<EarthDemon>().Health -= 5;
                gameObject.GetComponent<ShadowDemon>().Health -=5;
                gameObject.GetComponent<GoblinBasic>().Health -= 5;
                gameObject.GetComponent<Skeleton>().Health -= 5;
                gameObject.GetComponent<DemonSoldier>().Health -= 5;
            }
            else
            {
                if (colli.gameObject.tag == "HeavySpell")
                {
                    gameObject.GetComponent<WaterDemon>().Health -= 10;
                    gameObject.GetComponent<FlameDemon>().Health -= 10;
                    gameObject.GetComponent<EarthDemon>().Health -= 10;
                    gameObject.GetComponent<ShadowDemon>().Health -= 10;
                    gameObject.GetComponent<GoblinBasic>().Health -= 10;
                    gameObject.GetComponent<Skeleton>().Health    -= 10;
                    gameObject.GetComponent<DemonSoldier>().Health -= 10;
                }
            }
        }
        //archer attacks
        if (colli.gameObject.tag == "LightArrow")
        {
            // do this 
             gameObject.GetComponent<WaterDemon>().Health -= 2.3f;
             gameObject.GetComponent<FlameDemon>().Health -= 2.3f;
             gameObject.GetComponent<EarthDemon>().Health -= 2.3f;
             gameObject.GetComponent<ShadowDemon>().Health -= 2.3f;
             gameObject.GetComponent<GoblinBasic>().Health -= 2.3f;
             gameObject.GetComponent<Skeleton>().Health    -= 2.3f;
             gameObject.GetComponent<DemonSoldier>().Health -= 2.3f;
        }
            
        
        //priest attack
        if (colli.gameObject.tag == "HolyAttack")
        {
            //Strike 1
            // do this 
            gameObject.GetComponent<WaterDemon>().Health -= 1;
            gameObject.GetComponent<FlameDemon>().Health -= 1;
            gameObject.GetComponent<EarthDemon>().Health -= 1;
            gameObject.GetComponent<ShadowDemon>().Health -= 1;
            gameObject.GetComponent<GoblinBasic>().Health -= 1;
            gameObject.GetComponent<Skeleton>().Health -= 1;
            gameObject.GetComponent<DemonSoldier>().Health -= 1;
        }
        //warrior attacks
        if (colli.gameObject.tag == "LightAttack")
        {
            // do this 
            gameObject.GetComponent<WaterDemon>().Health -= 6.5f;
            gameObject.GetComponent<FlameDemon>().Health = 6.5f;
            gameObject.GetComponent<EarthDemon>().Health  = 6.5f;
            gameObject.GetComponent<ShadowDemon>().Health = 6.5f;
            gameObject.GetComponent<GoblinBasic>().Health = 6.5f;
            gameObject.GetComponent<Skeleton>().Health    = 6.5f;
            gameObject.GetComponent<DemonSoldier>().Health =6.5f;
        }
        
    }

    /*
    void OnTriggerStay(Collider colli)
    {
        //magic attacks
        if (colli.gameObject.tag == "AoeSpell")
        {
            gameObject.GetComponent<EnemyHealthSystem>().Health -= .5f;
        }

    }
    */
        }
