using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Run2 : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 1f;
    //private GameObject playerG;

    Transform player;
    Rigidbody2D rb;
    Enemy enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //playerG = GameObject.FindGameObjectWithTag("Player");

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.Log("Player hilang");
        }

        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector2.Distance(enemy.transform.position, player.transform.position);
        Debug.Log(distance);

        if (distance < 6)
        {
            animator.SetBool("Run", true);
            enemy.LookAtPlayer();
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            animator.SetBool("Run", false);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.ResetTrigger("Attack");
    }
}
