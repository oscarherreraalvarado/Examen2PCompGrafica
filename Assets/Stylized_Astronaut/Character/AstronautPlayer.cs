using UnityEngine;
using UnityEngine.SceneManagement;

namespace AstronautPlayer
{
    public class AstronautPlayer : MonoBehaviour
    {
        private Animator anim;
        private CharacterController controller;
        private ControladorVida controladorVida;
        private Vector3 startPosition; // Variable para la posición inicial del jugador

        public float speed = 6.0f;
        public float turnSpeed = 400.0f;
        public float jumpSpeed = 20.0f;
        public float gravity = 20.0f;
        private Vector3 moveDirection = Vector3.zero; // Vector de movimiento

        private Transform platform = null;
        private int jumpCount = 0; // Contador de saltos
        private int maxJumps = 2; // Número máximo de saltos permitidos (doble salto)

        void Start()
        {
            controller = GetComponent<CharacterController>();
            anim = gameObject.GetComponentInChildren<Animator>();
            //vida
            controladorVida = GameObject.Find("Controlador Vida").GetComponent<ControladorVida>();

        
            // Guardar la posición inicial del jugador
            startPosition = transform.position;
        }

        void Update()
        {
            if (controller.isGrounded)
            {
                // Manejo del movimiento hacia adelante
                moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
                jumpCount = 0; // Restablecer el contador de saltos cuando está en el suelo

                // Control de la animación de caminar
                if (Input.GetKey("w"))
                {
                    anim.SetInteger("AnimationPar", 1);
                }
                else
                {
                    anim.SetInteger("AnimationPar", 0);
                }

                // Control del salto
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                    anim.SetTrigger("Jump");
                    jumpCount++;
                }
            }
            else
            {
                // Control de doble salto
                if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
                {
                    moveDirection.y = jumpSpeed;
                    anim.SetTrigger("Jump");
                    jumpCount++;
                }

                // Control de la animación en el aire
                anim.SetInteger("AnimationPar", 0);
            }

            // Control de la rotación del jugador
            float turn = Input.GetAxis("Horizontal");
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

            // Aplicar movimiento y gravedad
            controller.Move(moveDirection * Time.deltaTime);
            moveDirection.y -= gravity * Time.deltaTime;
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.CompareTag("Enemigo"))
            {
                controladorVida.vida--; // Reducir la vida del jugador
                if (controladorVida.vida == 0)
                {
                    SceneManager.LoadScene("MainMenu");
                }
                else
                {
                    transform.position = startPosition; // Reiniciar posición del jugador
                }
            }

            // Manejar colisiones con plataformas móviles
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