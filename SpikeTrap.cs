using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private enum State
    {
        Lowered,
        Lowering,
        Raising,
        Raised
    }

    private State state = State.Lowered;
    private const float SpikeHeight = 3.6f;
    private const float LoweredSpikeHeight = .08f;

    [Header("Stats")]
    [Tooltip("Time in seconds after lowering the spikes before raising them again.")]
    public float interval = 2f;

    [Tooltip("Time in seconds after raising the spikes before they start lowering again.")]
    public float raiseWaitTime = .3f;

    [Tooltip("Time in seconds taken to fully lower the spikes.")]
    public float lowerTime = .6f;

    [Tooltip("Time in seconds taken to fully raise the spikes.")]
    public float raiseTime = .08f;

    private float lastSwitchTime = Mathf.NegativeInfinity;

    [Header("References")]
    [Tooltip("Reference to the parent of all the spikes.")]
    public Transform spikeHolder;

    public GameObject HitBoxGameObject;
    public GameObject ColliderGameObject;

    void StartRaising()
    {
        lastSwitchTime = Time.time;
        state = State.Raising;
        HitBoxGameObject.SetActive(true);
    }

    void StartLowering()
    {
        lastSwitchTime = Time.time;
        state = State.Lowering;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Spikes lowered by default
        Invoke("StartRaising", interval);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Lowering)
        {
            //Get the spike holder local scale
            Vector3 scale = spikeHolder.localScale;

            //Update Y scale by lerping from max heigh to min height
            scale.y = Mathf.Lerp(SpikeHeight, LoweredSpikeHeight, (Time.time - lastSwitchTime) / lowerTime);

            //Apply updated scale to spike holder
            spikeHolder.localScale = scale;

            //If spikes finished lowering
            if (scale.y == LoweredSpikeHeight)
            {
                //Update state and Invoke next raising in interval seconds
                Invoke("StartRaising", interval);
                state = State.Lowered;
                ColliderGameObject.SetActive(false);
            }
        }
        else if (state == State.Raising)
        {
            //Get spike holder local scal
            Vector3 scale = spikeHolder.localScale;

            //Update Y scale by lerping from min to max
            scale.y = Mathf.Lerp(LoweredSpikeHeight, SpikeHeight, (Time.time - lastSwitchTime) / raiseTime);

            //Apply updated scale to spike holder
            spikeHolder.localScale = scale;

            //If spikes finished raising
            if (scale.y == SpikeHeight)
            {
                //Update state and Invoke next lowering
                Invoke("StartLowering", raiseWaitTime);
                state = State.Raised;

                //Activate Collider to block player
                ColliderGameObject.SetActive(true);

                //Deactivate HitBox so it doesn't kill player
                HitBoxGameObject.SetActive(false);
            }
        }
    }
}
