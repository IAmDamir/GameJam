using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSys : MonoBehaviour
{
    public Animator anim;
    public AnimatorClipInfo[] clipInfo;

    private void Awake()
    {
        clipInfo = anim.GetCurrentAnimatorClipInfo(0);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        clipInfo = anim.GetCurrentAnimatorClipInfo(0);
    }

    public void Walk(float speed)
    {
        if (clipInfo.Length > 0 && clipInfo[0].clip.name != "Walk-Idle")
        {
            anim.Play("Walk-Idle");
        }

        anim.SetFloat("Speed", Mathf.MoveTowards(anim.GetFloat("Speed"), speed, 0.125f));
    }

    public void BowUse()
    {
        if (clipInfo[0].clip.name != "bow")
        {
            anim.Play("bow");
        }
    }

    public void Play(string clipName)
    {
        anim.Play(clipName);
    }

    public AnimatorStateInfo StateInfo(int num = 0)
    {
        return anim.GetCurrentAnimatorStateInfo(num);
    }
}