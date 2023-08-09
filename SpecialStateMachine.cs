using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 更确定的状态机
/// </summary>
public class SpecialStateMachine : BaseStateMachine
{
    //在这里添加需要的属性
    bool b;
    protected override void Start()
    {
        //在这里 添加额外的状态机
        m_States.AddFirst(new SkillAttackState(this));
        base.Start();
    }
}