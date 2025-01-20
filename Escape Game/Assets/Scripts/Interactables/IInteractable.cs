using UnityEngine;

public interface IInteractable
{
    void Interact();

    AudioClip GetPickUpSound();
}
