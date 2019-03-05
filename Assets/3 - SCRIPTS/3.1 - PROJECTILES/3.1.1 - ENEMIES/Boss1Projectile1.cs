using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Projectile1 : BaseEnemyProjectile {

	// Use this for initialization
	void OnEnable ()
	{
		base.Initialize(3, 1, 3, 0, 10);
		
	}

	public override void Update()
	{
			
			count += 1 * Time.deltaTime;


			//homingTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

			if(count > 1)
			{
				speed += accelSpeed * Time.deltaTime;
			}

			if (count >= 5)
			{
				transform.rotation = new Quaternion(0, 0, 0, 0);
				UbhObjectPool.Instance.ReleaseGameObject(transform.parent.gameObject);
				count = 0;
			}

		
	}


}
