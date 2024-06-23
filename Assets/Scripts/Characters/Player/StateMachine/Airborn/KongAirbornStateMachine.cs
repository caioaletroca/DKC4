using UnityEngine;

public class KongAirbornStateMachine : BaseStateMachine<KongController>
{
    #region Constructor

    public KongAirbornStateMachine(KongController controller, Animator animator) : base(controller, animator) {
        AddState(new KongAirbornRiseState(controller, animator));
        AddState(new KongAirbornAirState(controller, animator));
        AddState(new KongAirbornLandState(controller, animator));
        AddState(new KongAirbornSomersaultState(controller, animator));

        RegisterStateTransitions();
    }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetState(typeof(KongIdleState));
        var walk = stateMachine.GetState(typeof(KongWalkState));
        var run = stateMachine.GetState(typeof(KongRunState));
        var hook = stateMachine.GetState(typeof(KongHookStateMachine));
        var insideBarrel = stateMachine.GetState(typeof(KongInsideBarrelState));

        AddTransition(idle, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.Land, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.HorizontalValue < 0.001)
            }
        ));
        AddTransition(walk, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.Land, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.HorizontalValue > 0.001 && !controller.Run)
            }
        ));
        AddTransition(run, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.Land, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.HorizontalValue > 0.001 && controller.Run)
            }
        ));
        AddTransition(hook, new FunctionPredicate(() => controller.Hook));
        AddTransition(insideBarrel, new FunctionPredicate(() => controller.Barrel));

        SetCurrent(typeof(KongAirbornRiseState));
    }

    #endregion
}