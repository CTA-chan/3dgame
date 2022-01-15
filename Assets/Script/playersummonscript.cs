using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersummonscript : MonoBehaviour
{
    public GameObject playerdata;
    Vector3 pos;
    // Start is called before the first frame update
    void Start(){
        pos = this.transform.position;
        pos.y += 2;
        summon();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void summon(){
         Instantiate(playerdata,pos,Quaternion.identity);
    }
}
