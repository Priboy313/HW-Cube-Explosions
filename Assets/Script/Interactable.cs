using System;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable 
{
    public event Action InteractionEvent;

    public void Interaction()
    {
        InteractionEvent?.Invoke();
    }
}
