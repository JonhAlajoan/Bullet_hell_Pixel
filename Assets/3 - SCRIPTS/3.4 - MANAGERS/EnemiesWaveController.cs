using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesWaveController : MonoBehaviour {

	protected managerOfScene m_sceneManager;

	[SerializeField]
	protected int m_enemiesInChildren;

	void Start()
	{
		if (m_sceneManager == null)
			m_sceneManager = FindObjectOfType<managerOfScene>();
	}
	void Update()
	{
		m_enemiesInChildren = transform.childCount;
	
		if (m_enemiesInChildren <= 0)
		{
			m_sceneManager.changeStateOfCombat(false);
		}
	}
}
