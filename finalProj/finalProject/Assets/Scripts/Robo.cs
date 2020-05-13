using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo : MonoBehaviour
{

    public float DefaultWalkSpeed = 2.5f;
    public float DefaultJumpHeight = 5f;

    public Transform CheckGTransform;
    public float groundRadCheck = 0.2f;
    public Transform targetTr;
    public LayerMask mouseTar;
    public LayerMask groundMask;
    //public GameObject playerModel;

    private float horMovement;
    private float verMovment;

    private Animator animator;
    private Rigidbody rbody;
    private bool isGrounded;
    private Camera mainCamera;
    public GameObject bulletPrefab;
    public Transform muzzleTransform;


    //calculate facing direction
    private int FacingSign
    {
        get
        {
            Vector3 perpen = Vector3.Cross(transform.forward, Vector3.forward);
            float dir = Vector3.Dot(perpen, transform.up);
            if (dir > 0f)
            {
                return 1;
            }
            else {
                if (dir < 0.0f) {
                    return -1;
                }
                else {
                    return 0;
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        horMovement = Input.GetAxis("Horizontal");
        verMovment = Input.GetAxis("Vertical");


        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseTar))
        {
            targetTr.position = hit.point;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, 0, 0);
            rbody.AddForce(Vector3.up * Mathf.Sqrt(DefaultJumpHeight * -1 * Physics.gravity.y), ForceMode.VelocityChange);
        }

        /*if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0) 
        {
            transform.rotation = Quaternion.Euler(0f, )
        }
        */
        if (Input.GetButtonDown("Fire1")) {
            Fire();
        }


    }

    public void Fire() {
        var shoot = Instantiate(bulletPrefab);
        shoot.transform.position = muzzleTransform.position;
        var bullet = shoot.GetComponent<Bullet>();
        bullet.Fire(shoot.transform.position, muzzleTransform.eulerAngles, gameObject.layer);
        /*var go = Instantiate(bulletPrefab);
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.x, aimDirection.y) * Mathf.Rad2Deg;
        muzzleTransform.eulerAngles = new Vector3(0,0,angle);
        Debug.Log(angle);*/
    }

    private void FixedUpdate()
    {
    	//Movement
    	rbody.velocity = new Vector3(horMovement * DefaultWalkSpeed, rbody.velocity.y, verMovment * DefaultWalkSpeed);
        animator.SetFloat("speed", (FacingSign * rbody.velocity.x) / DefaultWalkSpeed);

    	//Facing Rotation
    	rbody.MoveRotation(Quaternion.Euler(new Vector3(0, -1 * 90 * Mathf.Sign(targetTr.position.x - transform.position.x), 0)));

        //ground check
        isGrounded = Physics.CheckSphere(CheckGTransform.position, groundRadCheck, groundMask, QueryTriggerInteraction.Ignore);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, targetTr.position);
        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(targetTr.position);

    }

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "sphere") {
            Destroy(gameObject);
        }
    }
}
