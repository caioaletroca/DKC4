using UnityEngine;

public partial class KongController {
    /// <summary>
    /// Run time definitions for animation names
    /// </summary>
    public static class Animations {
        public static int Invisible = Animator.StringToHash("diddy_invisible");

        public static int Idle = Animator.StringToHash("diddy_idle");

        public static int Walk = Animator.StringToHash("diddy_walk");

        public static int Run = Animator.StringToHash("diddy_run");

        public static int Rise = Animator.StringToHash("diddy_rise");

        public static int Air = Animator.StringToHash("diddy_air");

        public static int Land = Animator.StringToHash("diddy_land");

        public static int Attack = Animator.StringToHash("diddy_attack");

        public static int AttackToStand = Animator.StringToHash("diddy_attack_to_stand");

        public static int Hooking = Animator.StringToHash("diddy_hooking");

        public static int Hook = Animator.StringToHash("diddy_hook");
        
        public static int StandToCrouch = Animator.StringToHash("diddy_stand_to_crouch");

        public static int Crouch = Animator.StringToHash("diddy_crouch");

        public static int CrouchToStand = Animator.StringToHash("diddy_crouch_to_stand");

        public static int AirToSomersault = Animator.StringToHash("diddy_air_to_somersault");

        public static int Somersault = Animator.StringToHash("diddy_somersault");
    }
}