using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movescript : MonoBehaviour
{
    //move
    float x;
    float z;
    //float speed = 20.0f;
    public Rigidbody rb;
    private Vector3 latestPos;
    public GameObject Character;
    Vector3 pos;
    float moveSpeed = 3f;
    //jumps
    public bool jump = false;
    //wall
    bool wall;
    bool climbwall;
    //animation
    public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {

        //移動
      x =  Input.GetAxis("Horizontal");
      z = Input.GetAxis("Vertical");

        //Animator
      if(Input.GetKeyDown(KeyCode.W)){
          animator.SetBool("run",true);
      }else if(Input.GetKeyUp(KeyCode.W)){
          animator.SetBool("run",false);
      }
          //move
          if(climbwall == false){
            /*Vector3 move = new Vector3(x,0,z);
            if(rb.velocity.magnitude < 5){
                rb.AddForce(transform.forward * z, ForceMode.Force);

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
            }*/
          //jump
            if(jump == true){
                JumpManager();
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
         Debug.DrawRay(wallray.origin, wallray.direction * 0.5f, Color.red, 0.5f);
    }

    void JumpManager(){
      if(Input.GetKeyDown(KeyCode.Space)){
          jump = false;
          rb.AddForce(transform.up * 200);
          animator.SetTrigger("jump");
      }
    }

    void FixedUpdate() {
      // カメラの方向から、X-Z平面の単位ベクトルを取得
      Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

      // 方向キーの入力値とカメラの向きから、移動方向を決定
      Vector3 moveForward = cameraForward * z + Camera.main.transform.right * x;

      // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
      rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

      // キャラクターの向きを進行方向に
      if (moveForward != Vector3.zero) {
        transform.rotation = Quaternion.LookRotation(moveForward);
      }
    }
    void OnTriggerEnter(Collider other){
            if (other.gameObject.tag == "Ground"){
                jump = true;
            }else{
                jump = false;
            }
    }
}
