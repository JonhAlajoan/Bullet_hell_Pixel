using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour {

	public LayerMask collisionMask;
	float speed = 30;
	float damage = 5f;
	public Transform Hit_vfx;
	public bool firstHit = true;
	float lifetime = 3;
	float skinWidth = .1f;
	float count;
	Transform playerPos;


	void Start()
	{


		Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
		if (initialCollisions.Length > 0)
		{
		//	OnHitObject(initialCollisions[0]);
		}

		//playerPos = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent<Transform>();
	}

	public void SetSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	void Update()
	{
		count += 1 * Time.deltaTime;
		if (count >= 2)
		{
			transform.rotation = new Quaternion(0, 0, 0, 0);
			TrashMan.despawn(gameObject);
			count = 0;
		}
		float moveDistance = speed * Time.deltaTime;
		transform.Translate(Vector2.up* moveDistance);
	}

}

