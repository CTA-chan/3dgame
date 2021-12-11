using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movescript : MonoBehaviour
{
    
    float speed = 20.0f;
    public Rigidbody rb;
    private Vector3 latestPos;
    //jumps
    bool jump;
    //wall
    bool wall;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        float x =  Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        Vector3 move = new Vector3(x,0,z); 
        if(wall == false){
            if(rb.velocity.magnitude < 5){
                rb.AddForce(move, ForceMode.Force);
            }

            Vector3 diff = transform.position - latestPos;    
            latestPos = transform.position;
            if(Mathf.Abs(diff.x) > 0.001f || Mathf.Abs(diff.z) > 0.001f){
                Quaternion rot = Quaternion.LookRotation(diff);
                rot = Quaternion.Slerp(rb.transform.rotation, rot, 0.2f);
                rot.z = 0f;
                rot.x = 0f;
                this.transform.rotation = rot;
            }
        }
        if(jump == true){
            if(Input.GetKeyDown(KeyCode.Space)){
                jump = false;
                rb.AddForce(transform.up * 200);
            }
        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,1.0f)){
            if(hit.collider.gameObject.tag == "wall"){
                wall = true;
            }
        }
    }

    void OnCollisionEnter(Collision other){
            if (other.gameObject.tag == "Ground"){
                jump = true;
            }else{
                jump = false;
            }
    }
}
