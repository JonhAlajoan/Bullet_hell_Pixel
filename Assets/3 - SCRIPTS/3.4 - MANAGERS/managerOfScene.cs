using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class managerOfScene : MonoBehaviour
{

	
	public bool stateOfCombat; //State of combat that'll be used by other classes to detemine if the ship is OnCombat
	public string typeOfController;

	protected Animator m_animatorShip; //Main animator of the ship	

	public GameObject m_virtualCamera; //Virtual camera from cinemachine

	public CinemachineVirtualCamera m_combatCam;
    public CinemachineTargetGroup m_targetGroup;
	[SerializeField]
	protected Transform m_enemyFlockPosition;
    public GameObject canvas;

	private void Start()
	{
		m_animatorShip = FindObjectOfType<BaseShip>().GetComponent<Animator>();
		m_virtualCamera = FindObjectOfType<CinemachineVirtualCamera>().gameObject;

		if (!m_enemyFlockPosition)
			m_enemyFlockPosition = FindObjectOfType<EnemiesWaveController>().transform;

		if (!m_combatCam)
		{
			m_combatCam = GameObject.FindGameObjectWithTag("Vcams").transform.FindInChildren("Combate")
					.GetComponent<CinemachineVirtualCamera>();
		} 

		if (m_virtualCamera)
		{
			if (m_virtualCamera.name == "CameraExploring")
			{
				Debug.Log("The virtual camera is correct");
			}
			else
			{
				GameObject.FindGameObjectWithTag("CinemachineController").GetComponent<GameObject>();
			}
		}

        if (canvas)
            canvas.SetActive(false);
			
	}

	//This method change the state of combat whenever called (normally from a trigger)
	public void changeStateOfCombat(bool _stateOfCombat)
	{
		stateOfCombat = _stateOfCombat;

		//The virtual camera is the one with highest priority (normally the explorationCamera), if it gets deactivated the combat one assumes
		if (stateOfCombat == true)
		{
			m_virtualCamera.SetActive(false);
		}

		if (stateOfCombat == false)
		{
			m_virtualCamera.SetActive(true);
		}

		m_animatorShip.SetBool("isOnCombat", stateOfCombat);
	}

    void AddEnemiesToTargetGroup(GameObject _enemy)
    {
        m_targetGroup.m_Targets = new CinemachineTargetGroup.Target[0];
    }



	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S))
		{
			typeOfController = "Keyboard";
		}
		if(Input.GetAxis("JRightHorizontal") > 1)
		{
			typeOfController = "Joystick";
		}


        if(!m_enemyFlockPosition)
        {
            if (canvas)
                canvas.SetActive(true);
        }
           
        /*
		if (!m_combatCam)
		{
			m_combatCam = GameObject.FindGameObjectWithTag("Vcams").transform.FindInChildren("Combate")
					.GetComponent<CinemachineVirtualCamera>();
		}*/

        /*
		if (m_enemyFlockPosition)
			m_combatCam.m_LookAt = m_enemyFlockPosition;
		else if (m_enemyFlockPosition == null)
			m_enemyFlockPosition = FindObjectOfType<EnemiesWaveController>().transform;
			*/

    }
}
