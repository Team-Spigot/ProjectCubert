using UnityEngine;
using System.Collections.Generic;

public class SpiderMove : MonoBehaviour
{
    public enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum InitalDirections
    {
        Left,
        Right
    }

    public Transform LinecastDown;
    public Transform LinecastLeft;
    public Transform LinecastRight;
    public Transform LinecastCornerRight;
    public Transform LinecastCornerLeft;
    public Transform LinecastSideRight1;
    public Transform LinecastSideRight2;
    public Transform LinecastSideLeft1;
    public Transform LinecastSideLeft2;

    public Transform Body;

    public float speed;

    public Directions Direction;


    public InitalDirections InitalDirection;

    private Vector2 velocity;

    private bool hitwalldown;
    private bool hitwallleft;
    private bool hitwallright;
    private bool hitwallcornerdownright;
    private bool hitwallcornerdownleft;
    private bool hitwallsideright;
    private bool hitwallsideleft;

    private Vector3 startingPosition;
    private Directions startingDirection;
    private Quaternion startingRotation;

    void Awake()
    {
        startingPosition = transform.position;
        startingDirection = Direction;
        startingRotation = transform.rotation;
    }

    void Start()
    {
        if (Direction == Directions.Down)
        {
            velocity = new Vector2(0, -1);
        }
        else if (Direction == Directions.Left)
        {
            velocity = new Vector2(-1, 0);
        }
        else if (Direction == Directions.Right)
        {
            velocity = new Vector2(1, 0);
        }
        else if (Direction == Directions.Up)
        {
            velocity = new Vector2(1, 0);
        }
    }

    void Update()
    {
        hitwalldown = Physics2D.Linecast(LinecastDown.position, transform.position, 1 << LayerMask.NameToLayer("Walls"));
        hitwallleft = Physics2D.Linecast(LinecastLeft.position, transform.position, 1 << LayerMask.NameToLayer("Walls"));
        hitwallright = Physics2D.Linecast(LinecastRight.position, transform.position, 1 << LayerMask.NameToLayer("Walls"));
        hitwallcornerdownright = Physics2D.Linecast(LinecastCornerRight.position, transform.position, 1 << LayerMask.NameToLayer("Walls"));
        hitwallcornerdownleft = Physics2D.Linecast(LinecastCornerLeft.position, transform.position, 1 << LayerMask.NameToLayer("Walls"));
        hitwallsideright = Physics2D.Linecast(LinecastSideRight1.position, LinecastSideRight2.position, 1 << LayerMask.NameToLayer("Walls"));
        hitwallsideleft = Physics2D.Linecast(LinecastSideLeft1.position, LinecastSideLeft2.position, 1 << LayerMask.NameToLayer("Walls"));

        if (InitalDirection == InitalDirections.Left)
        {
            if (hitwalldown && hitwallleft)
            {
                if (Direction == Directions.Down)
                {
                    velocity = new Vector2(-1, 0);
                    Direction = Directions.Left;
                    Body.RotateAround(transform.position, Vector3.forward, -90);
                    Debug.Log("Turn Down => Left (Inner Corner)");
                }
                else if (Direction == Directions.Left)
                {
                    velocity = new Vector2(0, 1);
                    Direction = Directions.Up;
                    Body.RotateAround(transform.position, Vector3.forward, -90);
                    Debug.Log("Turn Left => Up (Inner Corner)");
                }
                else if (Direction == Directions.Right)
                {
                    velocity = new Vector2(0, -1);
                    Direction = Directions.Down;
                    Body.RotateAround(transform.position, Vector3.forward, -90);
                    Debug.Log("Turn Right => Down (Inner Corner)");
                }
                else if (Direction == Directions.Up)
                {
                    velocity = new Vector2(1, 0);
                    Direction = Directions.Right;
                    Body.RotateAround(transform.position, Vector3.forward, -90);
                    Debug.Log("Turn Up => Right (Inner Corner)");
                }
            }
            else if (hitwallcornerdownright && !hitwallsideright && !hitwalldown && !hitwallleft)
            {
                if (Direction == Directions.Down)
                {
                    velocity = new Vector2(1, 0);
                    Direction = Directions.Right;
                    Body.RotateAround(transform.position, Vector3.forward, 90);
                    Debug.Log("Turn Down => Right (Outer Corner)");
                }
                else if (Direction == Directions.Left)
                {
                    velocity = new Vector2(0, -1);
                    Direction = Directions.Down;
                    Body.RotateAround(transform.position, Vector3.forward, 90);
                    Debug.Log("Turn Left => Down (Outer Corner)");
                }
                else if (Direction == Directions.Right)
                {
                    velocity = new Vector2(0, 1);
                    Direction = Directions.Up;
                    Body.RotateAround(transform.position, Vector3.forward, 90);
                    Debug.Log("Turn Right => Up (Outer Corner)");
                }
                else if (Direction == Directions.Up)
                {
                    velocity = new Vector2(-1, 0);
                    Direction = Directions.Left;
                    Body.RotateAround(transform.position, Vector3.forward, 90);
                    Debug.Log("Turn Up => Left (Outer Corner)");
                }
            }
        }
        else if (InitalDirection == InitalDirections.Right)
        {
            if (hitwalldown && hitwallright)
            {
                if (Direction == Directions.Down)
                {
                    velocity = new Vector2(1, 0);
                    Direction = Directions.Right;
                    Body.RotateAround(transform.position, Vector3.forward, 90);
                    Debug.Log("Turn Down => Right (Inner Corner)");
                }
                else if (Direction == Directions.Left)
                {
                    velocity = new Vector2(0, -1);
                    Direction = Directions.Down;
                    Body.RotateAround(transform.position, Vector3.forward, 90);
                    Debug.Log("Turn Left => Down (Inner Corner)");
                }
                else if (Direction == Directions.Right)
                {
                    velocity = new Vector2(0, 1);
                    Direction = Directions.Up;
                    Body.RotateAround(transform.position, Vector3.forward, 90);
                    Debug.Log("Turn Right => Up (Inner Corner)");
                }
                else if (Direction == Directions.Up)
                {
                    velocity = new Vector2(-1, 0);
                    Direction = Directions.Left;
                    Body.RotateAround(transform.position, Vector3.forward, 90);
                    Debug.Log("Turn Up => Left (Inner Corner)");
                }
            }
            else if (hitwallcornerdownleft && !hitwallsideleft && !hitwalldown && !hitwallright)
            {
                if (Direction == Directions.Down)
                {
                    velocity = new Vector2(-1, 0);
                    Direction = Directions.Left;
                    Body.RotateAround(transform.position, Vector3.forward, -90);
                    Debug.Log("Turn Down => Left (Outer Corner)");
                }
                else if (Direction == Directions.Left)
                {
                    velocity = new Vector2(0, 1);
                    Direction = Directions.Up;
                    Body.RotateAround(transform.position, Vector3.forward, -90);
                    Debug.Log("Turn Left => Up (Outer Corner)");
                }
                else if (Direction == Directions.Right)
                {
                    velocity = new Vector2(0, -1);
                    Direction = Directions.Down;
                    Body.RotateAround(transform.position, Vector3.forward, -90);
                    Debug.Log("Turn Right => Down (Outer Corner)");
                }
                else if (Direction == Directions.Up)
                {
                    velocity = new Vector2(1, 0);
                    Direction = Directions.Right;
                    Body.RotateAround(transform.position, Vector3.forward, -90);
                    Debug.Log("Turn Up => Right (Outer Corner)");
                }
            }
        }

        /*
        if (direction == Directions.Down)
        {
            if (hitwalldown && hitwallleft && !hitwallup && !hitwallright)
            {
                velocity = new Vector2(1, 0);
                direction = Directions.Right;
                Debug.Log("Down => Right (Inner Corner)");
                Body.RotateAround(transform.position, Vector3.forward, 90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, 90);
            }
            else if (hitwalldown && !hitwallleft && !hitwallup && hitwallright)
            {
                velocity = new Vector2(-1, 0);
                direction = Directions.Left;
                Debug.Log("Down => Left (Inner Corner)");
                Body.RotateAround(transform.position, Vector3.forward, -90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, -90);
            }
            else if (hitwallcornerupleft && !hitwalldown && !hitwallleft && !hitwallup && !hitwallright)
            {
                velocity = new Vector2(-1, 0);
                direction = Directions.Left;
                Debug.Log("Down => Left (Outer Corner)");
                Body.RotateAround(transform.position, Vector3.forward, -90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, -90);
            }
            else if (hitwallcornerupright && !hitwalldown && !hitwallleft && !hitwallup && !hitwallright)
            {
                velocity = new Vector2(1, 0);
                direction = Directions.Right;
                Debug.Log("Down => Right (Outer Corner)");
                Body.RotateAround(transform.position, Vector3.forward, 90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, 90);
            }
        }
        else if (direction == Directions.Left)
        {
            if (hitwalldown && hitwallleft && !hitwallup && !hitwallright)
            {
                velocity = new Vector2(0, 1);
                direction = Directions.Up;
                Debug.Log("Left => Up (Inner Corner)");
                Body.RotateAround(transform.position, Vector3.forward, -90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, -90);
            }
            else if (!hitwalldown && hitwallleft && hitwallup && !hitwallright)
            {
                velocity = new Vector2(0, -1);
                direction = Directions.Down;
                Debug.Log("Left => Down (Inner Corner)");
                Body.RotateAround(transform.position, Vector3.forward, 90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, 90);
            }
            else if (hitwallcornerupright && !hitwalldown && !hitwallleft && !hitwallup && !hitwallright)
            {
                velocity = new Vector2(0, 1);
                direction = Directions.Up;
                Debug.Log("Left => Up (Outer Corner)");
                Body.RotateAround(transform.position, Vector3.forward, -90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, -90);
            }
            else if (hitwallcornerdownright && !hitwalldown && !hitwallleft && !hitwallup && !hitwallright)
            {
                velocity = new Vector2(0, -1);
                direction = Directions.Down;
                Debug.Log("Left => Down (Outer Corner)");
                Body.RotateAround(transform.position, Vector3.forward, 90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, 90);
            }
        }
        else if (direction == Directions.Right)
        {
            if (hitwalldown && !hitwallleft && !hitwallup && hitwallright)
            {
                velocity = new Vector2(0, 1);
                direction = Directions.Up;
                Debug.Log("Right => Up (Inner Corner)");
                Body.RotateAround(transform.position, Vector3.forward, -90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, -90);
            }
            else if (!hitwalldown && !hitwallleft && hitwallup && hitwallright)
            {
                velocity = new Vector2(0, -1);
                direction = Directions.Down;
                Debug.Log("Right => Down (Inner Corner)");
                Body.RotateAround(transform.position, Vector3.forward, -90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, -90);
            }
            else if (hitwallcornerupleft && !hitwalldown && !hitwallleft && !hitwallup && !hitwallright)
            {
                velocity = new Vector2(0, 1);
                direction = Directions.Up;
                Debug.Log("Right => Up (Outer Corner)");
                Body.RotateAround(transform.position, Vector3.forward, 90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, 90);
            }
            else if (hitwallcornerdownleft && !hitwalldown && !hitwallleft && !hitwallup && !hitwallright)
            {
                velocity = new Vector2(0, -1);
                direction = Directions.Down;
                Debug.Log("Right => Down (Outer Corner)");
                Body.RotateAround(transform.position, Vector3.forward, 90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, 90);
            }
        }
        else if (direction == Directions.Up)
        {
            if (!hitwalldown && hitwallleft && hitwallup && !hitwallright)
            {
                velocity = new Vector2(1, 0);
                direction = Directions.Right;
                Debug.Log("Up => Right (Inner Corner)");
                Body.RotateAround(transform.position, Vector3.forward, -90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, -90);
            }
            else if (!hitwalldown && !hitwallleft && hitwallup && hitwallright)
            {
                velocity = new Vector2(-1, 0);
                direction = Directions.Left;
                Debug.Log("Up => Left (Inner Corner)");
                Body.RotateAround(transform.position, Vector3.forward, 90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, 90);
            }
            else if (hitwallcornerdownleft && !hitwalldown && !hitwallleft && !hitwallup && !hitwallright)
            {
                velocity = new Vector2(-1, 0);
                direction = Directions.Left;
                Debug.Log("Up => Left (Outer Corner)");
                Body.RotateAround(transform.position, Vector3.forward, 90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, 90);
            }
            else if (hitwallcornerdownright && !hitwalldown && !hitwallleft && !hitwallup && !hitwallright)
            {
                velocity = new Vector2(1, 0);
                direction = Directions.Right;
                Debug.Log("Up => Right (Outer Corner)");
                Body.RotateAround(transform.position, Vector3.forward, -90);
                LinecastOrigin.RotateAround(transform.position, Vector3.forward, -90);
            }
        }
        */
        transform.position += (Vector3)velocity * speed * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)velocity);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(LinecastDown.position, transform.position);
        Gizmos.DrawLine(LinecastLeft.position, transform.position);
        Gizmos.DrawLine(LinecastRight.position, transform.position);
        Gizmos.DrawLine(LinecastCornerRight.position, transform.position);
        Gizmos.DrawLine(LinecastCornerLeft.position, transform.position);
        Gizmos.DrawLine(LinecastSideRight1.position, LinecastSideRight2.position);
        Gizmos.DrawLine(LinecastSideLeft1.position, LinecastSideLeft2.position);
    }

    public void Reset()
    {
        transform.position = startingPosition;
        Direction = startingDirection;
        transform.rotation = startingRotation;
        Start();
    }
}
