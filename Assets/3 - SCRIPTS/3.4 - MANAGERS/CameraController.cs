using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.PostProcessing;

public class CameraController : MonoBehaviour
{
	//Virtual camera that is being used in the scene. 
	CinemachineVirtualCamera m_vCam;

	//The post processing profile to be used
	protected PostProcessingProfile m_postProcessingProfile;

	private void Start()
	{
		//"Combate" is the name of the camera with combate mechanics
		m_vCam = GameObject.Find("Combate").GetComponent<CinemachineVirtualCamera>();
	}

	//This method stops all coroutines related to call another cameraShaker
	public void Shaker(float _amplitude, float _frequency, float _duration)
	{
		StopAllCoroutines();
		StartCoroutine(CameraShaker(_amplitude, _frequency, _duration));
	}

	//This method shakes the camera
	public IEnumerator CameraShaker(float _amplitude, float _frequency, float _duration)
	{
		//uses the amplitude and frequency from parameters to change the m_AmplitudeGain and m_FrequencyGain to cause the shaking
		m_vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = _amplitude;
		m_vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = _frequency;

		yield return new WaitForSeconds(_duration);

		//Stops shaking
		m_vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
		m_vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
	}

}
