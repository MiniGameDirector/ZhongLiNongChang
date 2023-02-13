using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BearController : MonoBehaviour
{

    public Transform bear;
    public string startAnimName, latterAnimName;
    public Vector3 bearStandePos;
    private Animator bearAnimator;
    private Vector3 bearStartPos;
    private Tweener tween;

    // Start is called before the first frame update
    void Awake()
    {
        //bear = this.transform;
        bearStartPos = bear.position;
        bearAnimator = bear.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>
    /// 初始化 animation、transform
    /// </summary>
    public void OnEnable() {
        bearAnimator.Play(startAnimName);
        tween = bear.DOLocalMove(bearStandePos, 3f).SetEase(Ease.Linear).OnComplete(delegate ()
        {
            bearAnimator.Play(latterAnimName);
            StartCoroutine(AudioController.GetInstance().SetAudioClipByName("开场语音", false, null));
        });
    }
    public void OnDisable()
    {
        tween.Kill();
        bear.position = bearStartPos;
        StopAllCoroutines();
    }
}
