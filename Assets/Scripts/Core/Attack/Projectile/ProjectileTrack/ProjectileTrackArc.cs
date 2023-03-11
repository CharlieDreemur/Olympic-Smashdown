using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// /// <summary>
// /// For enemies mostly, the projectile will move to the player in a 3D parabola way.
// /// Don't finished yet, please do not use it.
// /// </summary>
public class ProjectileTrackArc : ProjectileTrack
{   
    public ProjectileTrackArc(Projectile projectile):base(projectile) {}

    public override void Move()
    {
        //! projectile.rb.detectCollisions = false;

        var finalPosition = projectile.transform.position;

        float step = Speed * Time.fixedDeltaTime * projectile.totalDistanceToTarget;
        finalPosition += finalPosition.Direction2D(projectile.targetPosition) * step;
        projectile.distance += step;
        var progress = projectile.distance / projectile.totalDistanceToTarget;
        /* //!
        var newHeight = projectile.args.Data.maxArcHeight * projectile.args.Data.heightCurve.Evaluate(progress);
        finalPosition = new Vector3(
            finalPosition.x, 
            projectile.originalHeight + newHeight, 
            finalPosition.z
        );
        */

        projectile.rb.MovePosition(finalPosition);
        /*
        if (projectile.distance >= projectile.totalDistanceToTarget)
        {
            projectile.Trigger();
        }
        */
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }
}
