using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float velocity;
    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(velocity, myRigidBody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = new Vector2(-velocity, myRigidBody.velocity.y);
        }
    }
}
