using UnityEngine;

public abstract class AppleStateBase
{
    public abstract void EnterState(AppleStateManager apple);

    public abstract void UpdateState(AppleStateManager apple);

    public abstract void OnCollisionEnter(AppleStateManager apple,Collision collision);

}
