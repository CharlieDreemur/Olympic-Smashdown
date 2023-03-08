using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileTrack
{   
    [HideInInspector]
    protected ProjectileData _data;
    protected Projectile projectile;
    protected Entity target;
    protected float speedMultipler;
    protected AnimationCurve speedCurve;
    protected Rigidbody2D rb
    {
        get=>projectile.RB;
    }
    protected float Speed
    {
        get
        {
            return speedMultipler * speedCurve.Evaluate(projectile.time);
        }
    }
    protected Vector3 Scale
    {
        get
        {
            return _data.scale * _data.scaleCurve.Evaluate(projectile.time);
        }
    }

    public ProjectileTrack(Projectile projectile)
    {
        this.projectile = projectile;
        this._data = projectile.args.Data;
        this.target = projectile.args.target;
        this.speedMultipler = _data.speedMultipler;
        this.speedCurve = _data.speedCurve;
    }

    public abstract void Move();

    public virtual void FixedUpdate()
    {
        projectile.transform.localScale = Scale;
    }

    
}
