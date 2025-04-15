using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEditor.Experimental.GraphView;


public class GamblingGunHolder : MonoBehaviour
{
    public Animator gunAnimator;
    //Gun stats
    public float timeBetweenShooting, reloadTime;
    public int magazineSize;
    public bool allowButtonHold;
    int bulletsLeft;
    public CameraShake cameraShake;
    public float shakeDuration;
    public float shakeIntensity ;
    public ParticleSystem MuzzleFlash;  
    public Transform currentWeapon;

    //bools 
    bool shooting, readyToShoot, reloading;


    //Reference
    public Camera fpsCam;
    public TextMeshProUGUI text; 
    public List<bool> states = new List<bool> { true, false, true, false };




    private void Update()
    {
        MyInput();
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
            
            MuzzleFlash.Stop();
            MuzzleFlash.Play();
        }
        else{
            
        }
        

 
        if (cameraShake != null)
        {
            cameraShake.ShakeCamera(shakeDuration, shakeIntensity);
        }



        bulletsLeft--;


        Invoke("ResetShot", timeBetweenShooting);


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
