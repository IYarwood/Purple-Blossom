using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{

    public GameObject model;
    public GameObject secondaryModel;
    private bool state = true;

    private float timeSinceActivation;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var script = gameObject.GetComponent<TestingPlayerScript>();
        if (Input.GetKeyDown(KeyCode.L) && script.hasVortex == true)
        {
            if (state == true)
            {
                model.SetActive(false);
                state = false;
                secondaryModel.SetActive(true);
                timeSinceActivation = Time.time;
            }
        }

        if (Time.time - timeSinceActivation >= 2)
        {
            model.SetActive(true);
            secondaryModel.SetActive(false);
            state = true;
        }
        

    }
}
