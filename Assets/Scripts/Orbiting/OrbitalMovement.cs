using System;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour
{
    private Transform player; //Var to hold player transform

    [Tooltip("Determines how far away the enemy is from the player.")]
    [SerializeField, Range(3, 7)]private float radius = 3f; //Var to determine radius

    /*Var for determining the base rotation speed
     * Would be better served by a propertydrawer with a dropdown like last week's lab
     * as then I could cut out an if statement later, but for the purposes of this lab
     * I think this will do.
    */
    [Tooltip("Enter a positive number to go counter-clockwise enter a negative number to go clockwise.")]
    [SerializeField, Range(-1, 1)] private float rotationSpeed = 1; 

    [Tooltip("Determines the angle around the player this enemy will start at.")]
    [SerializeField, Range(0, 360)] private float startingAngle = 0f; //Var for determining the enemy's starting angle
    private float currentAngle; //var for storing the current angle the enemy is at

    void Start()
    {
        //Try to find the player's transform, if this fails throw an error
        try
        {
            player = GameObject.Find("Player").GetComponent<Transform>();
        }
        catch
        {
            throw new ArgumentException("Unable to find player game object!", nameof(OrbitalMovement));
        }

        //Set the current angle = starting angle
        currentAngle = startingAngle;
    }

    void Update()
    {
        MoveShip();
    }

    //Func to handle rotating the ship around the player
    private void MoveShip()
    {
        //Increase the current angle
        currentAngle += DetermineSpeed() * Time.deltaTime;


        //If currentAngle goes above or below the max or min value wrap around to the other value
        if (currentAngle >= 360)
        {
            currentAngle = 0;
        }
        else if (currentAngle <= 0)
        {
            currentAngle = 360;
        }
        //Determine the coords for the x and z axis
        float shipPosX = player.position.x + (radius * Mathf.Cos(currentAngle));
        float shipPosZ = player.position.z + (radius * Mathf.Sin(currentAngle));

        //set the enemy's position
        Vector3 shipPos = new Vector3(shipPosX, 0, shipPosZ);

        transform.position = shipPos; //Set the ship's new position
    }

    //Func to help determine the player's speed
    private float DetermineSpeed()
    {
        //Determine offset from the player
        Vector3 offset = player.position - transform.position;

        //Calculate the distance between the ship and the player
        float distance = offset.sqrMagnitude;

        //Make sure rotationSpeed is allotted correctly
        if (rotationSpeed < 0)
        {
            rotationSpeed = -1;
        }
        else
        {
            rotationSpeed = 1;
        }

        /*Multiply the ship's distance by the rotationSpeed (Mostly for a negative positive thing)
        to get the return speed value
        */
        return (distance * 0.05f) * rotationSpeed;
    }
}
