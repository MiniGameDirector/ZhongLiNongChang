using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseItem : MonoBehaviour
{
    //初始化 显示事件 隐藏事件 销毁事件
    public UnityAction enableAction, disableAction, destroyAction;
    private Dictionary<string, Tweener> dicTweeners = new Dictionary<string, Tweener>();
    private void OnEnable()
    {
        enableAction?.Invoke();
    }
    private void OnDisable()
    {
        StopTween();
        disableAction?.Invoke();
    }
    private void OnDestroy()
    {
        StopTween();
        destroyAction?.Invoke();
    }
    private void StopTween(bool realOver = true) {
        StopAllCoroutines();
        if (dicTweeners.Count > 0)
        {
            foreach (KeyValuePair<string, Tweener> item in dicTweeners)
            {
                if (realOver)
                {
                    item.Value.Kill();
                }
                else
                {
                    item.Value.Pause();
                }
               
            }
        }
    }
    /// <summary>
    /// 添加 动画
    /// </summary>
    /// <param name="tweenerName"></param>
    /// <param name="tweener"></param>
    public void AddTweener(string tweenerName, Tweener tweener) {
        if (dicTweeners.ContainsKey(tweenerName))
        {
            dicTweeners[tweenerName].Kill();
            dicTweeners[tweenerName] = tweener;
        }
        else
        {
            dicTweeners.Add(tweenerName, tweener);
        }
    }
    public void PauseTweener() {
        StopTween(false);
    }
    public void RestartGame() {
        if (dicTweeners.Count > 0)
        {
            foreach (KeyValuePair<string, Tweener> item in dicTweeners)
            {
                item.Value.Play();
            }
        }
    }
}
