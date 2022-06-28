using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemScript : BaseInteractable
{
    public override void Interact()
    {
        player.PickUpPlumeCollectable(_interactableStats.Coin);
        Destroy();
    }
}
