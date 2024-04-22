using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject ship;
    public Transform[] bulletSpawnPoints;
    public float fireRate = 5f; 
    private Vector3 aim;
    public Camera cam;
    public Vector3 mousePos;
    private float nextFireTime = 0f;
    public int shots = 1;
    public AudioSource audioSource;
        public AudioSource shoot2;
        public AudioSource shoot3;
        private int shotNum = 0;

    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        fireRate = GameController.Instance.fireRate;
        shots = (int)GameController.Instance.shots;
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) 
        {
                if(!GameController.Instance.hasMoved) {
                        GameController.Instance.hasMoved = true;
                }
            if(Time.time >= nextFireTime) {
                Debug.Log("shooting");
                Shoot();
                if(shotNum == 0) {

                        audioSource.Play();
                        shotNum++;
                } else if (shotNum == 1) {
                        shoot2.Play();
                        shotNum++;
                } else if (shotNum == 2) {
                        shoot3.Play();
                        shotNum = 0;
                }
                nextFireTime = Time.time + 1f / fireRate;
            }
            
        }
    }

    void Shoot()
    {
            // Debug.Log(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                for (int i = 0; i < shots; i++) {
                        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoints[i].position, bulletSpawnPoints[i].rotation);
                        bullet.transform.LookAt(hit.point);
                        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), true);
                }
            }
            // var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
       
    }
}