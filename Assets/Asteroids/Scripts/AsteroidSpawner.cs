using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidSpawner : MonoBehaviour
    {
        

        //array of prefabs to spawn
        public GameObject[] asteroidPrefabs;
        //spawn rate
        public float spawnRate = 1f;
        //spawn radius
        public float spawnRadius = 2f;
        //maximum velocity
        public float maxVelocity = 3f;
        // Use this for initialization

        public  void Spawn(GameObject prefab, Vector3 position)
        {
           


            //randomise asteroid rotation
            Quaternion randomRot = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
            //spawn random location
            GameObject asteroid = Instantiate(prefab,  position,randomRot, transform);
            //get rigidbody2d from asteroid
            Rigidbody2D rigid = asteroid.GetComponent<Rigidbody2D>();
            //speed of the asteroids is random but under 3
            Vector3 randomforce = Random.insideUnitCircle * maxVelocity;
            //adding the force to the asteroid rigidbody
            rigid.AddForce(randomforce, ForceMode2D.Impulse);
            
        }
        void SpawnLoop()
        {
            //set camera bounds using extension method
            Bounds camBounds = Camera.main.GetBounds(spawnRadius);
            //random position within a circle
            Vector2 randomPos = camBounds.GetRandomPosonBounds();
            //spawn inside the sphere with random pos and rotation

            

            //random index into asteroid prefab array
            int randIndex = Random.Range(0, asteroidPrefabs.Length);
            //get random asteroid using prefabs
            GameObject randAsteroid = asteroidPrefabs[randIndex];
            //spawn using randompos
            Spawn(randAsteroid, randomPos);
        }
        void Start()
        {
            //call the function a set amount of times
            InvokeRepeating("SpawnLoop", 0, spawnRate);
        }

        // Update is called once per frame
        void Update()
        {
          
        }
        private void OnDrawGizmos()
        {
            Bounds camBounds = Camera.main.GetBounds(spawnRadius);
            //draw the bounds
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(camBounds.center, camBounds.size);
        }
    }
}