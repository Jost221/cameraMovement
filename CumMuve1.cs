using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CumMuve1 : MonoBehaviour
{
    float mainSpeed = 10.0f; //regular speed
    float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 1000.0f; //Maximum speed when holdin gshift
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;
    bool switcher = true;

    public float x = -13.16f;//start rotation positions
    public float y = -41f;
    public float z = 0f;

    void Start()
    {
        UnityEngine.Cursor.visible = false;// cursor availability
    }

    void Update()
    {

        x += Input.GetAxis("Mouse X");
        y += Input.GetAxis("Mouse Y");

        transform.rotation = Quaternion.Euler(-y, x, z);

        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }

        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.Space))
        { //If player wants to move on X and Z axis only
            transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        else{transform.Translate(p);}

        

    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W)){p_Velocity += new Vector3(0, 0, 1);}
        if (Input.GetKey(KeyCode.S)){p_Velocity += new Vector3(0, 0, -1);}
        if (Input.GetKey(KeyCode.A)){p_Velocity += new Vector3(-1, 0, 0);}
        if (Input.GetKey(KeyCode.D)){p_Velocity += new Vector3(1, 0, 0);}
        return p_Velocity;
    }
}
