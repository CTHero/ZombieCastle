using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    //bullet


    //gun stats
    [Header("Gun Properties")]
    public KeyCode fireKey = KeyCode.Mouse0;
    public int bulletsPerTap = 1;
    public float timeBetweenShots = .25f;
    public float timeBetweenShooting = 1f;
    public float reloadTime = 2f;
    public float spread = .5f;

    
    public int magazineSize = 12;
    public bool automaticFiring =  false;

    [Header("Bullet Properties")]
    public GameObject bulletPrefab;
    public float shootForce = 100f;
    public float upwardForce = 0f;

    int bulletsLeft;
    int bulletsShot;

    bool shooting;
    bool readyToShoot;
    bool reloading;

    public Camera fpsCam;
    public Transform attackPoint;

    bool allowAnotherShot = true;

    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void myInput()
    {
        if (automaticFiring) shooting = Input.GetKey(fireKey);
        else shooting = Input.GetKeyDown(fireKey);

        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) reload();
        //Reload automatically if trying to shoot without ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) reload();

        //Shooting
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Debug.Log("Shooting");
            shoot();
        }
    }
    private void shoot()
    {
        readyToShoot = false;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        //Calculate direction
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread,spread);

        //calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        if(muzzleFlash !=null) Instantiate(muzzleFlash, attackPoint.position,Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function
        if(allowAnotherShot)
        {
            Invoke("resetShot", timeBetweenShooting);
            allowAnotherShot = false;
        }

        if(bulletsShot < bulletsPerTap && bulletsLeft >0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void resetShot()
    {
        //Allow shooting and invoking
        readyToShoot = true;
        allowAnotherShot = true;
    }

    private void reload()
    {
        reloading = true;
        Invoke("reloadFinished", reloadTime);
    }
    private void reloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myInput();

        if(ammunitionDisplay != null)  //If the ammunition display exists
        {
            //Set the amount of bullets left
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + "/" + magazineSize / bulletsPerTap);
        }

    }
}
