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
    Vector3 pos;
    Vector3 posjump;
    //jumps
    bool jump = false;
    GameObject JumpGround;
    //wall
    bool wall;
    bool climbwall;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {

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
                JumpManager();
                jump = false;
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
        //wall
        pos = transform.position;
        pos.y -= 0.6f;
        var CF = Character.transform.forward;
        RaycastHit wallhit;
        Ray wallray = new Ray(pos, CF);
        if (Physics.Raycast(wallray,out wallhit,0.5f)){
            if(wallhit.collider.gameObject.tag == "wall"){
                wall = true;
            }
        }else{
            wall = false;
        }
        if(wall == true){
            if(Input.GetKey(KeyCode.W)){
            climbwall = true;
            //Debug.Log("AA");
          }if(wall == false){
              climbwall = false;
          }
        }else if(wall == false){
            climbwall = false;
        }
        //jump_sensor
        RaycastHit jumphit;
        var CU = -Character.transform.up;
        posjump = transform.position;
        posjump.y -= 1f;
        Ray jumpray = new Ray(posjump, CU);
        if (Physics.Raycast(jumpray,out jumphit,0.1f)){
            JumpGround = jumphit.collider.gameObject;
            if(jumphit.collider.gameObject.tag == "Ground"){
              if(jump == false){
                jump = true;
              }
            }
        }
         Debug.DrawRay(wallray.origin, wallray.direction * 0.5f, Color.red, 0.5f);
    }

    void JumpManager(){
      if(Input.GetKeyDown(KeyCode.Space)){
          jump = false;
          rb.AddForce(transform.up * 200);
          Debug.Log(jump);
      }
    }
    /*void OnCollisionEnter(Collision other){
            if (other.gameObject.tag == "Ground"){
                jump = true;
            }else{
                jump = false;
            }
    }*/
}
