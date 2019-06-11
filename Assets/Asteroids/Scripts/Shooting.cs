using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Shooting : MonoBehaviour
    {
        //spawn many boolets
        public GameObject bulletPrefab;

        
        //time.deltatime for both of these when you use them
        public float bulletSpeed = 20f;     
        public float shootRate = 0.2f;
        //timer to count shootRate
        private float shootTimer = 0f;
        // Use this for initialization

            void Shoot()
        {
            //new bullet clone
            GameObject clone = Instantiate(bulletPrefab, transform.position, transform.rotation);
         //   GetComponent rigidbody from clone
            Rigidbody2D rigid = clone.GetComponent<Rigidbody2D>();
            //addforce to bullets use bulletspeed
            rigid.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            shootTimer += Time.deltaTime;

            if (shootTimer >= shootRate )
            {
                //if space down
                if (Input.GetKeyDown(KeyCode.Space) )
                {
                    //call shoot
                    Shoot();
                    //set to 0
                    shootTimer = 0f;
                }
            }
        }
    }
}