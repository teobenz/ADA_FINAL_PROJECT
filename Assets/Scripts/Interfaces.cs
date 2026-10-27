using UnityEngine;

public interface iTimeTrack
{
    void Reverse();
    void Forward();
    void Manipulate();
    void Resume();
}

public interface iInteractable
{
    bool manipulating { get; set; }
    bool activated { get; set; }
    void Interact();
}
