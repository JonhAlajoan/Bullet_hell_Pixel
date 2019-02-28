using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	protected GameObject Crosshair;

	Vector3 mousePosition;
	
    
	void LateUpdate ()
	{
		Cursor.visible= false;
		mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		Crosshair.transform.position = Vector2.Lerp(transform.position, mousePosition, 300f * Time.deltaTime);

		Crosshair.transform.Rotate(0,0,180 * Time.deltaTime,0);
	}

	


}
