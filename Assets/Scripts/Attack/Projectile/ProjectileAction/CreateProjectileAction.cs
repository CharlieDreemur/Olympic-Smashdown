using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "GameBuilders/ScriptableObject/ProjectileSystem/ProjectileAction/CreateProjectile", order = 0)]
public class CreateProjectileAction : ProjectileAction
{
    public override void Activate(Projectile projectile)
    {
        CreateProjectile(projectile);
    }

    public CreateProjectileArgs args;
    [System.Serializable]
    public struct CreateProjectileArgs
    {
        public ProjectileData projectileData;
        [Range(-360.0f, 360.0f)]
        [Tooltip("Degree Angle, rorate on Y-Axix")]
        public float angle;

    }

    /// <summary>
    /// Create a new projectile based on data
    /// </summary>
    /// <param name="projectileData"></param>
    public void CreateProjectile(Projectile projectile)
    {
        UnityEngine.Assertions.Assert.IsNotNull(args.projectileData, "Warning: projectileData cannot be null");
        Vector3 direction = Quaternion.AngleAxis(args.angle, Vector3.up) * projectile.gameObject.transform.forward;
        List<Entity> ignoredCollision = projectile.triggerEntities;
        Projectile.InstantiateProjectile(
            args.projectileData,
            projectile.transform.position,
            projectile.args.DamageInfo.Instigator,
            direction,
            projectile.args.target,
            ignoredCollision);
    }

}

