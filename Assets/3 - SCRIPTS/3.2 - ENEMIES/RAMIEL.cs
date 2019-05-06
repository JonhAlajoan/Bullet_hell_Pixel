using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAMIEL : BaseEnemy {

	// Use this for initialization

	public UbhShotCtrl[] m_shotControl;

	public override void Start()
	{
		base.Start();
		base.Initialization(100, 30, 100, gameObject.GetComponent<Animator>());
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}

	public override void Shoot(string _typeOfShot)
	{
		foreach (UbhShotCtrl _shotcontrol in m_shotControl)
		{
			_shotcontrol.StartShotRoutine();
		}

		Debug.Log("Shooting crybaby: " + _typeOfShot);
	}
}
