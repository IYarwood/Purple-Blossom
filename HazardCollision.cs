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
        //8 is the layer number of the player
        if (other.gameObject.layer == 8)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Die();
                Debug.Log("Player Hit");
            }
        }
    }
}
