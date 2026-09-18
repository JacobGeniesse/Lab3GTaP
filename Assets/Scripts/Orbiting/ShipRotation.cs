using System;
using UnityEngine;

public class ShipRotation : MonoBehaviour
{
    private Transform player; //Transform var for player position

    void Start()
    {
        //Try and find the player game object
        try
        {
            player = GameObject.Find("Player").GetComponent<Transform>();
        }
        catch
        {
            //If this fails throw an error
            throw new ArgumentException("Unable to find player game object!", nameof(ShipRotation));
        }

        RotateShip();
    }

    void Update()
    {
        RotateShip(); //rotate the ship
    }

    void RotateShip()
    {
        //Delegate a var for transform.forward for ease of use
        Vector3 forward = transform.forward;

        //Assign a value for the player's position
        Vector3 playerPos = player.position - transform.position;

        //Calculate the difference between the normalized relative forward position and the normalized player position
        float currentDif = (Vector3.Dot(forward.normalized, playerPos.normalized));

        //Transistion currentDif from Radians to Degrees for rotation use
        currentDif = currentDif * Mathf.Rad2Deg; 

        transform.Rotate(0, currentDif, 0); //Rotate by the currentDif amount to have the ship lock onto player

        //Debug.Log(currentDif); //For testing if the currentDifference is being logged correctly
    }
}
