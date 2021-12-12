using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackscript : MonoBehaviour
{
    GameObject　enemy;
    HitPointscript script;
    int attackpoint = 10;
    float attacktime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(enemy != null){
        if(attacktime == 0f){
          if(Input.GetMouseButtonDown(0)){
              int hp = script.HP;
              hp -= attackpoint;
              script.HP = hp;
              Debug.Log(hp);
              attacktime = 1f;
          }
        }
      }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy"){
            enemy = other.gameObject;
            script = enemy.GetComponent<HitPointscript>();
        }
     }
     void AttackTimeManager(){
        if(attacktime <= 0f){
            attacktime -= Time.deltaTime;
        }
     }
}
