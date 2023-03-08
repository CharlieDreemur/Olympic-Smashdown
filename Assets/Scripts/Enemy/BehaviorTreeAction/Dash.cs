using System.Collections;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Dash towards the target. Returns success always.")]
public class Dash : Action
{
    // The speed of the object
    public SharedFloat dashSpeed = 5;

    // The transform that the object is moving towards
    public SharedTransform target;
    public SharedFloat dashTime = 2;
    [SerializeField]
    private float leftTime = 0;
    [SerializeField]
    private Vector3 direction = Vector3.zero;
    public override void OnStart()
    {
        direction = (target.Value.position - transform.position).normalized;
        leftTime = dashTime.Value;
    }
    public override TaskStatus OnUpdate()
    {

        transform.position += direction * dashSpeed.Value * Time.deltaTime;
        leftTime -= Time.deltaTime;
        if (leftTime <= 0)
            return TaskStatus.Success;
        return TaskStatus.Running;

    }

}