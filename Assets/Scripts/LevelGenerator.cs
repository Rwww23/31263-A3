using UnityEngine;

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

    int CheckUp(int row, int col)
    {
        if (row == 0)
            return -1;
        return levelMap[row-1,col];
    }
    int CheckDown(int row, int col)
    {
        if (row == levelMap.GetLength(0)-1)
            return -1;
        return levelMap[row+1,col];
    }
    int CheckLeft(int row, int col)
    {
        if (col == 0)
            return -1;
        return levelMap[row,col-1];
    }    
    int CheckRight(int row, int col)
    {
        if (col == levelMap.GetLength(1)-1)
            return -1;
        return levelMap[row,col+1];
    }    
    void Start()
    {
        Destroy(GameObject.Find("Level 01"));
        GameObject level = new GameObject("Level 01");
        Sprite[] sprites = new Sprite[9];
        for (int i = 0; i <= 8; i++)
        {
            sprites[i] = Resources.Load<Sprite>($"walls/{i}");
        }
        for (int row = 0; row < levelMap.GetLength(0); row++)
        {
            for (int col = 0; col < levelMap.GetLength(1); col++)
                {
                    float rotate = 0f;
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
                            rotate = 90f;
                    }
                    if (type == 2)
                    {
                        if (up is 1 or 2 or -1 && down is 1 or 2 or -1)
                            rotate = 90f;                        
                    }
                    
                    GameObject tile = new GameObject("Tile");
                    tile.transform.parent = level.transform;

                    SpriteRenderer renderer = tile.AddComponent<SpriteRenderer>();
                    renderer.sprite = sprites[type];
                    tile.transform.position = new Vector3(2*col, 2*(levelMap.GetLength(0)-row), 0);
                    tile.transform.rotation = Quaternion.Euler(0,0,rotate);
                }
            
        }



        
    }

}
