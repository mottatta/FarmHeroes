using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    float speed = 10.0f;

    public Transform downSpot, upSpot, leftSpot, rightSpot;
 
void Update () {
        return;
	if (Input.GetMouseButton (0)) {
		if (Input.GetAxis ("Mouse X") > 0) {
			transform.position -= new Vector3 (Input.GetAxisRaw ("Mouse X") * Time.deltaTime * speed, 
	                                   0.0f, Input.GetAxisRaw ("Mouse Y") * Time.deltaTime * speed);
		}
 
		else if (Input.GetAxis ("Mouse X") < 0) {
			transform.position -= new Vector3 (Input.GetAxisRaw ("Mouse X") * Time.deltaTime * speed, 
	                                   0.0f, Input.GetAxisRaw ("Mouse Y") * Time.deltaTime * speed);
		}

            if (Input.GetAxis("Mouse Y") > 0)
            {
                //Debug.Log("scroll up, cam goes down");
                Vector3 newPos = transform.position - new Vector3(0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0f);
                newPos = GetValidPosition(newPos);
                if (newPos.y <= upSpot.position.y) transform.position = newPos;
            }

            else if (Input.GetAxis("Mouse Y") < 0)
            {
                Debug.Log("scroll down, cam goes up");
                Vector3 newPos = transform.position - new Vector3(0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0f);
                newPos = GetValidPosition(newPos);
                if (newPos.y >= downSpot.position.y) transform.position = newPos;
                //transform.position -= new Vector3(0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0f);
            }
        }
}

    Vector3 GetValidPosition(Vector3 vec)
    {
        return new Vector3(
             Mathf.Clamp(vec.x, leftSpot.position.x, rightSpot.position.x),
             Mathf.Clamp(vec.y, downSpot.position.y, upSpot.position.y),
             -5);
    }
}
