using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float velocity = 15f;
    public float life = 1f;

    private int fireByLayer;
    private float existanceLen;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, velocity * Time.deltaTime, ~(1<< fireByLayer)))
        {
            transform.position = hit.point;
            Vector3 reflected = Vector3.Reflect(transform.forward, hit.normal);
            Vector3 direction = transform.forward;
            Vector3 reflectedProjOnPlane = Vector3.ProjectOnPlane(reflected, Vector3.forward);
            transform.forward = reflectedProjOnPlane;
            transform.rotation = Quaternion.LookRotation(reflectedProjOnPlane, Vector3.forward);
            Hit(transform.position, direction, reflected, hit.collider);
            //transform.position = hit.point;
            //Hit(transform.position, transform.forward, hit.collider);
        }
        else {
            transform.Translate(Vector3.forward*velocity*Time.deltaTime);
        }
        if (Time.time > existanceLen + life) {
            Destroy(gameObject);

        }

    }
    public void Hit(Vector3 position, Vector3 direction, Collider collider)
    {
        Destroy(gameObject);
    }
    //what bullet does once it hits
    public void Hit(Vector3 position, Vector3 direction,Vector3 reflected,Collider collider ) {
        Destroy(gameObject);
    }

    public void Fire(Vector3 position,/*float*/ Vector3 euler, int layer) {
        existanceLen = Time.time;
        transform.position = position;
        transform.eulerAngles = euler;
        //transform.eulerAngles = new Vector3(0,0,euler);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 vop = Vector3.ProjectOnPlane(transform.forward,Vector3.forward);
        transform.forward = vop;
        transform.rotation = Quaternion.LookRotation(vop, Vector3.forward);
    }
}
