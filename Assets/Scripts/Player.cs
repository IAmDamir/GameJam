using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private enum Attacks
    {
        Attack1, Attack2, Attack3
    }

    [Header("Systems")]
    public GameObject playerModel;

    private AudioSys audio;

    public AnimationSys anim;
    private InputSys inputSys;

    [Header("Movement")]
    public GameObject frwrd;

    private Vector3 rght;
    public Rigidbody rb;
    public float speed;

    [Header("Dash")]
    private DashPerk dash;

    public float dashDuration;
    public float dashCooldown;

    [Header("Bow")]
    private BowPerk bow;

    public float bowDuration;
    public float bowCooldown;
    [SerializeField] private ProjectileSpawner projSpawner;
    [SerializeField] private Transform projSpawnerTransform;

    [Header("Attack")]
    private float attackTime = 0;

    private float nextAttackTime = 0;
    public static int noOfClicks = 0;
    private float lastClickedTime = 0;
    private float MaxComboDelay = 1;
    private bool isAttaking = false;
    [SerializeField] private GameObject[] AttackColliders;

    private Dictionary<Attacks, string> animationList = new Dictionary<Attacks, string>
    {
        { Attacks.Attack1, "Stabbing"},
        { Attacks.Attack2, "Stable Sword Inward Slash"},
        { Attacks.Attack3, "Upward Thrust"}
    };

    private void Awake()
    {
        inputSys = GetComponent<InputSys>();

        inputSys.GetControls().Actions.Bow.performed += _ => BowUse();
        inputSys.GetControls().Actions.Attack.performed += _ => Attack();
    }

    private void BowUse()
    {
        bow.Action(true);
        bow.Rotate();
    }

    private void Attack()
    {
        if (Time.time <= nextAttackTime)
            return;

        Vector3 pos = inputSys.MouseWorldPos()
; playerModel.transform.LookAt(pos);
        foreach (var attack in AttackColliders)
        {
            attack.transform.LookAt(pos);
        }

        isAttaking = true;
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            anim.Play(animationList[Attacks.Attack1]);
            StartCoroutine(AttackCollider(AttackColliders[0], 0.1f, 0.5f));
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 4);

        if (noOfClicks >= 2 && anim.StateInfo().normalizedTime > 0.8 && anim.StateInfo().IsName(animationList[Attacks.Attack1]))
        {
            anim.Play(animationList[Attacks.Attack2]);
            StartCoroutine(AttackCollider(AttackColliders[1], 0.1f, 0.4f));
        }
        if (noOfClicks >= 3 && anim.StateInfo().normalizedTime > 0.8 && anim.StateInfo().IsName(animationList[Attacks.Attack2]))
        {
            anim.Play(animationList[Attacks.Attack3]);
            StartCoroutine(AttackCollider(AttackColliders[1], 0.2f, 0.6f));
        }
    }

    private IEnumerator AttackCollider(GameObject collider, float startup, float active)
    {
        yield return new WaitForSeconds(startup);
        collider.SetActive(true);
        yield return new WaitForSeconds(active);
        collider.SetActive(false);
    }

    // Start is called before the first frame update
    private void Start()
    {
        audio = GetComponent<AudioSys>();
        rb = GetComponent<Rigidbody>();
        rght = Quaternion.Euler(new Vector3(0, 90, 0)) * frwrd.transform.forward;

        dash = new DashPerk(dashDuration, dashCooldown, inputSys, rb, speed);
        bow = new BowPerk(bowDuration, bowCooldown, inputSys, anim, playerModel, projSpawner, projSpawnerTransform);
    }

    // Update is called once per frame
    private void Update()
    {
        if (anim.StateInfo().normalizedTime > 0.8 && anim.StateInfo().IsName(animationList[Attacks.Attack3]))
        {
            noOfClicks = 0;
            isAttaking = false;
        }

        if (Time.time - lastClickedTime > MaxComboDelay)
        {
            noOfClicks = 0;
            isAttaking = false;
        }

        attackTime = Time.deltaTime;
        //Walkng
        inputSys.IsometricMovement(frwrd.transform.forward, rght);

        //Vector3 look
        if (bow.timerAction == 0 && !isAttaking)
        {
            playerModel.transform.LookAt(transform.position + inputSys.direction);
            rb.velocity = inputSys.direction * speed;
            if (inputSys.direction != new Vector3())
            {
                audio.StepSounds(audio.stepsMarble);
                anim.Walk(1);
            }
            else
                anim.Walk(0);
        }

        //Dash
        inputSys.Dash();
        dash.Action(inputSys._dash);

        bow.Action(false);
    }
}