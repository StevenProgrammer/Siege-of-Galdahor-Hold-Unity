using UnityEngine;
using System.Collections;

public class Ogre : MonoBehaviour {


    public float Health = 500;

    public float TargetDistance;
    public float LookDistance;
    public float AttackDistance;
    public float ChaseDistance;
    public float Speed;
    public float Damping;
    public Transform Target;
    Rigidbody Rb;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
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

        if (TargetDistance < ChaseDistance && TargetDistance > AttackDistance)
        {

            transform.Translate(0, 0, 0.5f * Speed);
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsAttacking", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsDead", false);
            //anim.Play("run", -1, 0f);
            //Rb.AddForce(transform.forward * Speed);
        }
        else
        {
            if (TargetDistance < AttackDistance)
            {
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsAttacking", true);
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsDead", false);
            }


        }

        if (Health <= 0)
        {
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsAttacking", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsDead", true);
        }

    }
}
