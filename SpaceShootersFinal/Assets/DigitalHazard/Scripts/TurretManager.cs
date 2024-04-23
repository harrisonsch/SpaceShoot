using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour{

    private Transform player;
    public Transform turretBodyPivot, turretGunsPivot, turretGunsEnd, gunShakePivot, fireBaseLeft, fireBaseRight, firePointLeft, firePointRight;
    public GameObject turretAmmo;
    public GameObject turretFireVFX;
    public float firingRange = 5f, projectileSpeed = 10f, attackRate = 1f, rotationSpeed = 2f, rotationGunSpeed = 0.5f;
    private float nextAttackTime = 0f, distToPlayer = 0f; 
    private bool canAttack = true;
    public bool turretShoot = true;

    void Start(){
        if (GameObject.FindWithTag("Player") != null){
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
    }

    void Update(){
        distToPlayer = Vector3.Distance(transform.position, player.position);
        if(turretShoot) {
            if (distToPlayer <= firingRange){
            LookAtPlayer();

            if (canAttack){
                StartCoroutine(GunShake());
                StartCoroutine(FireBullets());
                canAttack = false;
            }
                }    
        }
    }

    void FixedUpdate(){
           if (Time.time >= nextAttackTime){
                canAttack=true;
                nextAttackTime = Time.time + 1f / attackRate;
            } else {
                canAttack=false;
            }
    }

    void LookAtPlayer(){
        if (player != null){
        //rotate turret body
            Vector3 directionToPlayer = player.position - turretBodyPivot.position;
            directionToPlayer.y = 0; //we want to rotate on Y. which axis is being set = 0?
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                
            turretBodyPivot.rotation = Quaternion.Slerp(turretBodyPivot.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            //turretBodyPivot.rotation = lookRotation;

        //rotate turret guns       

        // Determine which direction to rotate towards:
        Vector3 targetGunsDirection = player.position - turretGunsPivot.position;
        //Debug.Log("turretGunsPivot: " + turretGunsPivot.localRotation + ", targetGunsDirection: " + targetGunsDirection);
        // The step size is equal to speed times frame time:
        float singleStep = rotationGunSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newGunsDirection = Vector3.RotateTowards(turretGunsPivot.forward, targetGunsDirection, singleStep, 0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(turretGunsPivot.position, newGunsDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
            //if ((turretGunsPivot.localRotation.x >= -0.5f)&&(turretGunsPivot.localRotation.x <= 0.5f)){
                turretGunsPivot.rotation = Quaternion.LookRotation(newGunsDirection);
            //}

        }
    }

    IEnumerator FireBullets() {
        //create the gun vectors
        Vector3 fwdL = (firePointLeft.position - fireBaseLeft.position).normalized;
        Vector3 fwdR = (firePointRight.position - fireBaseRight.position).normalized;
        
        //instantiate muzzleflashes and bullets, then add force
        GameObject flashLeft = Instantiate(turretFireVFX, firePointLeft.position, Quaternion.identity);
        GameObject bulletLeft = Instantiate(turretAmmo, firePointLeft.position, turretGunsPivot.rotation);
        bulletLeft.GetComponent<Rigidbody>().AddForce(fwdL * projectileSpeed, ForceMode.Impulse); 
        StartCoroutine(DestroyBullet(bulletLeft, 4f));
        StartCoroutine(DestroyBullet(flashLeft, 2f));

        yield return new WaitForSeconds(0.05f);

        GameObject flashRight = Instantiate(turretFireVFX, firePointRight.position, Quaternion.identity);
        GameObject bulletRight = Instantiate(turretAmmo, firePointRight.position, turretGunsPivot.rotation);
        bulletRight.GetComponent<Rigidbody>().AddForce(fwdR * projectileSpeed, ForceMode.Impulse); 
        StartCoroutine(DestroyBullet(bulletRight, 4f));
        StartCoroutine(DestroyBullet(flashRight, 2f));        
    }

    IEnumerator DestroyBullet(GameObject bullet, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        Destroy(bullet);
    }

    IEnumerator GunShake() {
        gunShakePivot.position = turretGunsEnd.position;
        yield return new WaitForSeconds(0.05f);
        gunShakePivot.position = turretGunsPivot.position;
    }

}




/*
//flashLeft.transform.parent = firePointLeft;

https://gamedev.stackexchange.com/questions/138041/how-to-properly-rotate-a-turret
Vector3 positionOfTheEnemy = enemy.transform.position; // Creates a new object type of Vector3 because it's struct, so it has no reference to enemy position
positionOfTheEnemy.y = turretTransform.position.y;
turretTransform.LookAt(positionOfTheEnemy);



https://forum.unity.com/threads/how-can-you-limit-the-lookat-axis.134330/
to rotate on the Y axis
turretGunsPivot.rotation = Quaternion.LookRotation(turretGunsPivot.forward, player.position - turretGunsPivot.position);

to rotate on X axis:
        LookAtXConstZ(turretGunsPivot, player);

   public void LookAtXConstZ(Transform trans, Transform target){
        // Fix X pointing at the target, and maintain the current Z direction
        Vector3 newX = target.position - trans.position;
        Vector3 newZ = trans.forward;
    
        // Calculate new Y direction
        Vector3 newY = Vector3.Cross(newZ, newX);
    
        // Let the library method do the heavy lifting
        trans.rotation = Quaternion.LookRotation(newZ, newY);
    }


        //turretBodyPivot.LookAt(player.position);
        //turretGunsPivot.LookAt(player.position);
        //turretGunsPivot.rotation = Quaternion.Euler(turretGunsPivot.rotation * new Vector3(1,0,0));

            Vector3 posPlayerGunTemp = player.position; // Creates a new object type of Vector3 because it's struct, so it has no reference to enemy position
            posPlayerGunTemp.x = turretGunsPivot.position.x;
            turretGunsPivot.LookAt(posPlayerGunTemp);

            Vector3 posPlayerBaseTemp = player.position; // Creates a new object type of Vector3 because it's struct, so it has no reference to enemy position
            posPlayerBaseTemp.y = turretBodyPivot.position.y;
            turretBodyPivot.LookAt(posPlayerBaseTemp);


        //TURRET ROTATION:
        if ((player != null) && (distToPlayer <= firingRange)) {
			//rotate turret towards player:
			Vector3 lookDir = (player.position - turretGunsPivot.position).normalized;
			float angle = Mathf.Atan3(lookDir.y, lookDir.x, lookDir.z) * Mathf.Rad2Deg;
			float offset = 180f;
			if (player.position.x > gameObject.transform.position.x){
				offset = 0;
			}
			turretGunsPivot.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
		} else {
			//rotate turret to forward (when player is not in range, or dead):
			turretGunsPivot.rotation = Quaternion.Euler(Vector3.forward);
		}
*/