using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    //1
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;

    public GameObject bullet;
    public float bulletSpeed = 100f;
    private float vInput;
    private float hInput;

    //1
    private Rigidbody _rb;
    // Start is called before the first frame update
    //2
    private CapsuleCollider _col;
    private bool doJump = false;
    private bool doShoot = false;

    void Start()
    {
        //3
        _rb = GetComponent<Rigidbody>();
        //4
        _col = GetComponent<CapsuleCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        //3
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        //4
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;
        /*4
        this.transform.Translate(Vector3.forward * vInput Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            doJump = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            doShoot = true;
        }

    }


    void FixedUpdate()
    {
        if (doJump)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            doJump = false;
        }

        if (doShoot)
        {
            // NOTE: original code adds a world space offset of 1 unit in the x direction, so as you turn, origin of bullet moves wrt player
            //GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;
            GameObject newBullet = Instantiate(bullet, this.transform.position + this.transform.right, this.transform.rotation) as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward * bulletSpeed;
            doShoot = false;
        }
        //2
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            Debug.Log("jumping");
        }
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;
            //4
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            //5
            bulletRB.velocity = this.transform.forward * bulletSpeed;
            //3
        }
        Vector3 rotation = Vector3.up * hInput;
        Quaternion anglerot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * anglerot);
        //5 
    }
    //6
    private bool IsGrounded()
    {
        //7 
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        //8
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        //9
        return grounded;
    }

    private float speedMultiplier;

    public void BoostSpeed(float multiplier, float seconds)
    {
        speedMultiplier = multiplier;
        moveSpeed *= multiplier;
        Invoke("EndSpeedBoost", seconds);
    }

    private void EndSpeedBoost()
    {
        Debug.Log("speed boost ended");
        moveSpeed /= speedMultiplier;
    }

    private float jumpMultiplier;

    public void JumpBoost(float multiplier, float seconds)
    {
        jumpMultiplier = multiplier;
        jumpVelocity *= multiplier;
        Invoke("EndJumpBoost", seconds);
    }

    private void EndJumpBoost()
    {
        Debug.Log("Jump Boost Ended");
        jumpVelocity /= jumpMultiplier;
    }
}
