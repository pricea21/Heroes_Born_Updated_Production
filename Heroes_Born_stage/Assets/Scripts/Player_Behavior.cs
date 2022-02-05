using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;

    private float vInput;
    private float hInput;

    //1
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        //3
        _rb = GetComponent<Rigidbody>();
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
    }
    
    void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;
        Quaternion anglerot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * anglerot);
        //5 
    }
}
