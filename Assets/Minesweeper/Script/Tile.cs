using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {

        public int x, y;
        public bool isMine = false;
        public bool isRevealed = false;
        [Header("references")]
        public Sprite[] emptySprites;
        public Sprite[] mineSprites;
        private SpriteRenderer rend;

        //revealing a tile load sprite
        public void Reveal(int adjacentMines, int mineState = 0)
        {
            isRevealed = true;

            if (isMine)
            {
                //render mine sprite
                rend.sprite = mineSprites[mineState];
            }
            else
            {
                //render empty sprite
                rend.sprite = emptySprites[adjacentMines];
            }

        }
        void Awake()
        {

            rend = GetComponent<SpriteRenderer>();
        }
        // Use this for initialization
        void Start()
        {
            //spawn random mines
            isMine = Random.value < .1f;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
