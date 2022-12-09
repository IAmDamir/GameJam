using UnityEngine;

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

        anim.BowUse();
        prSpawner.Shoot(plSpawner.position, plSpawner.rotation);
    }

    public void Rotate()
    {
        prSpawner.transform.LookAt(inp.MouseWorldPos());
    }
}