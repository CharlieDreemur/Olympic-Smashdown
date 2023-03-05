using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCreatorTest : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    
    public CreateProjectileArgs args;
    [System.Serializable]
    public struct CreateProjectileArgs
    {
        public ProjectileData projectileData;
        [Range(-360.0f, 360.0f)]
        [Tooltip("Degree Angle, rorate on Y-Axix")]
        public float angle;

    }
        
    void Awake()
    {
        CreateProjectile(ballPrefab.GetComponent<Projectile>());
    }

    public void CreateProjectile(Projectile projectile)
    {
        UnityEngine.Assertions.Assert.IsNotNull(args.projectileData, "Warning: projectileData cannot be null");
        Vector3 direction = Quaternion.AngleAxis(args.angle, Vector3.forward) * projectile.gameObject.transform.right;
        List<Entity> ignoredCollision = projectile.triggerEntities;
        Projectile.InstantiateProjectile(
            args.projectileData,
            projectile.transform.position,
            null,  //!
            direction,
            projectile.args.target,
            ignoredCollision);
    }

}
