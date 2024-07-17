using UnityEngine;
using System.Collections;

namespace AstronautPlayer
{
	public class AstronautPlayer : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;

		public float speed = 6.0f; // Ajusté la velocidad para ser más realista
		public float turnSpeed = 400.0f;
		public float jumpSpeed = 20.0f; // Nueva variable para la velocidad del salto
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;

		void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
		}

		void Update (){
			if (controller.isGrounded){
				moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;

				if (Input.GetKey("w")) {
					anim.SetInteger("AnimationPar", 1);
				} else {
					anim.SetInteger("AnimationPar", 0);
				}

				// Si el jugador presiona el botón de salto (barra espaciadora)
				if (Input.GetButton("Jump")) {
					moveDirection.y = jumpSpeed;
					anim.SetTrigger("Jump"); // Activar la animación de salto
				}
			} else {
				// Si está en el aire, desactivar la animación de caminar
				anim.SetInteger("AnimationPar", 0);
			}

			float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

			// Aplicar movimiento y gravedad
			controller.Move(moveDirection * Time.deltaTime);
			moveDirection.y -= gravity * Time.deltaTime;
		}
	}
}