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
    protected float moveSpeed;
    protected AnimationCurve speedCurve;
    protected Rigidbody2D rb
    {
        get=>projectile.RB;
    }
    public float Speed
    {
        get
        {
            return moveSpeed * speedCurve.Evaluate(projectile.time);
        }
        set
        {
            moveSpeed = value;
        }
    }
    protected Vector3 Scale
    {
        get
        {
            return _data.scale * _data.scaleCurve.Evaluate(projectile.time);
        }
    }
    protected float scaleMultiplier = 1f;

    public ProjectileTrack(Projectile projectile)
    {
        this.projectile = projectile;
        this._data = projectile.args.Data;
        this.target = projectile.args.target;
        this.moveSpeed = _data.speedMultipler;
        this.speedCurve = _data.speedCurve;
    }

    public abstract void Move();

    public virtual void FixedUpdate()
    {
        projectile.transform.localScale = Scale * scaleMultiplier;
    }

    public void SetProjectileScale(float scale)
    {
        scaleMultiplier = Mathf.Clamp(scale, 0, _data.maxScale);
    }
}
