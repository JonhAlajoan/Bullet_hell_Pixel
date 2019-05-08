using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviourStrafePlayer : StateMachineBehaviour {

    /*--------------This script is used on animator states as the A.I that chases the player if they're distant and 
                    strafes back if the player is approaching them-----------------------------------*/

	[SerializeField]
	protected float m_chasingDistance; //If this value is higher than the distance between the player and enemy
	[SerializeField]
    //If this value is lesser than the distance between the player and the enemy 
	protected float m_retreatDistance;
	[SerializeField]

	public float speed; //speed of the enemy that'll pursue the player

	protected float m_count; //counter 

	protected Transform m_playerPosition;

	[SerializeField]
	protected float m_aggroRange; //The m_distance will be multiplied by this range to define when the aggro is lost

	protected managerOfScene m_sceneManager;

	protected float m_accel; //Acceleration (For lancer)


	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		m_playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

		if (m_sceneManager == null)
			m_sceneManager = FindObjectOfType<managerOfScene>();
	}


	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        //For the lancer to work, every instance of it must be named in capital letters "LANCER"
        #region Lancer
        if (animator.name == "LANCER")
        {
            if (m_accel >= 4)
			{
				speed += m_accel * 3;
				m_accel = 1;
			}
            
			//gets the boolean "isAttached" from the lancer class. The attachment will always be true if the lancer contacted the player
			if(animator.GetComponent<LANCER>().isAttached == true)
			{
                //Change the enemy parent, being it now the playerShip
				animator.transform.parent = m_playerPosition.transform;
				animator.rootPosition = new Vector3(0, 0, 0);
                //disable own collider for preventing bugs purpose
				animator.GetComponent<Collider2D>().enabled = false;
				m_accel = 0;
			} 

            //IF the lancer isn't attached to player...
			if(animator.GetComponent<LANCER>().isAttached == false)
			{ 
                /*I don't know how this works, but basically if you put the .right of your object to be the subtraction of target position
                minus its own position, the object will rotate for that direction*/
				Vector3 _rot = animator.transform.right = m_playerPosition.position - animator.transform.position;
				animator.rootPosition = _rot;

				m_accel += 2f * Time.deltaTime;

                //if the distance between the enemy and the player is greater than the m_chasingDistance, move towards the player
				if (Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) > m_chasingDistance)
				{
					animator.transform.position = Vector2.MoveTowards(animator.transform.position, m_playerPosition.transform.position
						, speed * Time.deltaTime);
				}
                //if the distance between the enemy and the player is lesser than the m_retreatDistance, move towards the player
                if (Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) < m_retreatDistance)
				{
					animator.transform.position = Vector2.MoveTowards(animator.transform.position, m_playerPosition.transform.position
						, -speed * Time.deltaTime);
				}

                //if it is between the two above, do nothing;   
				if (Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) < m_chasingDistance
					&& Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) > m_retreatDistance)
				{
					animator.transform.position = animator.transform.position;
				}

                //This is the controller that sets the enemy back to patrolling position the distance between player and enemy is greater than chasing distance time aggroRange
				else if (Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) > m_chasingDistance * m_aggroRange)
				{
                    //setting the animator parameters
					animator.SetBool("isPatrolling", true);
					animator.SetBool("isAttacking", false);
                    //setting the stateofCombat to the scene manager
					m_sceneManager.changeStateOfCombat(false);

				}
			}
		}
        #endregion

        #region Chase/Strafe Code

        if (Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) > m_chasingDistance)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, m_playerPosition.transform.position
                , speed * Time.deltaTime);
        }
        //if the distance between the enemy and the player is lesser than the m_retreatDistance, move towards the player
        if (Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) < m_retreatDistance)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, m_playerPosition.transform.position
                , -speed * Time.deltaTime);
        }

        //if it is between the two above, do nothing;   
        if (Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) < m_chasingDistance
            && Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) > m_retreatDistance)
        {
            animator.transform.position = animator.transform.position;
        }

        //This is the controller that sets the enemy back to patrolling position the distance between player and enemy is greater than chasing distance time aggroRange
        else if (Vector2.Distance(animator.transform.position, m_playerPosition.transform.position) > m_chasingDistance * m_aggroRange)
        {
            //setting the animator parameters
            animator.SetBool("isPatrolling", true);
            animator.SetBool("isAttacking", false);
            //setting the stateofCombat to the scene manager
            m_sceneManager.changeStateOfCombat(false);

        }

        #endregion
        if (m_playerPosition == null)
			m_playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
	}

	
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        //OnstateExit, reset the hp of the enemy
		animator.GetComponent<BaseEnemy>().resetHp();
	}
	
}
