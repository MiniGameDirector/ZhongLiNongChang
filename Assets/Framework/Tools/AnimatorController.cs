using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController
{
    /// <summary>
    /// ͨ����������������Ŀ������Ķ���
    /// </summary>
    /// <param name="targetAnim"></param>
    /// <param name="animState"></param>
    public static void SetAnimatorByName(Animator targetAnim, string animState) {
        targetAnim.SetBool(animState, true);
    }
}
