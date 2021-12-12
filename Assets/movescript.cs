using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movescript : MonoBehaviour
{
    //move
    float speed = 20.0f;
    public Rigidbody rb;
    private Vector3 latestPos;
    public GameObject Character;
    GameObject t;
    Vector3 pos;
    //jumps
    bool jump;
    //wall
    bool wall;
    bool climbwall;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log(climbwall);
        //移動
        float x =  Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
          //move
        if(climbwall == false){
            Vector3 move = new Vector3(x,0,z);
            if(rb.velocity.magnitude < 5){
                rb.AddForce(move, ForceMode.Force);
            }
          //direction
            Vector3 diff = transform.position - latestPos;
            latestPos = transform.position;
            if(Mathf.Abs(diff.x) > 0.001f || Mathf.Abs(diff.z) > 0.001f){
                Quaternion rot = Quaternion.LookRotation(diff);
                //rot = Quaternion.Slerp(rb.transform.rotation, rot, 0.2f);
                rot.z = 0f;
                rot.x = 0f;
                Character.transform.rotation = rot;
            }
          //jump
            if(jump == true){
                if(Input.GetKeyDown(KeyCode.Space)){
                    jump = false;
                    rb.AddForce(transform.up * 200);
                }
            }
          /*squat
            if(Input.GetKeyDown(KeyCode.LeftShift)){
              if(climbwall == false){
                transform.localScale = new Vector3(1f,0.5f,1f);
                //pos.y -= 0.25f;
                speed = 10.0f;
                }
              }else if(Input.GetKeyUp(KeyCode.LeftShift)){
                if(climbwall == false){
                transform.localScale = new Vector3(1f,1f,1f);
                //pos.y += 0.25f;
                speed = 20.0f;
                }
              }*/
          //climb
        }else if(climbwall == true){
              //Debug.Log(wall);
              rb.velocity = Vector3.zero;
              transform.Translate(0f,0.1f,0.02f);
        }
        pos = transform.position;
        pos.y -= 0.6f;
        RaycastHit hit;
        Ray ray = new Ray(pos, transform.forward);
        if (Physics.Raycast(ray,out hit,0.5f)){
            GameObject t = hit.collider.gameObject;
            if(hit.collider.gameObject.tag == "wall"){
                wall = true;
            }
        }else{
            wall = false;
        }
        if(wall == true){
            if(Input.GetKey(KeyCode.W)){
            climbwall = true;
            //Debug.Log("AA");
          }
        }else if(wall == false){
            climbwall = false;
        }
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);
    }

    void OnCollisionEnter(Collision other){
            if (other.gameObject.tag == "Ground"){
                jump = true;
            }else{
                jump = false;
            }
    }
}
