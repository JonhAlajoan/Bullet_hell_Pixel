using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyProjectile : MonoBehaviour {

	public float speed;
	public float damage;
	public float lifetime;
	public float count;
	Transform playerPos;
	bool LockOn;
	public float angle;
	public float accelSpeed;
	public Transform homingTarget;


	public void Initialize(float speed, float damage, float lifetime, float angle, float accelSpeed)
	{
		this.speed = speed;
		this.damage = damage;
		this.lifetime = lifetime;
		this.angle = angle;
		this.accelSpeed = accelSpeed;
	}

	void Start()
	{
		count = 0;
		homingTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	public void SetSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<BaseShip>() != null)
		{
			BaseShip player = collision.gameObject.GetComponent<BaseShip>();
			player.TakeDamage(damage);
			TrashMan.spawn("VFX_HIT_PLAYER", transform.position, transform.rotation);
			count = 0;
			transform.rotation = new Quaternion(0, 0, 0, 0);
			UbhObjectPool.Instance.ReleaseGameObject(transform.parent.gameObject);
		}


	}
	public abstract void Update();
		
}
