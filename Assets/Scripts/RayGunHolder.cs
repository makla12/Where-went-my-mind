using UnityEngine;
using TMPro;
using System.Collections;


public class GunSystem : MonoBehaviour
{
    public Animator gunAnimator;
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    public CameraShake cameraShake; // Assign this in the inspector
    public float shakeDuration = 0.2f;
    public float shakeIntensity = 0.5f;
    public ParticleSystem MuzzleFlash;  
    public float Particledelay = 0.1f;
    public Transform currentWeapon;


  


    //bools 
    bool shooting, readyToShoot, reloading;


    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    public TextMeshProUGUI text;


    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();

        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);


        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();


        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && Time.timeScale != 0) {
            gunAnimator.SetBool("Shooting", true);
            bulletsShot = bulletsPerTap;
            Shoot();
        }
        else gunAnimator.SetBool("Shooting", false);
    }
    
    void UpdateMuzzleFlash()
    {   
        GameObject activeWeapon = FindActiveWeapon();

        if (activeWeapon != null)
        {
            Transform particlesTransform = activeWeapon.transform.Find("TostParticles");
            
            if (particlesTransform != null)
            {
                MuzzleFlash = particlesTransform.GetComponent<ParticleSystem>();
            }
        }
    } 
    private GameObject FindActiveWeapon()
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");

        foreach (GameObject weapon in weapons)
        {
            if (weapon.activeSelf) 
            {
                return weapon;
            }
        }

        return null; 
    }  
    private void Shoot()
    {
        readyToShoot = false;
        
        UpdateMuzzleFlash();
        if (MuzzleFlash != null)
        {
            MuzzleFlash.Play();
        }
        else{
            
        }
        // StartCoroutine(ShootTwice());
        


        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);


        //Calculate Direction with Spread
        // Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);


        //RayCast
        // if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        // {
        //     if (rayHit.collider.CompareTag("Enemy")){
        //         rayHit.collider.GetComponent<EnemyControler>().TakeDamage(damage);
        //     }
        // }

 
        if (cameraShake != null)
        {
            cameraShake.ShakeCamera(shakeDuration, shakeIntensity);
        }

        //Graphics
        // Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        // Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);     In case we want to add some effects from shoot


        bulletsLeft--;
        bulletsShot--;


        Invoke("ResetShot", timeBetweenShooting);


        if(bulletsShot > 0 && bulletsLeft > 0)
        Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
        gunAnimator.SetBool("Reloading", true);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        gunAnimator.SetBool("Reloading", false);
    }
}
