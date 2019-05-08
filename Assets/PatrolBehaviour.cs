using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour {

    //----------Script to be put on animatorState that makes the enemies patrol a certain radius---------

    
	protected float m_speed; //Speed of the enemy

	private float waitTime; //This variable will serve as controle to when the enemy should move

	[SerializeField]
	protected float m_startWaitTime; //the default wait time

	protected Vector2 m_points;  //The points that the enemy will move while patrolling

    protected Transform m_playerPosition; //The position of the player

	[SerializeField]
	protected float m_distanceToPlayer; //Basically the Aggro range, if the distance between the player and enemy are lesser than m_distance the combat will be true

	protected managerOfScene m_sceneManager; //The manager of the scene 


	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        //Ensure that the sceneManager is found
		if (m_sceneManager == null)
			m_sceneManager = FindObjectOfType<managerOfScene>();

		m_speed = 3f;
		waitTime = m_startWaitTime;

        //The points are random points inside a circle with 10 radius
		m_points = Random.insideUnitCircle * 10;

      
		if (m_playerPosition == null)
			m_playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
	}

	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{	
        //This move the enemy to the points with a Sin Wave
		animator.transform.localPosition = Vector2.MoveTowards(animator.transform.localPosition,
																	 m_points * Mathf.Sin(Time.time * 0.2f) * 1f, m_speed * Time.deltaTime);		


        //if it is getting closer to the point
		if(Vector2.Distance(animator.transform.localPosition, m_points) < 0.2f)
		{
            //if the waitTime < 0; Call a new point; Else; wait;
			if(waitTime <= 0)
			{
				m_points = Random.insideUnitCircle * 10;
				waitTime = m_startWaitTime;
			}

			else
			{
				waitTime -= Time.deltaTime;
			}
		}

        //if the distance between the player and the enemy is lesser than m_distanceToPlayer
		if(Vector2.Distance(animator.transform.position, m_playerPosition.position) < m_distanceToPlayer)
		{
            //set the animator parameters
			animator.SetBool("isAttacking", true);
			animator.SetBool("isPatrolling", false);
            //set the state of combat
			m_sceneManager.changeStateOfCombat(true);
		}

		
	}

}
