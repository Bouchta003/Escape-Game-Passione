using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de d�placement
    private Rigidbody rb; // Composant Rigidbody pour appliquer la physique

    private Vector3 movement;

    void Start()
    {
        // R�cup�rer le Rigidbody attach� au joueur
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // R�cup�rer les entr�es de l'utilisateur
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Cr�er un vecteur de mouvement bas� sur les entr�es
        movement = new Vector3(moveHorizontal, 0f, moveVertical);
    }

    void FixedUpdate()
    {
        // D�placer le joueur en utilisant le Rigidbody
        MovePlayer();
    }

    void MovePlayer()
    {
        // Appliquer le mouvement au Rigidbody pour d�placer le joueur
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
