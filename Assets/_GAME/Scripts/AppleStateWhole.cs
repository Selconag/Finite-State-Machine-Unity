using UnityEngine;

public class AppleStateWhole : AppleStateBase
{
    [Range(0,20f)]
    public float rottenCountdown = 10.0f;
    [Range(0, 5f)]
    public float healthAmount = 2.0f;

    public override void EnterState(AppleStateManager apple)
    {
        Debug.Log("Entered full state");
        apple.GetComponent<Rigidbody>().useGravity = true;
    }

    public override void UpdateState(AppleStateManager apple)
    {
        if(rottenCountdown >= 0)
        {
            rottenCountdown -= Time.deltaTime;
        }
        else
        {
            apple.SwitchState(apple.RottenState);
        }
    }

    public override void OnCollisionEnter(AppleStateManager apple, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ChangeHealth(healthAmount);

            apple.SwitchState(apple.ChewedState);
        }
    }
}
