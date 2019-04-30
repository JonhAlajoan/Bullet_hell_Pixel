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

	protected float m_startX;
	protected float m_startY;


	[SerializeField]
	protected float m_minX;
	[SerializeField]
	protected float m_maxX;

	[SerializeField]
	protected float m_minY;
	[SerializeField]
	protected float m_maxY;
	Vector2 points;

	public void CheckPositioning(Animator _animator)
	{
		if (_animator.transform.position.x > m_maxX || _animator.transform.position.y > m_maxY)
		{
			Mathf.Clamp(_animator.transform.position.x, m_startX, m_maxX);
		}
	}

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

		m_startX = animator.transform.position.x;
		m_startY = animator.transform.position.y;

		m_speed = 10f;

		m_maxX += m_startX;
		m_maxY += m_startY;

		waitTime = m_startWaitTime;	
		points = new Vector2(animator.transform.position.x + Random.Range(m_minX, m_maxX),
										 animator.transform.position.y + Random.Range(m_minY, m_maxY));
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		CheckPositioning(animator);
	
		animator.transform.position = Vector2.MoveTowards(animator.transform.position,
																	 points, m_speed * Time.deltaTime);		

		if(Vector2.Distance(animator.transform.position, points) < 0.2f)
		{
			if(waitTime <= 0)
			{
				points = new Vector2(animator.transform.position.x + Random.Range(m_minX, m_maxX),
										 animator.transform.position.y + Random.Range(m_minY, m_maxY));
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
