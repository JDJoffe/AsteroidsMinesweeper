using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Movement : MonoBehaviour
    {
        //units travelled per-s
        public float speed = 20f;
        
        //rotation per-s
        public float rotationSpeed = 360f;

        //atatch rigidbody2D
        private Rigidbody2D rigid;


        // Use this for initialization
        void Start()
        {
            //grab rigidbody2D component do not grab rigidbody
            rigid = GetComponent<Rigidbody2D>();
            //add force in transform's up direction
          
        }

        // Update is called once per frame
        void Update()
        {
            //check A key
            if (Input.GetKey(KeyCode.A  ))
            {
                //rotate left
                transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
               
            }
             if(Input.GetKey(KeyCode.D))
            {
                //rotate right
                //rigid.AddForce(transform.Rotate.90)
                transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
             if(Input.GetKey(KeyCode.W))
            {
                //move in direction currently facing
                 rigid.AddForce(transform.up * speed );
               
            }
            //if (Input.GetKey(KeyCode.S ))
            //{
            //    rigid.AddForce(transform.up * -speed );
            //}
            
        }
    }
}