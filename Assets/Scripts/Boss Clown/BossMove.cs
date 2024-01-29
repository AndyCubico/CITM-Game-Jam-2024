using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : StateMachineBehaviour
{
    private Boss _boss;

    [SerializeField] private float speed;
    private float _timer;
    private int _target = 0;
    private Vector2[] _points = { new Vector2(0, 0), new Vector2(-5.78f, 2.74f), new Vector2(6.63f, 2.57f),
        new Vector2(5.78f, -0.72f), new Vector2(-6.08f, -0.73f)};//Hardcoded :)

    [SerializeField] private float _minSpeed = 0.5f;
    [SerializeField] private float _acceleration = 0.01f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.GetComponent<Boss>();

        int aux = Random.Range(0, _points.Length);

        while (aux == _target)//check to avoid the current point
        {
            aux = Random.Range(0, _points.Length);
        }

        speed = 10;

        _target = aux;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (speed >= _minSpeed && Vector2.Distance(animator.transform.position, _points[_target]) < 5f)
        {
            speed -= _acceleration;
        }
        
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, _points[_target], speed * Time.deltaTime);

        if (Vector2.Distance(animator.transform.position, _points[_target]) < 0.1f)
        {
           animator.SetTrigger("Exit");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
