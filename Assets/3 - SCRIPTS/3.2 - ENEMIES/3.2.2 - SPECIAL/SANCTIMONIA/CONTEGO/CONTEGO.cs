using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONTEGO : BaseEnemy{

	[SerializeField]
	protected GameObject m_shield;

	public override void Start()
	{
		m_count = 0;
		base.Start();
		base.Initialization(20, 30, 300, gameObject.GetComponent<Animator>());

		m_shield = transform.GetComponentInChildren<Contego_Shield>().gameObject;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
		Debug.Log(m_count);
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
		TrashMan.spawn(_typeOfShot, transform.position, transform.rotation);
	}
}
