using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private PathDraw pathDraw;
    private PathMove pathMove;

    private bool shouldMove = false;

    private void Awake()
    {
        pathMove = new PathMove();
    }

    public void SetPathPointList(List<Vector3> pathPointList)
    {
        pathMove.SetPath(pathPointList);
        pathDraw.SetPath(transform, pathPointList);
    }

    public void StartMove()
    {
        shouldMove = true;
        pathMove.StartMove(8f);
        ShowPath(true);
    }

    public void StopMove()
    {
        shouldMove = false;
        ShowPath(false);
        pathMove.StopMove();
    }

    public void ShowPath(bool flag)
    {
        pathDraw.Show(flag);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        if (shouldMove && pathMove != null && !pathMove.IsEnd) {
            transform.position = pathMove.GetPosition(deltaTime, RemovePoint);
        }
    }

    private void RemovePoint()
    {
        if (pathDraw != null) {
            pathDraw.Remove();
        }
    }
}
