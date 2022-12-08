using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowPerk : PerksInterface
{
    InputSys inp;
    AnimationSys anim;
    GameObject pm;

    public BowPerk(float _duration, float _cooldown, InputSys inputSys, AnimationSys animSys, GameObject playerModel)
    {
        duration = _duration;
        cooldown = _cooldown;
        inp = inputSys;
        anim = animSys;
        pm = playerModel;
    }

    public override void Code()
    {
        pm.transform.LookAt(inp.MouseWorldPos());
        anim.BowUse();
        Debug.Log("pew at " + inp.MouseWorldPos());
        /*if(anim.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            Debug.Log("pew at " + inp.MouseWorldPos());
        }*/
        //throw new System.NotImplementedException();
    }
}
