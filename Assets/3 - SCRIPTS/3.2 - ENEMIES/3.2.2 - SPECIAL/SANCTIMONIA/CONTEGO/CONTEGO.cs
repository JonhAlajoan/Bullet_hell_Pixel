using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONTEGO : BaseEnemy{

	[SerializeField]
	protected GameObject m_shield;

	protected UbhShotCtrl m_shotControl;

	public override void Start()
	{
		m_count = 0;
		base.Start();
		base.Initialization(100, 30, 300, gameObject.GetComponent<Animator>());

		m_shield = transform.GetComponentInChildren<Contego_Shield>().gameObject;
		if (!m_shotControl)
			m_shotControl = GetComponent<UbhShotCtrl>();
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();

		if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
		{
			m_animator.SetBool("isMaximumCapacity", true);
		}

	}

	/*public void CreateShield()
	{
		GameObject _shield = TrashMan.spawn("CONTEGO_SHIELD", transform.position, transform.rotation);
		_shield.transform.parent = transform;
		_shield.transform.position = transform.localPosition;
		m_shield = _shield;
	}*/

	public override void Shoot(string _typeOfShot)
	{
		m_shotControl.StartShotRoutine();
	}
}
