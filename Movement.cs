using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{   //reference to the transform of the player 
    public Transform transform
    //reference to the players movespeed 
    public float movespeed = 24f;
    //reference to the players max speed 
    public float timeToMaxSpeed = .26f;
    // reference to the acceleration of the player. (calls on the other variable values)
    private float VelocityGainPerSecond { get { return movespeed / timeToMaxSpeed; } }
    // Time to lose speed specified in default units "f"
    public float timeToLoseMaxSpeed = .2f;
    // the decay in speed once key is no longer pressed.
    private float VelocityLossPerSecond { get { return movespeed / timeToLoseMaxSpeed; } } 
    public PlayerStates playerStates;
    //A table containing all the player states for easy switching between states.
    public enum PlayerStates
   {
        Alive,
        Dead,
        Running,
        WallRunning,
   }
    
    

   // A method to controll player movement based on enum state
   public void PlayerMovment()
    {// A rough sketch of how the player will move when they are alive 
        if (playerStates == PlayerStates.Alive)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (movementVelocity.z >= 0) //If we're already moving forward
                {
                    //Increase Z velocity by VelocityGainPerSecond, but don't go higher than movespeed
                    movementVelocity.z = Mathf.Min(movespeed, movementVelocity.z + VelocityGainPerSecond * Time.deltaTime);
                    
                    // ***need to add left and right movement***
               
                }


            }
        }
        
        
        
        
        // will contain all code for death affects such as animations 
        else if (playerStates == PlayerStates.Dead)




        // will be used for any speedboosts that we assign be it a item or otherwise 
        else if (playerStates == PlayerStates.Running)




        // will be used to let the player wall run
        else if (playerStates == PlayerStates.WallRunning)

                        // there is a section in the book simular to this 
                           // i will look into it this week when I get a chance 









        }

    // Start is called before the first frame update
    void Start()
    {
        playerStates = PlayerStates.Alive; 
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovment();  
           
    }
}
