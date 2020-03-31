using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    [Range(0,1)]
    [SerializeField]
    float movementFactor; //0 for not moved, 1 for fully moved

    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)//checks if period is set to 0
            return;

        float cycles = Time.time / period; //grows contonually from 0

        const float tau = Mathf.PI * 2f; //about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); //goes from -1 to +1

        movementFactor = rawSinWave / 2f + 0.5f; // -1 / 2 = -0.5 + 0.5 = 0   // 1 / 2 = 0.5 + 0.5 = 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
