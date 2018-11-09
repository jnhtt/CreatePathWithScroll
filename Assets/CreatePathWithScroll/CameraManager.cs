using UnityEngine;

public class CameraManager : SingletonMonoBehaviour<CameraManager>
{
    [SerializeField]
    private Camera refCamera;

    private bool reservedFlag = false;
    private Vector3 reservedPosition;

    public Camera Camera
    {
        get { return refCamera; }
        private set { refCamera = value; }
    }

    public void ReservePosition(Vector3 p)
    {
        reservedFlag = true;
        reservedPosition = p;
    }

    public void Move(Vector3 mov)
    {
        ReservePosition(Camera.transform.position + mov);
    }

    public void SetPosition(Vector3 p)
    {
        Camera.transform.position = p;
    }

    public void SetRotation(Quaternion rot)
    {
        Camera.transform.rotation = rot;
    }

    private void FixedUpdate()
    {
        if (reservedFlag) {
            SetPosition(reservedPosition);
            reservedFlag = false;
        }
    }
}
