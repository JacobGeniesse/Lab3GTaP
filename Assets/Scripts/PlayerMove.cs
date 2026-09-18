using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Tooltip("Border value for the horizontal limit the player can travel to, adjust this if you adjust the camera.")]
    [SerializeField] private float borderX = 0; //var for the camera border

    [SerializeField] private InputActionAsset masterList; //Input master list var

    [Tooltip("Adjust this float to control player ship speed.")]
    [SerializeField, Range(1f, 10f)] private float moveSpeed; //var for how fast the player can move

    private InputAction movement; //Input var

    void Start()
    {
        //Try to get movement input from the master list
        try
        {
            movement = masterList["Move"];
        }
        catch
        {
            //If this fails throw an error, and hint that the problem may be that the input action asset is not assigned
            throw new ArgumentException("Unable to assign movement Input! Check if the Input Action Asset is assigned!", nameof(PlayerMove));
        }
    }

    void Update()
    {
        //Calculate where the player wants to move
        Vector3 desiredMove = AssignDirection();
        
        //If the player is not beyond the borders of the screen, let them move
        if(transform.position.x <= borderX && transform.position.x >= -borderX)
        {
            transform.Translate(desiredMove * Time.deltaTime);
        }

        BoxInPlayer(); //Get the player back in bounds if they somehow get out
    }

    //Func to help box in the player
    private void BoxInPlayer()
    {
        if (transform.position.x > borderX)
        {
            transform.position = new Vector3(borderX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -borderX)
        {
            transform.position = new Vector3(-borderX, transform.position.y, transform.position.z);
        }
    }

    //Func to determine how the player will move
    private Vector3 AssignDirection()
    {
        Vector2 moveInput = movement.ReadValue<Vector2>(); //Grab movement values

        //Assign only the x value as per assignment description
        Vector3 moveDir = new Vector3(moveInput.x, 0, 0).normalized;

        return moveDir * moveSpeed; //Multiply by the move speed
    }
}
