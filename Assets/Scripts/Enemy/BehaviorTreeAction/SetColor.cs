using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[TaskDescription("Set Color")]
public class SetColor : Action
{
    public SharedTransform self;
    public Color color;

    public override TaskStatus OnUpdate()
    {
        self.Value.GetComponent<SpriteRenderer>().color = color;
        return TaskStatus.Success;
    }
}