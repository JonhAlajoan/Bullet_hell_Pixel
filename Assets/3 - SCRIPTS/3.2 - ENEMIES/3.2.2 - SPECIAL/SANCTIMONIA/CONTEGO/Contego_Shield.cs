using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contego_Shield : BaseEnemy {

	protected bool m_isMaximumCapacity;

	public override void Shoot(string _typeOfBullets)
	{
		throw new System.NotImplementedException();
	}

	// Use this for initialization
	public override void Start()
	{
		base.Start();
		base.Initialization(200, 0, 0, transform.parent.GetComponent<Animator>());
		m_isMaximumCapacity = false;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
		m_isMaximumCapacity = m_animator.GetBool("isMaximumCapacity");
		ShieldGrowth();
	}

	public void ShieldGrowth()
	{
		if(m_isMaximumCapacity == false)
			m_health += Time.deltaTime * 28.58f;
	}

	
}
