using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{   public float damage;
    public float range;
    
    public float maxAmmo;
    public float currentAmmo;

    public float firingRate;
    public float reloadspeed;
    float fireTimer = 0f;

    public bool reloading = false;
    float reloadingTimer = 0f;

    public Text ammo ;

    public ZombieEar zombieEar;

    public bool fireShot;

    public PlayerHealth health;

    private void Start()
    {
        currentAmmo = maxAmmo;
        zombieEar = new ZombieEar();
    }
    void Update()
    {
        if (!health.died) { 
            fireShot = false;
            ammo.text = currentAmmo.ToString();
            if (currentAmmo > 0 && !reloading)
                if (Input.GetButton("Fire1") && Time.time > fireTimer)
                {
                    
                    FindAnyObjectByType<Audio>().play("gunShoot");
                    currentAmmo--;
                    fireTimer = Time.time + 1f / firingRate;
                    Fire();
                    fireShot = true;
                    
                }
            if (Input.GetKeyDown(KeyCode.R) == true)
                {
                    reloading = true;
                }
            reload();

            ZombieEar.shotfired = fireShot;
        }
    }
    void Fire() {
        //flash animation
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range)) {
            zombieEar.SetTraget(transform);
            ZombieHealth Target = hit.transform.GetComponent<ZombieHealth>();
            if (Target != null)
            {
                Target.DamageTaken(damage);
            }
                

        }
    }
    void reload()
    {   
        if (reloading)
        {
            if (currentAmmo < 40 && Time.time > reloadingTimer)
            {
                currentAmmo ++ ;
                reloadingTimer = Time.time + 1f/reloadspeed ;
            }
            if(currentAmmo >= 40 )
                reloading = false ;
        } 
    }
    //impact flash 
    //impact force
}
