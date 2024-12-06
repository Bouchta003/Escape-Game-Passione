using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    private Rigidbody rb; // Composant Rigidbody pour appliquer la physique

    private Vector3 movement;

    void Start()
    {
        // Récupérer le Rigidbody attaché au joueur
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Récupérer les entrées de l'utilisateur
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Créer un vecteur de mouvement basé sur les entrées
        movement = new Vector3(moveHorizontal, 0f, moveVertical);
    }

    void FixedUpdate()
    {
        // Déplacer le joueur en utilisant le Rigidbody
        MovePlayer();
    }

    void MovePlayer()
    {
        // Appliquer le mouvement au Rigidbody pour déplacer le joueur
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
