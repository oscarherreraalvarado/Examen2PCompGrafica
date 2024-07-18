using UnityEngine;

namespace AstronautPlayer
{
    public class AstronautPlayer : MonoBehaviour
    {
        private Animator anim;
        private CharacterController controller;

        public float speed = 6.0f;
        public float turnSpeed = 400.0f;
        public float jumpSpeed = 8.0f;
        private Vector3 moveDirection = Vector3.zero;
        public float gravity = 20.0f;

        private Transform platform = null;
        private int jumpCount = 0; // Contador de saltos
        private int maxJumps = 2; // Número máximo de saltos permitidos (doble salto)

        void Start()
        {
            controller = GetComponent<CharacterController>();
            anim = gameObject.GetComponentInChildren<Animator>();
        }

        void Update()
        {
            if (controller.isGrounded)
            {
                moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
                jumpCount = 0; // Restablecer el contador de saltos cuando está en el suelo

                if (Input.GetKey("w"))
                {
                    anim.SetInteger("AnimationPar", 1);
                }
                else
                {
                    anim.SetInteger("AnimationPar", 0);
                }

                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                    anim.SetTrigger("Jump");
                    jumpCount++;
                }
            }
            else
            {
                if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
                {
                    moveDirection.y = jumpSpeed;
                    anim.SetTrigger("Jump");
                    jumpCount++;
                }

                anim.SetInteger("AnimationPar", 0);
            }

            float turn = Input.GetAxis("Horizontal");
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

            // Aplicar movimiento y gravedad
            controller.Move(moveDirection * Time.deltaTime);
            moveDirection.y -= gravity * Time.deltaTime;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.CompareTag("MovingPlatform"))
            {
                platform = hit.gameObject.transform;
                transform.SetParent(platform);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                transform.SetParent(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                transform.SetParent(null);
            }
        }
    }
}