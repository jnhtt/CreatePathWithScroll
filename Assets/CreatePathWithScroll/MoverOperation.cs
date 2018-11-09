using System.Collections.Generic;
using UnityEngine;

public class MoverOperation : BaseInputOperation
{
    private const float ADD_PATH_POINT_INTERVAL = 0.1f;
    private const float VIEWPORT_EDGE_LEFT = 0.1f;
    private const float VIEWPORT_EDGE_RIGHT = 0.9f;
    private const float VIEWPORT_EDGE_UP = 0.9f;
    private const float VIEWPORT_EDGE_DOWN = 0.1f;

    private const float SCROLL_SPEED = 10f;
    private static readonly Vector3 UP_OFFSET = new Vector3(0f, 0.2f, 0f);

    private Mover mover;
    private List<Vector3> pathPointList;
    private bool dragFlag;
    private bool addPathPointByEventFlag;
    private float addPathPointTimer;

    public MoverOperation(Mover mover)
    {
        this.mover = mover;
        pathPointList = new List<Vector3>();
    }

    private void AddPathPoint(Vector3 point)
    {
        pathPointList.Add(point);
        if (mover != null)
        {
            mover.SetPathPointList(pathPointList);
        }
    }

    public override void OnBeginDrag(InputData inputData)
    {
        base.OnBeginDrag(inputData);
        pathPointList.Clear();
        pathPointList.Add(mover.transform.position + UP_OFFSET);
        addPathPointByEventFlag = true;
        addPathPointTimer = 0f;
        if (mover != null) {
            mover.StopMove();
            mover.ShowPath(true);
            dragFlag = true;
        }
    }

    public override void OnDrag(InputData inputData)
    {
        if (addPathPointTimer < ADD_PATH_POINT_INTERVAL) {
            return;
        }

        base.OnDrag(inputData);
        RaycastHit hit;
        if (Utils.TryGetHitInfo(inputData.screenPosition, 1 << LayerMask.NameToLayer("Ground"), out hit)) {
            AddPathPoint(hit.point + UP_OFFSET);
            addPathPointTimer = 0f;
        }
    }

    public override void OnEndDrag(InputData inputData)
    {
        base.OnEndDrag(inputData);
        if (mover != null) {
            mover.StartMove();
        }
        dragFlag = false;
    }

    public override void Update(InputData inputData, float deltaTime)
    {
        base.Update(inputData, deltaTime);
        if (dragFlag) {
            MoveCameraInScreenEdge(inputData.screenPosition);
            if (!addPathPointByEventFlag && mover != null && addPathPointTimer > ADD_PATH_POINT_INTERVAL) {
                RaycastHit hit;
                if (Utils.TryGetHitInfo(inputData.screenPosition, 1 << LayerMask.NameToLayer("Ground"), out hit)) {
                    AddPathPoint(hit.point + UP_OFFSET);
                }
                addPathPointTimer = 0f;
            }
            addPathPointByEventFlag = false;
        }
        addPathPointTimer += deltaTime;
    }

    private void MoveCameraInScreenEdge(Vector2 screenPos)
    {
        var viewportPos = CameraManager.Instance.Camera.ScreenToViewportPoint(screenPos);
        viewportPos.z = 0f;

        Vector3 mov = Vector3.zero;
        if (viewportPos.x < VIEWPORT_EDGE_LEFT) {
            mov.x = viewportPos.x - VIEWPORT_EDGE_LEFT;
        } else if (viewportPos.x > VIEWPORT_EDGE_RIGHT) {
            mov.x = viewportPos.x - VIEWPORT_EDGE_RIGHT;
        }
        if (viewportPos.y < VIEWPORT_EDGE_DOWN) {
            mov.z = viewportPos.y - VIEWPORT_EDGE_DOWN;
        } else if (viewportPos.y > VIEWPORT_EDGE_UP) {
            mov.z = viewportPos.y - VIEWPORT_EDGE_UP;
        }

        CameraManager.Instance.Move(mov * SCROLL_SPEED);
    }
}
