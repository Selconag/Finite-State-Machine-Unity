using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleStateManager : MonoBehaviour
{
    AppleStateBase currentState;
    public AppleStateGrowing GrowingState = new AppleStateGrowing();
    public AppleStateWhole WholeState = new AppleStateWhole();
    public AppleStateChewed ChewedState = new AppleStateChewed();
    public AppleStateRotten RottenState = new AppleStateRotten();


    // Start is called before the first frame update
    void Start()
    {
        currentState = GrowingState;

        currentState.EnterState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(AppleStateBase state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
