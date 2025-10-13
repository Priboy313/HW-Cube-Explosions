using System;

public interface IInteractable
{
    public event Action InteractionEvent;
    public void Interaction();
}
