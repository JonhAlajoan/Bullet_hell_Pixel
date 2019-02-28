using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTime : MonoBehaviour {
	public static int fixedFrameCount = 0;

	private void FixedUpdate()
	{
		fixedFrameCount++;
	}
}
