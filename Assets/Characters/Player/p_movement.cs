using Unity.VisualScripting;
using UnityEngine;

public class p_movement : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    int movement = 0;
    float startTime = 0;
    Vector3 corner0;
    Vector3 corner1;
    Vector3 corner2;
    Vector3 corner3;

    
    Vector3 CornerPosition(Vector3 corner)
    {
        return new Vector3(2 * corner.y, 2 * (15 - corner.x), 0);
    }

    // tile.transform.position = (2*col, 2*(levelMap.GetLength(0)-row), 0); levelMap.GetLength(0) = 15

    
    bool Move(Vector3 cornerA, Vector3 cornerB, float startTime)//TODO: move from one corner to another
    {
        float distance = Vector3.Distance(cornerA, cornerB);
        float duration = distance/speed;
        float t_fraction = (Time.time - startTime)/ duration; 
        if (t_fraction >= 1)
        {
            transform.position = cornerB;
            this.startTime = Time.time;
            return true;
        }
        transform.position = Vector3.Lerp(cornerA, cornerB, t_fraction);
        return false;
    }


    void Start()
    {
        corner0 = CornerPosition(new Vector3(1,1,0));
        corner1 = CornerPosition(new Vector3(1,6,0));
        corner2 = CornerPosition(new Vector3(5,6,0));
        corner3 = CornerPosition(new Vector3(5,1,0));
        Debug.Log(corner0);
        Debug.Log(corner1);
        Debug.Log(corner2);
        Debug.Log(corner3);
        transform.position = corner0;
        startTime = Time.time;
    }

    void Update()
    {
        
        switch (movement)
        {
            case 0:
                if (Move(corner0, corner1, startTime))
                    movement = 1;
                break;
            case 1:
                if (Move(corner1, corner2, startTime))
                    movement = 2;
                break;
            case 2:
                if (Move(corner2, corner3, startTime))
                    movement = 3;
                break;
            case 3:
                if (Move(corner3, corner0, startTime))
                    movement = 0;
                break;                                

        }
    
    }


}
