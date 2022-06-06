
using UnityEngine;

public class AppleStateRotten : AppleStateBase
{
    [Range(0, 10f)]
    public float destroyCountdown = 5.0f;
    [Range(0, 5f)]
    public float healthAmount = 2.0f;
    public override void EnterState(AppleStateManager apple)
    {

    }

    public override void UpdateState(AppleStateManager apple)
    {

    }

    public override void OnCollisionEnter(AppleStateManager apple, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ChangeHealth(-healthAmount);

            apple.SwitchState(apple.ChewedState);
        }
    }
}
