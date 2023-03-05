using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCreatorTest : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    
    void Awake()
    {
        // CreateProjectile(ballPrefab.GetComponent<Projectile>());
    }

    /*
    public void CreateProjectile(Projectile projectile)
    {
        //! UnityEngine.Assertions.Assert.IsNotNull(args.projectileData, "Warning: projectileData cannot be null");
        //! Vector3 direction = Quaternion.AngleAxis(args.angle, Vector3.up) * projectile.gameObject.transform.forward;
        Vector3 direction = new Vector3(1f, 1f, 0f).normalized;
        List<Entity> ignoredCollision = projectile.triggerEntities;
        Projectile.InstantiateProjectile(
            args.projectileData,
            projectile.transform.position,
            projectile.args.DamageInfo.Instigator,
            direction,
            projectile.args.target,
            ignoredCollision);
    }
    */

}
