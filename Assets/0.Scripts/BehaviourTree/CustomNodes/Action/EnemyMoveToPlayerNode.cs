using System;
using System.Collections.Generic;
using UnityEngine;
using Seyang.BehaviourTree;

/// <summary>
/// 这个节点必须在确保范围内有玩家阵营存在的条件下执行
/// 在最开始时确定一个方向移动
/// 碰到玩家时，继续移动一段之间后退出
/// 这个节点的本质是为了接近玩家
/// </summary>
public class EnemyMoveToPlayerNode : ActionNode
{
    public override Type relevantType => typeof(EnemyMoveToPlayerRuntimeNode);
    public override RuntimeNodeBase InstantiateRuntimeNode()
    {
        return new EnemyMoveToPlayerRuntimeNode();
    }
}

public class EnemyMoveToPlayerRuntimeNode : EnemyRuntimeActionNodeBase
{
    private Vector2 direction;

    private const float ContinueMoveTime = 0.5f;
    private float counter;
    private bool isTouched;
    public override void OnStart()
    {
        base.OnStart();
        Controller.PlayAnimation(AnimationType.Move);
        direction = (Info.TargetPos - Info.CenterPos).normalized;
        direction = AdjustMoveDirection(direction);

        counter = ContinueMoveTime;
        isTouched = false;
        
        Controller.SetVelocity(direction * Info.moveSpeed);
    }

    protected override void OnFixedUpdate()
    {
    }

    protected override void OnCollisionEnter(Collision2D collision)
    {
        direction = AdjustMoveDirection(direction);
        Controller.SetVelocity(direction * Info.moveSpeed);
    }

    protected override void OnTriggerEnter(Collider2D collider)
    {
        isTouched = true;
    }

    public override NodeState OnUpdate()
    {
        if (isTouched)
        {
            counter += Time.deltaTime;
            if (counter >= ContinueMoveTime)
                return NodeState.Success;
        }
        return NodeState.Running;
    }
}

