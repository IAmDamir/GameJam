using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private AudioSys audio;

    public GameObject playerModel;

    public AnimationSys anim;

    private InputSys inputSys;
    public Rigidbody rb;
    public float speed;

    private DashPerk dash;
    public float dashDuration;
    public float dashCooldown;

    private BowPerk bow;
    public float bowDuration;
    public float bowCooldown;

    public GameObject frwrd;
    private Vector3 rght;

    // Start is called before the first frame update
    private void Start()
    {
        audio = GetComponent<AudioSys>();
        inputSys = GetComponent<InputSys>();
        rb = GetComponent<Rigidbody>();
        rght = Quaternion.Euler(new Vector3(0, 90, 0)) * frwrd.transform.forward;

        dash = new DashPerk(dashDuration, dashCooldown, inputSys, rb, speed);
        bow = new BowPerk(bowDuration, bowCooldown, inputSys, anim, playerModel);
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
        //Bow
        inputSys.Bow();
        bow.Action(inputSys._bow);
    }
}