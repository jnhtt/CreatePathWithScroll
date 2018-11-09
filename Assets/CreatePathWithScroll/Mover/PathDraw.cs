using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDraw : MonoBehaviour
{
    [SerializeField]
    protected LineRenderer lineRenderer;

    protected List<Vector3> pathPointList;
    protected int currentIndex;
    protected Transform target;

    public void Show(bool flag)
    {
        lineRenderer.enabled = flag;
    }

    public virtual void SetPath(Transform target, List<Vector3> path)
    {
        currentIndex = 0;
        pathPointList = new List<Vector3>(path);
        lineRenderer.positionCount = pathPointList.Count;
        lineRenderer.SetPositions(pathPointList.ToArray());
        this.target = target;
    }

    public virtual void Remove()
    {
        pathPointList.RemoveAt(0);
        if (pathPointList.Count > 1)
        {
            lineRenderer.positionCount = pathPointList.Count;
            lineRenderer.SetPositions(pathPointList.ToArray());
            lineRenderer.SetPosition(0, target.position);
        }
        else
        {
            Show(false);
        }
    }

    private void Update()
    {
        if (target != null)
        {
            lineRenderer.SetPosition(0, target.position);
        }
    }
}