using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Rigidbody2D playerRB;
	//Define starting variable values. All are subject to change
    public float speed = .5f;
    public float maxspeed = 1.0f;
    int allowjump = 1;
    public float jumpheight = 7.0f;
    public float gravity = -.12f;
    public float maxgravity = -.14f;
	//nolr is onll called when char is in cannon, disabling left right movement
    int nolr = 0;
	public AudioClip bounceSound;
	public AudioClip eatSound;
	public AudioClip boomSound;
    private Animator animator;
    void Start()
    {
        animator=  GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        playerRB.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
	{
        Vector3 position = transform.position;
        if (Input.GetButtonDown("Up"))
        {
			//Checks to see if you've already used your jump
            if (allowjump == 1)
            {
                Vector3 xv = playerRB.velocity;
				//Reset's player velocity, so they can't fling themselves upwards forever
                xv.y = 0.0f;
                playerRB.velocity = xv;
                Vector2 v2 = new Vector2(0.0f, jumpheight);
                playerRB.AddForce(v2, ForceMode2D.Impulse);
				//Turns jumping off
                allowjump = 0;
            }
        }
		//For left right movement the player accelerates up to a cap
        if (Input.GetButton("Left") && nolr == 0)
        {
            if (playerRB.velocity.x > -1 * maxspeed)
            {
                Vector2 v2 = new Vector2(-0.35f, 0.0f);
                playerRB.AddForce(v2, ForceMode2D.Impulse);
            }
            animator.SetTrigger("MoveLeft");
            position.x += -0.030f;
        }
		else if (Input.GetButton ("Right") == true && nolr == 0) 
		{
			if (playerRB.velocity.x < maxspeed) {
				Vector2 v2 = new Vector2 (0.35f, 0.0f);
				playerRB.AddForce (v2, ForceMode2D.Impulse);
			}
			position.x += 0.030f;
			animator.SetTrigger ("MoveRight");
		} else {
			animator.SetTrigger ("Idle Anim");
		}
        if (playerRB.velocity.x > 0)
        {
            Vector3 xv = playerRB.velocity;
            xv.x -= xv.x / 12;
            playerRB.velocity = xv;

        }
        if (playerRB.velocity.x < 0)
        {
            Vector3 xv = playerRB.velocity;

            xv.x += (-1 * xv.x) / 12;
            playerRB.velocity = xv;
        }

        Vector3 xvel = playerRB.velocity;
		//adds gravity
        addGravity();
		//updates position
        transform.position = position;
    }
    void OnCollisionStay2D(Collision2D col)
    {
        Vector3 xv = playerRB.velocity;
        xv.x = 0.0f;
        playerRB.velocity = xv;
        if (col.gameObject.tag == "Platform")
        {
			//Checks to make sure the player is on TOP of a platform to reset his jump, not on the side
            Collider2D collider = col.collider;
            Vector3 contactPoint = col.contacts[0].point;
            Vector3 center = collider.bounds.center;
            if (contactPoint.y > center.y)
            {
                allowjump = 1;
                if (playerRB.velocity.y < 0)
                {
                    gravity = -.05f;
                }

            }
        }

    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
			//Player can only jump once without collecting a pellet
            allowjump = 0;
            gravity = maxgravity;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
		//On contact with a jump pellet, allows for another jump
        if (col.tag == "Jump Pellet")
        {
			GetComponent<AudioSource> ().clip=eatSound;
			GetComponent<AudioSource>().Play();
            Destroy(col.gameObject);
            allowjump = 1;
        }
		//Trampolines force the player upward
        if (col.tag == "Trampoline")
        {
			GetComponent<AudioSource> ().clip=bounceSound;
			GetComponent<AudioSource>().Play();
			Debug.Log ("Trying to Play");
			NetScript netscript= col.GetComponent<NetScript>();
			netscript.jump = 1;
            Vector3 xv = playerRB.velocity;
            xv.y = 0.0f;
            playerRB.velocity = xv;
            Vector2 v2 = new Vector2(0.0f, 8.0f);
            playerRB.AddForce(v2, ForceMode2D.Impulse);
        }
		//Portal was the first name for the cannons, which fire the player at an angle
        if (col.tag == "Portal")
        {
           //Forces the player into the center of the portal on contact
            Vector3 ppos = playerRB.position;
            ppos.x = col.transform.position.x;
            ppos.y = col.transform.position.y;
            playerRB.position = ppos;
            Vector3 xv = playerRB.velocity;
            xv.y = 0.0f;
            xv.x = 0.0f;
            playerRB.velocity = xv;
			//Doesn't let the player move
            gravity = 0;
            nolr = 1;
        }
    }
    public void OnTriggerStay2D(Collider2D col)
    {


        if (col.tag == "Portal")
        {
          //Tilts the portal, adjusting the angle of launch
            if (Input.GetButton("Left"))
            {
                col.transform.Rotate(Vector3.forward * 5);
            }
            if (Input.GetButton("Right"))
            {
                col.transform.Rotate(Vector3.forward * -5);
            }
            if (Input.GetButtonDown("Up"))
            {
				GetComponent<AudioSource> ().clip=boomSound;
				GetComponent<AudioSource>().Play();
                Debug.Log("Trying to Fire");
				//To find the angle of launch, there is an invisible object at the tip of each cannon
				//the vector between that object and the cannon's tranform is the angle of launch
                PortalScript portalScript = col.GetComponent<PortalScript>();
                Debug.Log(portalScript.dirx);
                float firex = portalScript.dirx*10;
                    float firey = portalScript.diry*10;
                Vector2 fire =new Vector2 (firex, firey );
                playerRB.AddForce(fire, ForceMode2D.Impulse);
                nolr = 0;
                gravity = -.12f;


            }
        }
    }
    void addGravity()
    {
		//Adds gravity to the player, pushing them down a bit
        Vector2 v2 = new Vector2(0.0f, gravity);
        playerRB.AddForce(v2, ForceMode2D.Impulse);

    }
}
   
    

