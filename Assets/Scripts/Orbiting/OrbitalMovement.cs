using System;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour
{
    /*Coordinates
        x = center(x) + radius * cosine(angle)
        y = center(y) + radius * sine(angle)


        x = playerx + distance from player * cosine (angle around player)
        y = playery + distance from player * sine (angle around player)
    */

    private Transform player;

    [Tooltip("Determines how far away the enemy is from the player")]
    [SerializeField] private float radius = 1f;

    [Tooltip("Enter a positive number to go ___ enter a negative number to go ___")]
    [SerializeField] private float RotationSpeed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            player = GameObject.Find("Player").GetComponent<Transform>();
        }
        catch
        {
            throw new ArgumentException("Unable to find player game object!", nameof(OrbitalMovement));
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        //float shipAngle = (Vector3.SignedAngle(transform.position, player.right));

        //Debug.Log(shipAngle);
        //float shipPosX = player.position.x + radius * Mathf.Cos(shipAngle);
        //float shipPosY = player.position.y + radius * Mathf.Sin(shipAngle);

        //Vector3 shipPos = new Vector3(shipPosX, 0, shipPosY);

        //float timing = RotationSpeed * Time.deltaTime;

        //transform.position = Vector3.MoveTowards(transform.position, shipPos, timing);
    }
}
