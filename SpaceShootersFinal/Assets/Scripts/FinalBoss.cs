using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
        public TurretManager turret1;
        public TurretManager turret2;
        public TurretManager turret3;
        public TurretManager turret4;
        public bool canShootTurrets;
        public float delayTime = 1.0f;
        public float timeToSpawn = 0.5f;
        public GameObject spawn1;
        public GameObject spawn2;
        public GameObject spawn3;
        private bool canShoot = true;
        public GameObject enemyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot) {
                canShoot = false;
                StartCoroutine(FireTurrets());
        }
        
    }
    IEnumerator FireTurrets() {
        turret1.shootTurret();
        yield return new WaitForSeconds(delayTime);
        turret2.shootTurret();
        yield return new WaitForSeconds(delayTime);
        turret3.shootTurret();
        yield return new WaitForSeconds(delayTime);
        turret4.shootTurret();
        yield return new WaitForSeconds(timeToSpawn);
        Instantiate(enemyPrefab, spawn1.transform.position, Quaternion.identity);
        Instantiate(enemyPrefab, spawn2.transform.position, Quaternion.identity);
        Instantiate(enemyPrefab, spawn3.transform.position, Quaternion.identity);
        canShoot = true;
    }
}
