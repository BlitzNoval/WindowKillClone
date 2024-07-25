using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class CactiSecondary : SecondarySuperclass
{
    protected override void DoSecondaryEffect()
    {
        //base.DoSecondaryEffect();
        Debug.Log("Cacti version");
        //onhit
        switch (parentBehaviour.CurrentTier)
        {
            case WeaponTier.Common:
                break;
            case WeaponTier.Uncommon:
                break;
            case WeaponTier.Rare:
                break;
            case WeaponTier.Legendary:
                break;
        }
    }
}
