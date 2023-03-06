using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Dash towards the target. Returns success always.")]
public class Dash: Action
{
    // The speed of the object
    public SharedFloat dashSpeed = 5;

    // The transform that the object is moving towards
    public SharedTransform target;
    [SerializeField]
    private float leftTime = 0;
    public override TaskStatus OnUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.Value.position, dashSpeed.Value * Time.deltaTime);
        return TaskStatus.Success;

    }
}