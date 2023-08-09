using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 基础虚拟机 提供最通用、最基本的一些
/// </summary>
public class BaseStateMachine : MonoBehaviour
{
    /// <summary>
    /// 写一些通用的需要的属性
    /// </summary>
    [SerializeField]
    public Animator m_Animator;
    [SerializeField]
    public Transform m_PlayerTransform;
    [SerializeField]
    public Transform m_MonsterTransform;

    /// <summary>
    /// 当前状态机
    /// </summary>
    BaseState m_CurrentState;
    /// <summary>
    /// IdleState，所有State的出发点
    /// </summary>
    BaseState m_DefaultState;
    /// <summary>
    /// 所包含的状态机
    /// </summary>
    protected LinkedList<BaseState> m_States = new LinkedList<BaseState>();

    public float Distance => Vector3.SqrMagnitude(m_PlayerTransform.position - m_MonsterTransform.position);

    public void PlayAnimation(string name)
    {
        m_Animator.Play(name, 0, 0);
    }
    public bool IsAnimationOver(string name)
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(name) && m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            return true;
        }
        return false;
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //初始化状态机
        m_DefaultState = new IdleState(this);
        m_States.AddLast(new AttackState(this));
        m_States.AddLast(new RunState(this));

        m_CurrentState = m_DefaultState;
        m_CurrentState.OnEnter();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        m_CurrentState.Update();
        if (m_CurrentState == m_DefaultState)
        {
            foreach (var v in m_States)
                if (v.CanEnter())
                {
                    m_CurrentState.OnExit();
                    m_CurrentState = v;
                    m_CurrentState.OnEnter();
                    return;
                }
        }
        else
        {
            if (m_CurrentState.CanExit())
            {
                m_CurrentState.OnExit();
                m_CurrentState = m_DefaultState;
                m_CurrentState.OnEnter();
            }
        }
    }
}
public class IdleState : BaseState
{
    public IdleState(BaseStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("IdleState::OnEnter");    
        m_StateMachine.PlayAnimation("k_jingjie_001");
    }
    public override bool CanEnter()
    {
        return true;
    }
    public override bool CanExit()
    {
        return true;
    }
}
public class SkillAttackState : BaseState
{
    public SkillAttackState(BaseStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("SkillAttackState::OnEnter");
        m_StateMachine.PlayAnimation("k_putonggongji_001");
    }
    public override bool CanEnter()
    {
        return m_StateMachine.Distance < 6;
    }
    public override bool CanExit()
    {
        if (m_StateMachine.IsAnimationOver("k_putonggongji_001"))
            return true;
        else
            return false;
    }
}
public class AttackState : BaseState {
    public AttackState(BaseStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("AttackState::OnEnter");
        m_StateMachine.PlayAnimation("k_putonggongji_001");
    }
    public override bool CanEnter()
    {
        return m_StateMachine.Distance < 6;
    }
    public override bool CanExit()
    {
        if (m_StateMachine.IsAnimationOver("k_putonggongji_001"))
            return true;
        else
            return false;
    }
}
public class RunState : BaseState
{
    public RunState(BaseStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("RunState::OnEnter");
        m_StateMachine.PlayAnimation("k_jingjiepao_001");
    }
    public override void Update()
    {
        m_StateMachine.m_MonsterTransform.position += Vector3.Normalize(m_StateMachine.m_PlayerTransform.position - m_StateMachine.m_MonsterTransform.position) * Time.deltaTime * 10;
    }
    public override bool CanEnter()
    {
        return m_StateMachine.Distance < 400;
    }
    public override bool CanExit()
    {
        return m_StateMachine.Distance < 6;
    }
}
public abstract class BaseState {
    protected BaseStateMachine m_StateMachine;
    public BaseState(BaseStateMachine stateMachine)
    {
        m_StateMachine= stateMachine;
    }
    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void Update() { }
    public abstract bool CanEnter();
    public abstract bool CanExit();
}

