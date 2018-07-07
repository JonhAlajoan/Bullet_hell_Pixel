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
			OnHitObject(initialCollisions[0]);
		}

		playerPos = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent<Transform>();
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
			TrashMan.despawn(gameObject);
			count = 0;
		}
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions(moveDistance);
		transform.Translate(Vector2.up* moveDistance);
	}


	void CheckCollisions(float moveDistance)
	{
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide))
		{
			OnHitObject(hit);
		}
	}

	void OnHitObject(RaycastHit hit)
	{
		//IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
		//Transform enemyPos = hit.collider.GetComponent<Transform>();

		/*
		if (damageableObject != null)
		{
			Vector3 newPos = new Vector3(enemyPos.transform.position.x, playerPos.transform.position.y, enemyPos.transform.position.z);
			GameObject firstProjectile = TrashMan.spawn("ProjectileAlchemist", newPos, enemyPos.transform.rotation);
			GameObject secondProjectile = TrashMan.spawn("ProjectileAlchemist", newPos, enemyPos.transform.rotation);
			GameObject thirdProjectile = TrashMan.spawn("ProjectileAlchemist", newPos, enemyPos.transform.rotation);

			firstProjectile.transform.Rotate(gameObject.transform.rotation.x, -15, gameObject.transform.rotation.z);
			secondProjectile.transform.Rotate(gameObject.transform.rotation.x, 0, gameObject.transform.rotation.z);
			thirdProjectile.transform.Rotate(gameObject.transform.rotation.x, 15, gameObject.transform.rotation.z);

			TrashMan.spawn("hit_FirstAlchemist", gameObject.transform.position, gameObject.transform.rotation);
			damageableObject.TakeDamage(damage);


		}*/
		TrashMan.despawn(gameObject);

	}

	void OnHitObject(Collider c)
	{
		/*IDamageable damageableObject = c.GetComponent<IDamageable>();
		if (damageableObject != null)
		{
			damageableObject.TakeDamage(damage);
		}*/
		TrashMan.despawn(gameObject);
	}


}

