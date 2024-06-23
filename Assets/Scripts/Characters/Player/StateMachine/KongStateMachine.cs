using UnityEngine;

public class KongStateMachine : BaseStateMachine<KongController> {
    #region Constructor

    public KongStateMachine(KongController controller, Animator animator) : base(controller, animator) {
        AddState(new KongIdleState(controller, animator));
        AddState(new KongWalkState(controller, animator));
        AddState(new KongRunState(controller, animator));
        AddState(new KongAirbornStateMachine(controller, animator));
        AddState(new KongAttackState(controller, animator));
        AddState(new KongAttackToStandState(controller, animator));
        AddState(new KongHookStateMachine(controller, animator));
        AddState(new KongCrouchStateMachine(controller, animator));
        AddState(new KongInsideBarrelState(controller, animator));
        AddState(new KongBlastState(controller, animator));

        RegisterStateTransitions();

        SetState(typeof(KongIdleState));
    }

    #endregion
}