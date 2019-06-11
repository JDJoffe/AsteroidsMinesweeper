using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Asteroids
{




    public static class Utility
    {
        public static Vector3 GetRandomPosonBounds(this Bounds bounds)
        {
            //result to return in end
            Vector3 result = Vector3.zero;
            //smaller variable names min max
            Vector3 min = bounds.min;
            Vector3 max = bounds.max;
            //50% chance its at the top/bottom left/right
            bool topOrBottom = Random.Range(0, 2) > 0;
            //50% chance its top or bottom
            bool top = Random.Range(0, 2) > 0;
            //50% chance its left or right
            bool right = Random.Range(0, 2) > 0;

            //if top or bottom
            if (topOrBottom)
            {
                //random range on x
                result.x = Random.Range(min.x, max.x);
                //top or bottom?
                result.y = top ? max.y : min.y;
            }
            else
            {
                //left or right?
                result.x = right ? max.x : min.x;
                //random range on y
                result.y = Random.Range(min.y, max.y);
            }
            return result;
        }
        //calculates and returns bounds with padding
        public static Bounds GetBounds(this Camera cam, float padding = 1f)
        {
            //define camera dimensions
            float camHeight, camWidth;
            //position of camera
            Vector3 camPos = cam.transform.position;
            //camheight and camwidth size
            camHeight = 2f * cam.orthographicSize;
            camWidth = camHeight * cam.aspect;
            //apply padding
            camHeight += padding;
            camWidth += padding;
            //create new bounds for above info
            return new Bounds(camPos, new Vector3(camWidth, camHeight, 100));
        }
    }


}
