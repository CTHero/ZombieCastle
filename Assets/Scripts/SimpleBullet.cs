using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    //Who does the bullet damage
    public LayerMask myEnemies;

    //Bullet settings
    public int damage = 1;
    public float bulletLifetime= 1;

    private Rigidbody theRigidBody;
    PhysicMaterial physicsMaterial;

    // Start is called before the first frame update
    void Start()
    {
        theRigidBody = GetComponent<Rigidbody>();
        physicsMaterial = new PhysicMaterial();
        physicsMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
//        GetComponent<SphereCollider>().material = physicsMaterial;
        GetComponent<Collider>().material = physicsMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        bulletLifetime -= Time.deltaTime;
        if (bulletLifetime <= 0f)
        {
            bulletDestruction();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Replace this with a layer compare");
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().takeDamage(damage);
            Debug.Log("Hitting the enemy");
        }
        
        bulletDestruction();
    }
    private void bulletDestruction()
    {
        Destroy(gameObject);
    }

}
