using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Rigidbody2D playerRB;
    public float speed = .5f;
    public float maxspeed = 1.0f;
    int allowjump = 1;
    public float jumpheight = 7.0f;
    public float gravity = -.12f;
    public float maxgravity = -.14f;
    int nolr = 0;
    // Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        if (Input.GetButtonDown("Up"))
        {
            if (allowjump == 1)
            {
                Vector3 xv = playerRB.velocity;
                xv.y = 0.0f;
                playerRB.velocity = xv;
                Vector2 v2 = new Vector2(0.0f, jumpheight);

                playerRB.AddForce(v2, ForceMode2D.Impulse);
                allowjump = 0;

            }
        }
        if (Input.GetButton("Left") && nolr == 0)
        {
            if (playerRB.velocity.x > -1 * maxspeed)
            {
                Vector2 v2 = new Vector2(-0.35f, 0.0f);
                playerRB.AddForce(v2, ForceMode2D.Impulse);
            }
            position.x += -0.030f;
        }
        if (Input.GetButton("Right") == true && nolr == 0)
        {
            if (playerRB.velocity.x < maxspeed)
            {
                Vector2 v2 = new Vector2(0.35f, 0.0f);
                playerRB.AddForce(v2, ForceMode2D.Impulse);
            }
            position.x += 0.030f;
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
        // Debug.Log("Gravity: " + gravity);
        addGravity();
        transform.position = position;
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Jump Pellet")
        {
            Destroy(col.gameObject);
            allowjump = 1;
        }
        Vector3 xv = playerRB.velocity;
        xv.x = 0.0f;
        playerRB.velocity = xv;
        if (col.gameObject.tag == "Platform")
        {
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
            allowjump = 0;
            gravity = maxgravity;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Jump Pellet")
        {
            //    Debug.Log("JUMPER");
            Destroy(col.gameObject);
            allowjump = 1;
        }
        if (col.tag == "Trampoline")
        {

            //Debug.Log("TRMAPLINE");
            Vector3 xv = playerRB.velocity;
            xv.y = 0.0f;
            playerRB.velocity = xv;
            Vector2 v2 = new Vector2(0.0f, 8.0f);
            playerRB.AddForce(v2, ForceMode2D.Impulse);
        }
        if (col.tag == "Portal")
        {
           // Debug.Log("PORTAL");
            Vector3 ppos = playerRB.position;
            ppos.x = col.transform.position.x;
            ppos.y = col.transform.position.y;
            playerRB.position = ppos;
            Vector3 xv = playerRB.velocity;
            xv.y = 0.0f;
            xv.x = 0.0f;
            playerRB.velocity = xv;
            gravity = 0;
            nolr = 1;
            // OnTriggerStay(col);


        }
    }
    public void OnTriggerStay2D(Collider2D col)
    {


        if (col.tag == "Portal")
        {
          //  Debug.Log("IM HERE");
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
                Debug.Log("Trying to Fire");
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
        //  GameObject thing = col.child;

        //  Vector3 heading = thing.transform.position - playerRB.position;
    }
    void addGravity()
    {
        Vector2 v2 = new Vector2(0.0f, gravity);
        playerRB.AddForce(v2, ForceMode2D.Impulse);
        // pos.y-=0.01f ;
        // return pos;
    }
}
   
    

