using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrumpoEggProjectile : BaseEnemyProjectile {

	protected UbhShotCtrl m_shotControl;
	protected float m_nextShotTime;
	protected float m_msBetweenShots;

    float count = 0;
    float speed = 20;

	private void Start()
	{
		base.Initialize(0,"");
		m_shotControl = GetComponent<UbhShotCtrl>();
		m_msBetweenShots = 6000;
	}

	public void Shoot()
	{
		if (Time.time > m_nextShotTime)
		{
			m_shotControl.StartShotRoutine();
			m_nextShotTime = Time.time + m_msBetweenShots / 1000;
		}
	}


	public override void Update()
	{
		transform.Translate(Vector2.down * speed * Time.deltaTime);
	
		count += 1 * Time.deltaTime;

		if(count>=1)
		{
			speed = 0;
			Shoot();
		}

		if (count >= 7)
		{
			transform.rotation = new Quaternion(0, 0, 0, 0);
			count = 0;
			speed = Random.Range(5, 10);
			TrashMan.despawn(gameObject);
			
		}

	}
}
