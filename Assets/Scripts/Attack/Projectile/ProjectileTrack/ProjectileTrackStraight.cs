using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrackStraight : ProjectileTrack
{
    public ProjectileTrackStraight(Projectile projectile):base(projectile){
    }

    public override void Move()
    {
        float step = Speed * Time.fixedDeltaTime;
        Vector3 displacement = projectile.args.direction * step;
        var targetPosition = projectile.transform.position;
        targetPosition += displacement;
        targetPosition = Vector3.Lerp(targetPosition, targetPosition + displacement, step);
        //projectile.rb.AddForce(displacement, ForceMode.Acceleration);
        projectile.distance += step;
        projectile.rb.MovePosition(targetPosition);
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }
}
