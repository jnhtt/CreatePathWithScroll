using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static bool TryGetCameraPosition(Vector3 mpos, out Vector3 camPos)
    {
        camPos = Vector3.zero;
        var ray = CameraManager.Instance.Camera.ScreenPointToRay(mpos);
        var plane = new Plane(Vector3.up, 0f);
        float enter = 0f;
        var camTrans = CameraManager.Instance.Camera.transform;
        if (plane.Raycast(ray, out enter)) {
            float y = camTrans.position.y;
            Vector3 pos = ray.origin + enter * ray.direction;
            var camPlane = new Plane(-Vector3.up, y);
            var invRay = new Ray(pos, -camTrans.forward);
            if (camPlane.Raycast(invRay, out enter)) {
                camPos = invRay.origin + enter * invRay.direction;
                return true;
            }
        }
        return false;
    }

    public static bool TryGetHitInfo(Vector3 mpos, int layerMask, out RaycastHit hit)
    {
        var ray = CameraManager.Instance.Camera.ScreenPointToRay(mpos);
        return Physics.Raycast(ray, out hit, float.MaxValue, layerMask);
    }
}
