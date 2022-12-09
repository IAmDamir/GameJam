using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearPerk : PerksInterface
{
    private InputSys inp;
    private AnimationSys anim;
    private GameObject pm;
    private Spear sp;

    public SpearPerk(float _duration, float _cooldown, InputSys inputSys, AnimationSys animSys, GameObject playerModel, Spear spear)
    {
        duration = _duration;
        cooldown = _cooldown;
        inp = inputSys;
        anim = animSys;
        pm = playerModel;
        sp = spear;
    }

    public override void Code()
    {
        pm.transform.LookAt(inp.MouseWorldPos());

        sp.Shoot(sp.transform.position, sp.transform.rotation);
    }

    public void Rotate()
    {
        sp.transform.LookAt(inp.MouseWorldPos());
    }
}