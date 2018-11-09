using UnityEngine;

public interface IInputOperation
{
    void OnPointerDown(InputData inputData);
    void OnPointerUp(InputData inputData);
    void OnPointerExit(InputData inputData);
    void OnBeginDrag(InputData inputData);
    void OnDrag(InputData inputData);
    void OnEndDrag(InputData inputData);

    void Update(InputData inputData, float deltatime);
}

public class BaseInputOperation : IInputOperation
{
    public virtual void OnPointerDown(InputData inputData) {}
    public virtual void OnPointerUp(InputData inputData) {}
    public virtual void OnPointerExit(InputData inputData) {}
    public virtual void OnBeginDrag(InputData inputData) {}
    public virtual void OnDrag(InputData inputData) {}
    public virtual void OnEndDrag(InputData inputData) {}

    public virtual void Update(InputData inputData, float deltaTime) {}
}
