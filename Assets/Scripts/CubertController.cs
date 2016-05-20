using UnityEngine;

public class CubertController : MonoBehaviour
{

    public GameObject cubert;

    //Allows player to jump
    public float JumpForce = 20;
    public float jumpDuration = .1f;
    float jmpDuration;
    float jmpForce;
    bool JumpKey;
    Rigidbody2D rig2d;
    //Checks if player is on the ground
    public bool onGround;
    
    public float ups;
    public float sides;

    float angle;
    float angleInDegrees;
    Vector2 distPos;
    [HideInInspector]
    public float distMag;

    bool oilyWall;
    bool normalWall;
    bool stickyWall;

    HingeJoint2D tempHinge;

    bool jump;

    void Start()
    {
        jmpForce = JumpForce;
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && onGround)
        {
            jump = true;
        }
        //Debug.Log("JumpKey: " + JumpKey + " OnGround: " + onGround);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        JumpKey = false;

        //Debug.Log("distMag: " + distMag + ", angle: " + angle + ", angleInDegrees: " + angleInDegrees + ", ups: " + ups + ", sides: " + sides);

        var MousePosition = Input.mousePosition;
        var PlayerPosition = Camera.main.WorldToScreenPoint(transform.position);

        distPos = MousePosition - PlayerPosition;
        distMag = Vector3.Magnitude(distPos);

        angle = Mathf.Atan2(distPos.y, distPos.x);

        if (!onGround)
        {
            stickyWall = false;
        }

         #region ups
        //Creates a float based on the displacement between the player and the mouses y positions. Used for launching the player upwards
        //ups = ((MousePosition.y / 8) - PlayerPosition.y / 8);

        ups = (Mathf.Sin(angle) * distMag) / 11.7f;

        if (ups > 15f)
        {
            ups = 15f;
        }

        if (ups < 1 && ups > 0)
        {
            ups = 1f;
        }
        if (ups < 0)
        {
            ups = -1f;
        }

        #endregion

        #region sides
        //similar to the ups float. Find difference in x between the players and mouses x position.
        /*if (MousePosition.x < PlayerPosition.x)
        {
            sides = (Mathf.Abs((MousePosition.x / 15) - PlayerPosition.x / 15));
        }
        else if (MousePosition.x > PlayerPosition.x)
        {
            sides = ((MousePosition.x / 15) - PlayerPosition.x / 15);
        }*/

        sides = (Mathf.Cos(angle) * distMag) / 14.625f;

        if (sides > 12)
        {
            sides = 12;
        }
        if (sides < -12)
        {
            sides = -12;
        }

        if (sides < 1 && sides > 0)
        {
            sides = 0;
        }
        if (sides > -1 && sides < 0)
        {
            sides = 0;
        }
        #endregion

        #region flip
        //Checks the mouses position versus the player, so we can use flip to decide which direction we launch the player
        /*if (MousePosition.x < PlayerPosition.x)
        {
            flip = -1;
        }
        else if (MousePosition.x > PlayerPosition.x)
        {
            flip = 1;
        }*/
        #endregion

        #region jump
        //Launches the player based on a left mouse click.
        if (jump && onGround)
        {
            stickyWall = false;
            Debug.Log("playx: " + PlayerPosition.x + " playy: " + PlayerPosition.y + " mousex: " + Input.mousePosition.x + " mousey: " + Input.mousePosition.y + " ups: " + ups + " sides: " + sides + " OnGround: " + onGround + " angle: " + angle);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray))
            {
                if (!JumpKey)
                {
                    jmpDuration += Time.deltaTime;
                    jmpForce += Time.deltaTime;

                    if (jmpDuration < jumpDuration)
                    {
                        cubert.GetComponent<Rigidbody2D>().velocity = new Vector2(sides, ups); //Uses sides and ups to launch player
                        onGround = false;
                        JumpKey = true;
                        jump = false;
                    }
                    else
                    {
                        JumpKey = true;
                        // Debug.Log("Shit...");
                    }
                }
                //Instantiate(cubert, transform.position, transform.rotation);
            }
        }
        #endregion
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "OilyWall")
        {
            onGround = true;
            JumpKey = false;
            jmpDuration = 0;
            jmpForce = JumpForce;

            stickyWall = false;

            //Debug.Log("OnGround");
        }
        if (col.collider.tag == "NormalWall")
        {
            onGround = true;
            JumpKey = false;
            jmpDuration = 0;
            jmpForce = JumpForce;

            stickyWall = false;

            //Debug.Log("OnGround");
        }
        if (col.collider.tag == "Untagged")
        {
            onGround = true;
            JumpKey = false;
            jmpDuration = 0;
            jmpForce = JumpForce;

            stickyWall = false;

            //Debug.Log("OnGround");
        }
        if (col.collider.tag == "FlimsyWall")
        {
            onGround = true;
            JumpKey = false;
            jmpDuration = 0;
            jmpForce = JumpForce;


            /* foreach (ContactPoint2D c in col.contacts)
             {
                 tempHinge = col.gameObject.AddComponent<HingeJoint2D>();
                 tempHinge.connectedBody = gameObject.GetComponent<Rigidbody2D>();
             }*/

            //Debug.Log("OnGround");
        }
        if (col.collider.tag == "BounceWall")
        {
            onGround = true;
            JumpKey = false;
            jmpDuration = 0;
            jmpForce = JumpForce;

            stickyWall = false;

            //Debug.Log("OnGround");
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {

        if (col.collider.tag == "FlimsyWall")
        {
            onGround = true;
            JumpKey = false;
            jmpDuration = 0;
            jmpForce = JumpForce;
            

            //Debug.Log("OnGround");
        }

        if (col.collider.tag == "OilyWall")
        {
            onGround = true;
            JumpKey = false;
            jmpDuration = 0;
            jmpForce = JumpForce;

            stickyWall = false;

            //Debug.Log("OnGround");
        }

        if (col.collider.tag == "NormalWall")
        {
            onGround = true;
            JumpKey = false;
            jmpDuration = 0;
            jmpForce = JumpForce;

            stickyWall = false;

            //Debug.Log("OnGround");
        }
        if (col.collider.tag == "BounceWall")
        {
            onGround = true;
            JumpKey = false;
            jmpDuration = 0;
            jmpForce = JumpForce;

            stickyWall = false;

            //Debug.Log("OnGround");
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {

        if (col.collider.tag == "FlimsyWall")
        {
            onGround = false;
            JumpKey = true;
            jmpDuration = 0;
            jmpForce = JumpForce;

            stickyWall = false;


            //Debug.Log("OnGround");
        }
        if (col.collider.tag == "OilyWall")
        {
            onGround = false;
            JumpKey = true;
            jmpDuration = 0;
            jmpForce = JumpForce;

            stickyWall = false;

            //Debug.Log("OnGround");
        }
        if (col.collider.tag == "NormalWall")
        {
            onGround = false;
            JumpKey = true;
            jmpDuration = 0;
            jmpForce = JumpForce;

            stickyWall = false;

            //Debug.Log("OnGround");
        }
        if (col.collider.tag == "BounceWall")
        {
            onGround = true;
            JumpKey = false;
            jmpDuration = 0;
            jmpForce = JumpForce;

            stickyWall = false;

            //Debug.Log("OnGround");
        }
    }


}
