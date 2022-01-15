using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class firstgamescript : MonoBehaviour
{
    public Text firsttext;
    public GameObject firstbutton;
    bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        firsttext.enabled = true;
        firstbutton.SetActive(false);
        first = true;
    }

    // Update is called once per frame
    void Update()
    {
      if(first == true){
        if(Input.GetMouseButtonDown(0)){
          Debug.Log("AA");
          firstbutton.SetActive(true);
          firsttext.enabled = false;
          first = false;
        }
      }
    }
    public void OnClick()
    {
        Debug.Log("押された!");  // ログを出力
        SceneManager.LoadScene("StageSamoleScene");
    }
}
