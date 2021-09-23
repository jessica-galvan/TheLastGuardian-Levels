using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHeal : BaseInteractable, IPooleable
{
    [SerializeField] private PooleableType type;
    public PooleableType Type => type;

    public override void Interact()
    {
        if (player.LifeController.CanHeal())
        {
            player.LifeController.Heal(_interactableStats.Heal);
            AudioManager.instance.PlayPlayerSound(PlayerSoundClips.ReloadMana);
            Destroy();
        }
    }

    protected override void Destroy()
    {
        PoolManager.instance.Store(this);
    }
}
