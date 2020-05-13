using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

	public Transform target;
	public Vector3 offset;
	public bool useOffsetValue;
	public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
    	if (!useOffsetValue) {
    		offset = target.position - transform.position;
    	}
        
    }

    // Update is called once per frame
    void Update()
    {
    	float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
    	target.Rotate(0, horizontal, 0);
        

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
    	target.Rotate(0, horizontal, 0);
    }
}
