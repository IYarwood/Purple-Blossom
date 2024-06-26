using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        var script = other.gameObject.GetComponent<TestingPlayerScript>();
        var hazardScript = other.gameObject.GetComponent<Vortex>();
        //8 is the layer number of the player
        if (other.gameObject.layer == 8 && hazardScript.active == false)
        {
            var player = other.GetComponent<TestingPlayerScript>();
            if (player != null)
            {
                player.Die();
                Debug.Log("Player Hit");
            }
        }
    }
}
