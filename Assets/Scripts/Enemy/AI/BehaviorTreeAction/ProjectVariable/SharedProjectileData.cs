using UnityEngine;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedProjectileData : SharedVariable<ProjectileData>
    {
        public static explicit operator SharedProjectileData (ProjectileData  value) { return new SharedProjectileData  { mValue = value }; }
    }
}