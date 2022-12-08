using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPerk : PerksInterface
{
    InputSys inp;
    Rigidbody rb;
    float speed;

    public DashPerk(float _duration, float _cooldown, InputSys inputSys, Rigidbody rigidbody, float _speed)
    {
        duration = _duration;
        cooldown = _cooldown;
        rb = rigidbody;
        inp = inputSys;
        speed = _speed;
    }

    public override void Code()
    {
        rb.velocity = inp.direction * Mathf.Pow(speed, 3);
        //throw new System.NotImplementedException();
    }
}
