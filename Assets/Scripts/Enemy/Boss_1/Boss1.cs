using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : BaseBoss {

	private void Start()
	{
        base.Initialization(1000, 5, 100, gameObject.GetComponent<Animator>());
	}

	public override void Shoot()
	{
		
		if (Time.time > nextShotTime)
		{
			for (int i = 0; i < Muzzles.Length; i++)
			{
				//TrashMan.spawn("Bullet_1_Boss_1", Muzzles[i].position, Muzzles[i].rotation);
				//Debug.Log("i: " + i + "  " + Muzzles[i].rotation);
			}
			nextShotTime = Time.time + msBetweenShots / 1000;
		}
	}

}
