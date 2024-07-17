using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  salto : MonoBehaviour
{
  private float fallVelocity;          // Velocidad de caída
    public float jumpForce;              // Fuerza de salto

    public CharacterController Player;   // Componente CharacterController del jugador
    public float gravity = 9.8f;         // Fuerza de gravedad

    // Start se llama antes de la primera actualización del frame
    void Start()
    {
        Player = GetComponent<CharacterController>();  // Inicializa el CharacterController del jugador
    }

    // Update se llama una vez por frame
    void Update()
    {
        SetGravity();  // Ajusta la gravedad del jugador
        PlayersKills();  // Controla el salto del jugador
        Player.Move(new Vector3(0, fallVelocity, 0) * Time.deltaTime);  // Mueve al jugador en el eje Y
    }

    void SetGravity()
    {
        if (Player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;  // Si el jugador está en el suelo, ajusta la velocidad de caída
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;  // Si el jugador está en el aire, aumenta la velocidad de caída
        }
    }

    public void PlayersKills()
    {
        if (Player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;  // Si el jugador está en el suelo y presiona el botón de salto, aplica la fuerza de salto
        }
    }
}
