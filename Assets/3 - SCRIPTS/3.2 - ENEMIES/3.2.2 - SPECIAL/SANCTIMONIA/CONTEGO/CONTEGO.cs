using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONTEGO : BaseEnemy{

    //The shot controller of the enemy from Uni Bullet hell
	protected UbhShotCtrl m_shotControl;

	public override void Start()
	{
		m_count = 0;
		base.Start();
	    Initialization(100, 300, gameObject.GetComponent<Animator>());

		if (!m_shotControl)
			m_shotControl = GetComponent<UbhShotCtrl>();
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();

        //If the shield is complete, set the animator parameter to true
		if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
		{
			m_animator.SetBool("isMaximumCapacity", true);
		}

	}

    //method that'll be invoked on animation
	public override void Shoot(string _typeOfShot)
	{
		m_shotControl.StartShotRoutine();
	}
}
