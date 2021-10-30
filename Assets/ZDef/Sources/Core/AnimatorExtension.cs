using System.Collections;
using UnityEngine;

namespace ZDef.Core
{
    public static class AnimatorExtension
    {
        public static IEnumerator SetBoolAndWaitStateAsync(this Animator animator, string parameterName, bool value, string stateName)
        {
            animator.SetBool(parameterName, value);
            while (!animator.IsState(stateName))
            {
                yield return 0;
            }
        }
        
        public static bool IsState(this Animator animator, string stateName)
        {
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return animatorStateInfo.IsName(stateName);
        }
    }
}