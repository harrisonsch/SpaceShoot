using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour{

    public float selfDestructTime = 1f;

    void Start(){
        StartCoroutine(SelfDestruct(gameObject));
    }

    IEnumerator SelfDestruct(GameObject theObject){
            yield return new WaitForSeconds(selfDestructTime);
            Destroy(theObject);
    }
}
