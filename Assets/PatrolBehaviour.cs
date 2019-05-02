using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour {

	[SerializeField]
	protected GameObject m_enemy;

	protected float m_speed;

	private float waitTime;

	[SerializeField]
	protected float m_startWaitTime;

	protected Transform m_moveSpot;

	protected Vector2 m_points;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		m_speed = 3f;
		waitTime = m_startWaitTime;

		m_points = Random.insideUnitCircle * 10;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//CheckPositioning(animator);
	
		animator.transform.localPosition = Vector2.MoveTowards(animator.transform.localPosition,
																	 m_points * Mathf.Sin(Time.time * 0.2f) * 1f, m_speed * Time.deltaTime);		

		if(Vector2.Distance(animator.transform.localPosition, m_points) < 0.2f)
		{
			if(waitTime <= 0)
			{
				m_points = Random.insideUnitCircle * 5;
				waitTime = m_startWaitTime;
			}

			else
			{
				waitTime -= Time.deltaTime;
			}
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
