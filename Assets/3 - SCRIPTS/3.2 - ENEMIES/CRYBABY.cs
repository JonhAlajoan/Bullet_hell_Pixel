using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRYBABY : BaseEnemy {

	public UbhShotCtrl[] m_shotControl;

	public override void Start()
	{
		base.Start();
		base.Initialization(100, 100, gameObject.GetComponent<Animator>());
	}

	public override void Update()
	{
		base.Update();
	}

	public override void Shoot(string _typeOfShot)
	{
		foreach(UbhShotCtrl _shotcontrol in m_shotControl)
		{
			_shotcontrol.StartShotRoutine();
		}
		Debug.Log("Shooting crybaby: " + _typeOfShot);
	}
}
