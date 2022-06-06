
using UnityEngine;

public class AppleStateChewed : AppleStateBase
{
    [Range(0, 10f)]
    public float destroyCountdown = 5.0f;
    public override void EnterState(AppleStateManager apple)
    {
        //Animator animator = apple.GetComponent<Animator>();
        //animator.Play("Base Layer.eat", 0, 0);
    }

    public override void UpdateState(AppleStateManager apple)
    {
        if (destroyCountdown >= 0)
        {
            destroyCountdown -= Time.deltaTime;
        }
    }

    public override void OnCollisionEnter(AppleStateManager apple, Collision collision)
    {

    }
}