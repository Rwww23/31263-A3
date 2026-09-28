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
                    int type = levelMap[row, col];
                    
                    if (type == 0)
                        continue;
                    
                    GameObject tile = new GameObject("Tile");
                    tile.transform.parent = level.transform;

                    SpriteRenderer renderer = tile.AddComponent<SpriteRenderer>();
                    renderer.sprite = sprites[type];
                    tile.transform.position = new Vector3(2*col, 2*(levelMap.GetLength(0)-row), 0);
                }
            
        }



        
    }

}
