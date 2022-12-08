using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSys : MonoBehaviour
{
    public AudioSource[] stepsMarble;
    public AudioSource[] stepsStone;
    public float stepTime;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StepSounds(AudioSource[] stepType)
    {
        timer += Time.deltaTime;
        if(timer >= stepTime)
        {
            timer = 0;
            stepType[Random.Range(0, stepType.Length)].Play();
        }
    }
}
