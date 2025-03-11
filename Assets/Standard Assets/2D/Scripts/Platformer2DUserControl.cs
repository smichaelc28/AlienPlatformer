using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

        private Rigidbody2D player;

        public float speed = .3f;
        public bool isIce;

        private Vector2 newForce;
        private Vector2 direction;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        private void Start()
        {
            player = GetComponent<Rigidbody2D>();
            newForce = Vector2.zero;
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.

            if (isIce)
            {
                direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;

                if (direction.x == 0)
                {
                    newForce = player.velocity;
                    player.AddForce(newForce);
                    GetComponent<Animator>().SetFloat("Speed", 0);
                }
                else
                {
                    player.AddForce(direction);
                    m_Character.Move(direction.x, crouch, m_Jump);
                }
            }

            else if (!isIce) 
            {
                m_Character.Move(h, crouch, m_Jump);
                newForce = Vector2.zero;
            }

            
            m_Jump = false;

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Ice")
            {
                isIce = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == "Ice")
            {
                isIce = false;
            }
        }
    }
}
