using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpecialCollectable : BaseInteractable
{
    public override void Interact()
    {
        player.PickUpRockCollectable(_interactableStats.Coin);
        Destroy();
    }
}
