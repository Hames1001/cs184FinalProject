using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere : MonoBehaviour
{
    public float speed = 10f;

    public Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start() {
        rigidBody = this.GetComponent<Rigidbody>();
    }



    public void OnCollisionEnter(Collision collisionInfo) {
        Destroy(gameObject);
    }
}
