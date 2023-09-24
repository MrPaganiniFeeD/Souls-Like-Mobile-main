using System.Collections.Generic;
using UnityEngine;

namespace Fabrics
{
    public class FabricWeapon : IFabricWeapon
    {
        [SerializeField] private List<WeaponInfo> _weapons;


    }

    public interface IFabricWeapon
    {
    }
}