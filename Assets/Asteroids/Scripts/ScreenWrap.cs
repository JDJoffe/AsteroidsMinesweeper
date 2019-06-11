using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Asteroids
{
    public class ScreenWrap : MonoBehaviour
    {
        //debug color
        public Color debugcolor = Color.blue; 
        //padding for screenWrap
        public float padding = 3f;
        //spriterenderer variable
        private SpriteRenderer spriteRenderer;
        // Use this for initialization
        void awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        //update the position of entities the script is attatched to
        void updateposition()
        {
            //get the camera dimensions using padding
            Bounds camBounds = Camera.main.GetBounds(padding);
            //store the position and size in a shorter variable name
            Vector3 Pos = transform.position;
            //store min and max vectors
            Vector3 min = camBounds.min;
            Vector3 max = camBounds.max;
            //check left
            if (Pos.x < min.x)
            {
                Pos.x = max.x;
            }
            //check right
            if (Pos.x > max.x)
            {
                Pos.x = min.x;
            }
            //check top
            if (Pos.y > min.y)
            {
                Pos.y = max.y;
            }
            //check bottom
            if (Pos.y < max.y)
            {
                Pos.y = min.y;
            }
            //apply position
            transform.position = Pos;
        }
        private void LateUpdate()
        {
            updateposition();
        }
        private void OnDrawGizmos()
        {
            //set bounds around camera
            Bounds camBounds = Camera.main.GetBounds(padding);
            //draw the bounds
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(camBounds.center, camBounds.size);
        }

    }
}