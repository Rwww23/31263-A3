using Unity.VisualScripting;
using UnityEngine;

public class p_movement : MonoBehaviour
{
    Tween currentTween;
    [SerializeField] float duration = 2f;

    void MoveTo(Vector3 destination)
    {
        currentTween = new Tween(
        transform,
        transform.position,
        destination,
        Time.time,
        duration            
        );
    }
    Vector3 CornerPosition(Vector3 corner)
    {
        return new Vector3(2 * corner.y, 2 * 15 - corner.x, 0);
    }

    // tile.transform.position = (2*col, 2*(levelMap.GetLength(0)-row), 0); levelMap.GetLength(0) = 15

    void Start()
    {
        Vector3 corner1 = CornerPosition(new Vector3(1,1,0));
        Vector3 corner2 = CornerPosition(new Vector3(1,6,0));
        Vector3 corner3 = CornerPosition(new Vector3(6,6,0));
        Vector3 corner4 = CornerPosition(new Vector3(6,1,0));
        transform.position = corner1;
    }

    void Update()
    {

            float t = (Time.time - currentTween.StartTime) / currentTween.Duration; 
            transform.position = Vector3.Lerp(currentTween.StartPos,currentTween.EndPos,t);
    
    }


}
