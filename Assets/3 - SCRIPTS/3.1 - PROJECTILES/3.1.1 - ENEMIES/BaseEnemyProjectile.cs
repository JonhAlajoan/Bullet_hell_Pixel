using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyProjectile : MonoBehaviour {

	protected LayerMask CollisionMask;
	public float speed;
	public float damage;
	public float lifetime;
	float skinWidth = .1f;
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

		Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, CollisionMask);
		if (initialCollisions.Length > 0)
		{
			//	OnHitObject(initialCollisions[0]);
		}
		homingTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		//playerPos = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent<Transform>();
	}

	public void SetSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	public abstract void Update();
		
}
