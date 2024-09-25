using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsionPad : MonoBehaviour
{ 
    [SerializeField]
    private string playerTag;//set to Player in inspector
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private float force;
    [SerializeField]
    private float maxVelocity = 20f;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //float newDir = Mathf.Abs(collision.rigidbody.velocity.yf);
        Debug.Log(collision.relativeVelocity);
        collision.rigidbody.velocity =  new Vector2(0, -collision.relativeVelocity.y);
    }
}
