using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMove
{
    protected List<Vector3> pathPointList;
    protected float speed;
    protected int currentIndex;
    protected float movedDistance;

    public bool IsEnd { get; set; }
    public int CurrentIndex { get { return currentIndex; } }

    public virtual void SetPath(List<Vector3> path)
    {
        pathPointList = new List<Vector3>(path);
    }

    public virtual void StartMove(float speed)
    {
        IsEnd = false;
        this.speed = speed;
        movedDistance = 0f;
        currentIndex = 0;
    }

    public virtual void StopMove()
    {
        IsEnd = true;
        movedDistance = 0f;
        currentIndex = 0;
        if (pathPointList != null) {
            pathPointList.Clear();
        }
    }

    public virtual Vector3 GetPosition(float deltaTime, System.Action onRemove)
    {
        if (pathPointList == null) {
            return Vector3.zero;
        }
        int last = pathPointList.Count - 1;
        int startIndex = currentIndex;
        int endIndex = currentIndex + 1;
        if (last < endIndex)
        {
            IsEnd = true;
            return pathPointList[pathPointList.Count - 1];
        }

        Vector3 start = pathPointList[startIndex];
        Vector3 end = pathPointList[endIndex];
        float dist = Vector3.Distance(end, start);

        movedDistance += deltaTime * speed;

        while (dist <= movedDistance && startIndex < last)
        {
            currentIndex++;
            if (onRemove != null)
            {
                onRemove();
            }
            startIndex = currentIndex;
            endIndex = currentIndex + 1;
            movedDistance -= dist;
            if (last < endIndex)
            {
                endIndex = startIndex;
            }

            start = pathPointList[startIndex];
            end = pathPointList[endIndex];
            dist = Vector3.Distance(end, start);
        }

        float t = dist <= 0f ? 1f : movedDistance / dist;
        return t * end + (1f - t) * start;
    }
}
