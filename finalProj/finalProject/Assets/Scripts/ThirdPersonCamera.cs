﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

	[SerializeField]
	private float distanceAway;
	[SerializeField]
	private float distanceUp;
	[SerializeField]
	private float smooth;
	[SerializeField]
	private Transform follow;
	private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        follow = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate() 
    {
    	targetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;
    	Debug.DrawRay(follow.position, Vector3.up * distanceUp, Color.red);
    	Debug.DrawRay(follow.position, -1f * follow.forward * distanceAway, Color.blue);
    	Debug.DrawLine(follow.position, targetPosition, Color.magenta);

    	transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);

    	transform.LookAt(follow);
    }
}
