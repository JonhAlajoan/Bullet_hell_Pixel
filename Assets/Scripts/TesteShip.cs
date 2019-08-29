using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteShip : MonoBehaviour {

    public Transform target;
    public float speed;

    float mousePosY,mousePosX;
    bool isOnCombat;
    Screen screenManager;
    void Start()
    {
        isOnCombat = true;
        Cursor.visible = false;
    }

    private void SetStickCommandsUsingMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        // Figure out most position relative to center of screen.
        // (0, 0) is center, (-1, -1) is bottom left, (1, 1) is top right.      
        mousePosY = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
        mousePosX = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        // Make sure the values don't exceed limits.
        mousePosY = Mathf.Clamp(mousePosY, -1.0f, 1.0f);
        mousePosX = Mathf.Clamp(mousePosX, -1.0f, 1.0f);
    }

    void lookAt()
    {
        Vector3 mouseTarget = new Vector3(mousePosX, mousePosY, 0);
        transform.LookAt(target);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        SetStickCommandsUsingMouse();
        lookAt();
        playerMovement();
       
        if (Input.GetKey(KeyCode.R) && target == null)
            target = GameObject.FindObjectOfType<BaseEnemy>().transform;
        if (Input.GetKey(KeyCode.R) && target != null)
        {
            isOnCombat = false;
            target = null;
        }
            

        
    }

    void playerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
      

        if (isOnCombat)
        {
            Vector3 playermove = new Vector3(hor,vert,0f) * speed * Time.deltaTime;
            transform.Translate(playermove, Space.Self);
        }

        else
        {
            Vector3 playermove = new Vector3(hor, 0f, vert) * speed * Time.deltaTime;
            transform.Translate(playermove, Space.Self);
           
            transform.Rotate(new Vector3(mousePosX, mousePosY, 0), Space.Self);
        }
        
       
    }
}

