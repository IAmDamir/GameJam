using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxAttack : MonoBehaviour
{
    [SerializeField] private int attackPower = 1;

    private void OnTriggerEnter(Collider other)
    {
    }
}