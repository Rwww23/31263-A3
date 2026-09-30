using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,8},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };
    // PPU = 64; sprite size = 128*128 => 2*2
    float[,] rotate;
    // *assuming direction of 7&8 is fixed, i.e. rotate = 0f 
    int CheckUp(int row, int col)
    {
        if (row == 0)
            return -1;
        return levelMap[row-1,col];
    }
    float UpRotation(int row, int col)
    {
        if (row == 0)
            return -1;
        return rotate[row-1,col];
    }

    int CheckDown(int row, int col)
    {
        if (row == levelMap.GetLength(0)-1)
            return -1;
        return levelMap[row+1,col];
    }
    float DownRotation(int row, int col)
    {
        if (row == levelMap.GetLength(0)-1)
            return -1;
        return rotate[row+1,col];   
    } 

    int CheckLeft(int row, int col)
    {
        if (col == 0)
            return -1;
        return levelMap[row,col-1];
    }    
    float LeftRotation(int row, int col)
    {
        if (col == 0)
            return -1;
        return rotate[row,col-1];
    }      

    int CheckRight(int row, int col)
    {
        if (col == levelMap.GetLength(1)-1)
            return -1;
        return levelMap[row,col+1];
    }    
    float RightRotation(int row, int col)
    {
        if (col == levelMap.GetLength(1)-1)
            return -1;
        return rotate[row,col+1];
    }       

    void Start()
    {
        Destroy(GameObject.Find("Level 01"));
        GameObject level = new GameObject("Level 01");
        Sprite[] sprites = new Sprite[9];
        rotate = new float[levelMap.GetLength(0),levelMap.GetLength(1)];
        GameObject[,] tiles = new GameObject[levelMap.GetLength(0),levelMap.GetLength(1)];

        for (int i = 0; i <= 8; i++)
        {
            sprites[i] = Resources.Load<Sprite>($"walls/{i}");
        }

        for (int row = 0; row < levelMap.GetLength(0); row++)
        {
            for (int col = 0; col < levelMap.GetLength(1); col++)
                {
                    //rotate[row,col] = 0f;
                    int type = levelMap[row, col];
                    
                    if (type == 0)
                        continue;

                    int up = CheckUp(row,col);
                    int down = CheckDown(row,col);
                    int left = CheckLeft(row,col);
                    int right = CheckRight(row,col);

                    if (type == 4)// look for 3/4
                    {
                        if (up is 3 or 4 or 7 or 8 or -1 && down is 3 or 4 or 7 or 8 or -1)
                            rotate[row,col] = 90f;
                    }
                    if (type == 2)
                    {
                        if (up is 1 or 2 or -1 && down is 1 or 2 or -1)
                            rotate[row,col] = 90f;                        
                    }
                    
                    if (type == 1)
                    {
                        if (right is 2 && down is 2)
                        {
                            rotate[row,col] = 90f;
                        }
                        if (up is 2 && right is 2)
                        {
                            rotate[row,col] = 180f;
                        }
                        if (left is 2 && up is 2)
                        {
                            rotate[row,col] = 270f;
                        }
                    }

                    GameObject tile = new GameObject("Tile");
                    tile.transform.parent = level.transform;
                    SpriteRenderer renderer = tile.AddComponent<SpriteRenderer>();
                    renderer.sprite = sprites[type];
                    tile.transform.position = new Vector3(2*col, 2*(levelMap.GetLength(0)-row), 0);
                    tile.transform.rotation = Quaternion.Euler(0,0,rotate[row,col]);
                    tiles[row,col] = tile;
                }
        }

        for (int row = 0; row < levelMap.GetLength(0); row++)
        {
            for (int col = 0; col < levelMap.GetLength(1); col++)
            {
                int type = levelMap[row, col];         
                int up = CheckUp(row,col);
                int down = CheckDown(row,col);
                int left = CheckLeft(row,col);
                int right = CheckRight(row,col);
                float up_rotation = UpRotation(row,col);
                float down_rotation = DownRotation(row,col);
                float left_rotation = LeftRotation(row,col);
                float right_rotation = RightRotation(row,col);
                int[] neighbours = { up, right, down, left };
                float[] neighbours_rotation = {up_rotation, right_rotation, down_rotation, left_rotation};
                if (type == 3)
                {
                // can only determine direction of 3 after rotation of 4 is all done
                // *assuming at least one spare sprite between obstacles   

                    if (neighbours.Contains(1) || neighbours.Contains(2))
                    {
                        Debug.Log("invalid layout: check "+row+","+col);
                    }// the four sides should never be 1/2, otherwise debug.log("invalid layout: check"+row+","+col)

                    if (neighbours.Count(x => x is 0 or 5 or 6) > 2)
                    {
                        Debug.Log("invalid layout: check "+row+","+col);
                    }// no. sides that are 0/5/6 > 2: impossible; debug.log("invalid layout: check"+row+","+col)
                    else if (neighbours.Count(x => x is 0 or 5 or 6) == 2)
                    {
                        // up & right == 0/5/6: rotate = 0f
                        // up & left == 0/5/6: 90f
                        // left & down: 180f
                        // right & down: 270f
                        // right & left or up & down: invalid
                        if (up is 0 or 5 or 6 && left is 0 or 5 or 6)
                        {
                            rotate[row,col] = 90f;
                        }
                        if (left is 0 or 5 or 6 && down is 0 or 5 or 6)
                        {
                            rotate[row,col] = 180f;
                        }
                        if (right is 0 or 5 or 6 && down is 0 or 5 or 6)
                        {
                            rotate[row,col] = 270f;
                        }                                                
                    }// no. of sides that are 0/5/6 == 2: the other two sides form the corner
                    else if (neighbours.Count(x => x is 0 or 5 or 6) == 1)
                    {
                        if (neighbours.Count(x => x is 5 or 6) == 1)
                        {
                            Debug.Log("invalid layout: check"+row+","+col);
                            continue;
                        }
                        //。要不我assume no complex shape吧
                        
                    }//no. of side that is 0/5/6 == 1: if it is 5/6, debug.log("invalid layout: check"+row+","+col); else the opposite side of 0 must be part of the corner
                    else
                    {
                        
                    }
                tiles[row,col].transform.rotation =
                    Quaternion.Euler(0, 0, rotate[row,col]);    
                }

        

                    //no. of side that is 0/5/6 == 0, i.e. all sides are 3/4/7/8                       
            }
        }        



        
    }

}
