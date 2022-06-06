
using UnityEngine;

public class AppleStateGrowing : AppleStateBase
{
    Vector3 startingAppleSize = new Vector3(0.1f, 0.1f, 0.1f);
    Vector3 growAppleScalar = new Vector3(0.1f,0.1f,0.1f);
    //Acts like constructor, initial state of apple when it is created or entered play mode.
    public override void EnterState(AppleStateManager apple)
    {
        apple.transform.localScale = startingAppleSize;
    }

    //Update the growing factor of apple depending on time until apple reaches its full growing potential.
    public override void UpdateState(AppleStateManager apple)
    {
        if(apple.transform.localScale.x < 1)
        {
            apple.transform.localScale += growAppleScalar * Time.deltaTime;
        }
        else
        {
            apple.SwitchState(apple.WholeState);
        }
    }

    //There is no interaction when it is still growing.
    public override void OnCollisionEnter(AppleStateManager apple, Collision collision)
    {

    }
}
