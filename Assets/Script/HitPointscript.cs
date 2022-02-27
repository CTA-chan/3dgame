using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointscript : MonoBehaviour
{
    public int HP = 100;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "punch")
       {
           Debug.Log("痛いよぉおおお");      //プレイヤーの剣が当たったらダメージアニメーション発生
       }
   }
}
