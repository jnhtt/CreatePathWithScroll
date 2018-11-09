using UnityEngine;

public class LookingDownCameraController : MonoBehaviour
{
    [SerializeField]
    private ControllerPanel controllerPanel;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        controllerPanel.Init();
    }
}
