using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BowPerk : PerksInterface
{
    private InputSys inp;
    private AnimationSys anim;
    private GameObject pm;
    private ProjectileSpawner prSpawner;
    private Transform plSpawner;

    public BowPerk(float _duration, float _cooldown, InputSys inputSys, AnimationSys animSys, GameObject playerModel, ProjectileSpawner spawner, Transform projectileSpawner)
    {
        duration = _duration;
        cooldown = _cooldown;
        inp = inputSys;
        anim = animSys;
        pm = playerModel;
        prSpawner = spawner;
        plSpawner = projectileSpawner;
    }

    public override void Code()
    {
        pm.transform.LookAt(inp.MouseWorldPos());
        //prSpawner.transform.rotation = pm.transform.rotation;

        anim.BowUse();
        prSpawner.Shoot(plSpawner.position, plSpawner.rotation);
        Debug.Log("pew at " + inp.MouseWorldPos());
        /*if(anim.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            Debug.Log("pew at " + inp.MouseWorldPos());
        }*/
    }

    public void Rotate()
    {
        prSpawner.transform.rotation = pm.transform.rotation;
    }
}