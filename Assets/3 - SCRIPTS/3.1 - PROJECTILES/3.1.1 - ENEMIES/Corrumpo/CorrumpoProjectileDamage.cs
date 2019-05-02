using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrumpoProjectileDamage : BaseEnemyProjectile {

	public override void Update()
	{
		count += 1 * Time.deltaTime;

		if (count > 1)
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
