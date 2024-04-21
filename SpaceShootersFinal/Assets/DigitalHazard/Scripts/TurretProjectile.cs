using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TurretProjectile : MonoBehaviour{

      public int damage = 1;
      public GameObject hitVFX; //uncomment VFX #1
      public float SelfDestructTime = 2.0f;
      public float SelfDestructTimeVFX = 0.5f;
      public GameObject bulletArt;

      void Start(){
           // projectileArt = GetComponentInChildren<Renderer>();
           selfDestruct();
      }

      //if bullet hits a collider, play explosion animation, then destroy the effect and the bullet
      public void OnTriggerEnter(Collider other){
            Debug.Log("Enemy bullet hit a thing");
            if (other.gameObject.tag == "Player") {
                  Debug.Log("Player got hit.");
                  //gameHandlerObj.playerGetHit(damage);
                  //other.gameObject.GetComponent<EnemyMeleeDamage>().TakeDamage(damage);
            }
            GameObject animEffect = Instantiate(hitVFX, transform.position, Quaternion.identity); //uncomment VFX #2
            bulletArt.SetActive(false);
            gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(selfDestructHit(animEffect)); //destroy effect in its own prefab script
      }

      IEnumerator selfDestructHit(GameObject VFX){
            yield return new WaitForSeconds(SelfDestructTimeVFX);
            Destroy(gameObject);
      }

      IEnumerator selfDestruct(){
            yield return new WaitForSeconds(SelfDestructTime);
            Destroy(gameObject);
      }

}