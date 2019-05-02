using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviourStrafePlayer : StateMachineBehaviour {

	[SerializeField]
	protected float m_chasingDistance;
	[SerializeField]
	protected float m_retreatDistance;
	[SerializeField]
	protected float m_speed;

	protected float m_count;

	protected Transform m_playerPosition;

	[SerializeField]
	protected float m_aggroRange;

	protected managerOfScene m_sceneManager;


	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		m_playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

		if (m_sceneManager == null)
			m_sceneManager = GameObject.FindObjectOfType<managerOfScene>();
			
		//animator.transform.parent = null;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

		if(m_playerPosition == null)
			m_playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

		if (Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) > m_chasingDistance)
		{
			animator.transform.position = Vector2.MoveTowards(animator.transform.position, m_playerPosition.transform.position
				, m_speed * Time.deltaTime);
		}		

		if (Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) < m_retreatDistance)
		{
			animator.transform.position = Vector2.MoveTowards(animator.transform.position, m_playerPosition.transform.position
				, -m_speed * Time.deltaTime);
		}

		if (Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) < m_chasingDistance
			&& Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) > m_retreatDistance)
		{
			animator.transform.position = animator.transform.position;
		}


		else if (Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) > m_chasingDistance * m_aggroRange)
		{
		
			animator.SetBool("isPatrolling", true);
			animator.SetBool("isAttacking", false);
			m_sceneManager.changeStateOfCombat(false);

		}



	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.GetComponent<BaseEnemy>().resetHp();
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	
}
