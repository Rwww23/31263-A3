using UnityEngine;
using System.Collections.Generic;

public class Tweener : MonoBehaviour
{
    private Tween activeTween;
    private List<Tween> activeTweens = new List<Tween>();

    public bool TweenExists(Transform target)
    {
        return activeTweens.Exists(tween => tween.Target == target);
    }

    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (TweenExists(targetObject))
        {
            return false;
        }
        else
        {
            Tween newTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
            activeTweens.Add(newTween);
            return true;
        }


    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (Tween activeTween in activeTweens)
        for (int i = activeTweens.Count - 1; i >= 0; i--)
        {
            Tween activeTween = activeTweens[i];
            if (activeTween == null)
            {
                continue;
            }
            if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
            {
                float t = (Time.time - activeTween.StartTime) / activeTween.Duration;
                t = Mathf.Pow(t, 3); 
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, t);
            }
            else
            {
                activeTween.Target.position = activeTween.EndPos;
                activeTweens.RemoveAt(i);
            }            
        }

    }
}
