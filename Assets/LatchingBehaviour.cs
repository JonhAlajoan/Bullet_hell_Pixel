using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatchingBehaviour : StateMachineBehaviour
{

	[SerializeField]
	protected float m_chasingDistance;
	[SerializeField]
	protected float m_retreatDistance;
	[SerializeField]
	protected float m_speed;

	protected float m_count;

	protected Transform m_enemyNearestPosition;

	[SerializeField]
	protected float m_aggroRange;

	protected managerOfScene m_sceneManager;

	protected Transform m_playerPosition;


	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		m_enemyNearestPosition = FindObjectOfType<BaseEnemy>().transform;

		if (m_sceneManager == null)
			m_sceneManager = FindObjectOfType<managerOfScene>();
		
		if (!m_playerPosition)
			m_playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


		//animator.transform.parent = null;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

		if (m_enemyNearestPosition == null)
			m_enemyNearestPosition = FindObjectOfType<BaseEnemy>().transform;

		if (Vector2.Distance(animator.transform.position, m_enemyNearestPosition.transform.position) > m_chasingDistance)
		{
			animator.transform.position = Vector2.MoveTowards(animator.transform.position, m_enemyNearestPosition.transform.position
				, m_speed * Time.deltaTime);
		}

		if (Vector2.Distance(animator.transform.position, m_enemyNearestPosition.transform.position) < m_retreatDistance)
		{
			animator.transform.position = Vector2.MoveTowards(animator.transform.position, m_enemyNearestPosition.transform.position
				, -m_speed * Time.deltaTime);
		}

		if (Vector2.Distance(animator.transform.position, m_enemyNearestPosition.transform.position) < m_chasingDistance
			&& Vector2.Distance(animator.transform.position, m_enemyNearestPosition.transform.position) > m_retreatDistance)
		{
			animator.transform.position = animator.transform.position;
		}


		else if (Vector2.Distance(animator.transform.position,m_playerPosition.transform.position) > m_chasingDistance * m_aggroRange)
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
}
