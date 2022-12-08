using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject playerModel;

    [Header("Systems")]
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

    private void Awake()
    {
        inputSys = GetComponent<InputSys>();

        inputSys.GetControls().Actions.Bow.performed += BowUse;
    }

    private void BowUse(InputAction.CallbackContext ctx)
    {
        bow.Action(true);
        bow.Rotate();
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
        //Walkng
        inputSys.IsometricMovement(frwrd.transform.forward, rght);

        //Vector3 look
        if (bow.timerAction == 0)
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