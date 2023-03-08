using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrackHoming : ProjectileTrack
{
    public ProjectileTrackHoming(Projectile projectile):base(projectile)
    {}

    public override void Move()
    {
        float step = Speed * Time.fixedDeltaTime;
        projectile.transform.position = Vector3.Lerp(projectile.transform.position, target.transform.position, step);
        projectile.distance += step;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }
}
