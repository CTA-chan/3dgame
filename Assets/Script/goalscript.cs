using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class goalscript : MonoBehaviour
{
    public Text Goaltext;
    // Start is called before the first frame update
    void Start()
    {
        Goaltext.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "player"){
            Goaltext.text = "Goal";
            Goaltext.enabled = true;
            Invoke("scene",5.0f);
        }
    }
    void scene(){
      SceneManager.LoadScene("ResultScenes");
    }
}
