using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileAction: ScriptableObject 
{
    public abstract void Activate(Projectile projectile);
    public float delayTime = 0.0f;
}
