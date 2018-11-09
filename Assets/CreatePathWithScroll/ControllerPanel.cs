using UnityEngine;
using UnityEngine.EventSystems;

public class InputData
{
    public Vector3 screenPosition;
    public Vector3 previousCameraPosition;
}

public class ControllerPanel
    : UIBehaviour
    , IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private const int EMPTY = int.MinValue;

    private InputData inputData;
    private IInputOperation inputOperation;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
	{
        this.inputOperation = null;
        inputData = new InputData();
	}

	public void OnPointerDown(PointerEventData pointerEventData)
    {
        inputOperation = CreateInputOperation(pointerEventData.position);
        if (inputOperation != null) {
            inputData.screenPosition = pointerEventData.position;
            inputOperation.OnPointerDown(inputData);
        }
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (inputOperation != null) {
            inputData.screenPosition = pointerEventData.position;
            inputOperation.OnPointerUp(inputData);
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (inputOperation != null) {
            inputData.screenPosition = pointerEventData.position;
            inputOperation.OnPointerExit(inputData);
        }
    }
    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if (inputOperation != null) {
            inputData.screenPosition = pointerEventData.position;
            inputOperation.OnBeginDrag(inputData);
        }
    }
    public void OnDrag(PointerEventData pointerEventData)
    {
        if (inputOperation != null) {
            inputData.screenPosition = pointerEventData.position;
            inputOperation.OnDrag(inputData);
        }
    }
    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (inputOperation != null) {
            inputData.screenPosition = pointerEventData.position;
            inputOperation.OnEndDrag(inputData);
        }
    }
    private IInputOperation CreateInputOperation(Vector2 screenPos)
    {
        int objectLayer = 1 << LayerMask.NameToLayer("Object");
        RaycastHit hit;
        if (Utils.TryGetHitInfo(screenPos, objectLayer, out hit))
        {
            Mover mover = hit.collider.transform.GetComponent<Mover>();
            if (mover == null) {
                mover = hit.collider.gameObject.AddComponent<Mover>();
            }
            return new MoverOperation(mover);
        }
        return new LookingDownCameraOperation();
    }

    private void Update()
    {
        if (inputOperation != null) {
            inputOperation.Update(inputData, Time.deltaTime);
        }
    }
}
