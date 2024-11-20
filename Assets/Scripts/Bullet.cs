using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody theRigidBody;
    public GameObject explosion;
    public LayerMask myEnemies;

    //Bullet settings
    [Range(0f, 1f)]
    public float bounciness = 0f;
    public bool useGravity = false;

    public int explosionDamage = 1;
    public float explosionRange = 0.25f;

    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    int collisions = 0;
    PhysicMaterial physicsMaterial;

    void Start()
    {
        theRigidBody = GetComponent<Rigidbody>();
        setUp();
    }

    private void setUp()
    {
        physicsMaterial = new PhysicMaterial();
        physicsMaterial.bounciness = bounciness;
        physicsMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
        physicsMaterial.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = physicsMaterial;
        theRigidBody.useGravity = useGravity;
    }

    private void Explode()
    {

        if(explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] enemiesHit = Physics.OverlapSphere(transform.position, explosionRange, myEnemies);

        for(int i = 0; i < enemiesHit.Length; i++)
        {
            enemiesHit[i].GetComponent<Health>().takeDamage(explosionDamage);
        }
        Invoke("bulletDestruction", 0.05f);
    }

    private void bulletDestruction()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.CompareTag("Bullet")) return;

        collisions++;
        if (collision.collider.CompareTag("Enemy") && explodeOnTouch) Explode();

    }

    // Update is called once per frame
    void Update()
    {
        if(collisions >= maxCollisions)
        {
            Explode();
        }

        maxLifetime -= Time.deltaTime;
        if(maxLifetime <= 0f)
        {
            Explode();
        }
    }
}
