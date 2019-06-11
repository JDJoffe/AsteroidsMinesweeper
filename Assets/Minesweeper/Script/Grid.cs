using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper
{
    public class Grid : MonoBehaviour
    {
        // public bool isMine = false;
        public GameObject tilePrefab;
        public int width = 10, height = 10;
        public float spacing = .155f;
        private Tile[,] tiles;

        //spawn tiles
        Tile SpawnTile(Vector3 pos)
        {
            //clone the tile prefab
            GameObject clone = Instantiate(tilePrefab);
            //edit properties
            clone.transform.position = pos;
            Tile currentTile = clone.GetComponent<Tile>();
            //return
            return currentTile;
        }
        void GenerateTiles()
        {
            tiles = new Tile[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //halfsize for later
                    Vector2 halfSize = new Vector2(width * 0.5f, height * 0.5f);
                    //tile pivot around grid
                    Vector2 pos = new Vector2(x - halfSize.x, y - halfSize.y);

                    //offset vector to apply to individual tiles position
                    Vector2 offset = new Vector2(0.5f, 0.5f);
                    pos += offset;

                    pos *= spacing;

                    //spawn the tile
                    Tile tile = SpawnTile(pos);
                    //attatch to transform
                    tile.transform.SetParent(transform);
                    //store coordinates
                    tile.x = x;
                    tile.y = y;
                    //store tile array at coordinates
                    tiles[x, y] = tile;

                }
            }
        }
        // Use this for initialization
        void Start()
        {
            GenerateTiles();
            //detatch text element from the block
            //textElement.transform.SetParent(null);
            //randomly decide if it is a mine
            //isMine = Random.value < 0.1f;
        }

        // Update is called once per frame
        void Update()
        {
            //check if mouse has been pressed
            if (Input.GetMouseButtonDown(0))
            {
                //ray cast from the camera using the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                //did the reay cast hit anything?

                if (hit.collider != null)
                {
                    //is the thing we hit a tile
                    Tile hitTile = hit.collider.GetComponent<Tile>();
                    if (hitTile != null)
                    {
                        //run select a tile subprogram with selected tile
                        SelectTile(hitTile);
                    }
                }

            }
        }
        public int GetAdjacentMineCount(Tile Tile)
        {
            //count set to 0
            int count = 0;
            //loop though all adjacent tiles on X
            for (int x = -1; x <= 1; x++)
            {
                //loop through all adjacent tiles on Y
                for (int y = -1; y <= 1; y++)
                {
                    //calculate which adjacent tile to look at
                    int desiredX = Tile.x + x;
                    int desiredY = Tile.y + y;
                    // check if the desired x / y is out of bounds
                    if (desiredX < 0 || desiredX >= width ||
                        desiredY < 0 || desiredY >= height)
                    {
                        //continue to next element in loop
                        continue;
                    }
                    //select current tile
                    Tile currentTile = tiles[desiredX, desiredY];

                    //check if tile is a mine
                    if (currentTile.isMine)
                    {
                        //increment by 1
                        count++;
                    }
                }
            }

            return count;
            //remember this! ^^^^
        }
        void SelectATile()
        {
            //Generate ray from capera with mouse position
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            //perform raycast
            RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);

            //if mouse hits something
            if (hit.collider != null)
            {
                //try to get a tile component from what we clicked on
                Tile hitTile = hit.collider.GetComponent<Tile>();

                //check if what was clicked on is a tile
                if (hitTile != null)
                {
                    // count of all mines around the hit tile
                    int adjacentMines = GetAdjacentMineCount(hitTile);

                    //reveal what that tile was
                    hitTile.Reveal(adjacentMines);
                }
            }
        }
        void FFuncover(int x, int y, bool[,] Visited)
        {
            //is x and y within bounds of the grid
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                //have these coords been visited
                if (Visited[x, y])

                    //reveal that tile in that x & y coord
                    return;
                Tile tile = tiles[x, y];
                int adjacentMines = GetAdjacentMineCount(tile);
                tile.Reveal(adjacentMines);

                // if there are no adjacent mines around that tile
                if (adjacentMines == 0)
                {
                    //this tile has been visited
                    Visited[x, y] = true;
                    //visit all other tiles around this one
                    FFuncover(x - 1, y, Visited);
                    FFuncover(x + 1, y, Visited);
                    FFuncover(x, y - 1, Visited);
                    FFuncover(x, y + 1, Visited);
                }

            }

        }
        void UncoverMines(int mineState = 0)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile tile = tiles[x, y];
                    //check if tile is a mine
                    if (tile.isMine)
                    {
                        //reveal the tile
                        int adjacentMines = GetAdjacentMineCount(tile);
                        tile.Reveal(adjacentMines, mineState);
                    }
                }
            }
        }
        bool NoMoreEmptyTiles()
        {
            //set empty tile count to 0
            int emptyTileCount = 0;

            //loop through 2D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile tile = tiles[x, y];
                    //if tile is not revealed and not a mine
                    if (!tile.isRevealed && !tile.isMine)
                    {
                        emptyTileCount += 1;
                    }
                }
            }
            //if there are empty tiles it is true
            //if there are no empty tiles then it is false
            return emptyTileCount == 0;
        }
        void SelectTile(Tile selected)
        {
            int adjacentMines = GetAdjacentMineCount(selected);
            selected.Reveal(adjacentMines);

            //is the tile a mine
            if (selected.isMine)
            {
                //uncover all mines with loss state 0
                UncoverMines();
                //you lose dummy

            }
            //otherwise are there no mines around this tile?
            else if (adjacentMines == 0)
            {
                int x = selected.x;
                int y = selected.y;
                // the use flood fill to uncover adjacent mines
                FFuncover(x, y, new bool[width, height]);
            }
            //are there no more empty tiles in the game?
            if (NoMoreEmptyTiles())
            {
                //uncover all the mines with win state 1
                UncoverMines(1);
                //You win, cool
            }
        }
    }


}
