using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpscript : MonoBehaviour
{
    public GameObject player;
    movescript script;
    bool JumpBool;
    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
      script = player.GetComponent<movescript>();
      JumpBool = script.jump;
    }

    void OnTriggerEnter(Collider other){
            if (other.gameObject.tag == "Ground"){
                JumpBool = true;
                Debug.Log(JumpBool);
                script.jump = JumpBool;
            }
    }
}
