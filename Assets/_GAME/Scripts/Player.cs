using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1,25f)]
    [SerializeField] protected float Health = 10f;
    public void ChangeHealth(float Amount)
    {
        Health += Amount;
    }
}
