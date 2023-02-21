using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BearController : MonoBehaviour
{

    public Transform bear;
    public string startAnimName, latterAnimName;
    private Animator bearAnimator;
    private Vector3 bearStartRot;
    private Tweener tween;

    // Start is called before the first frame update
    void Awake()
    {
        bear = this.transform;
        bearStartRot = bear.localEulerAngles;
        bearAnimator = bear.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>
    /// ��ʼ�� animation��transform
    /// </summary>
    public void OnEnable() {
        //bearAnimator.Play(startAnimName);
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName("�����ܲ�", false, 
            AudioController.GetInstance().CreateAudio()));
        tween = bear.DORotate(Vector3.zero, 3.4f).SetEase(Ease.Linear).OnComplete(delegate ()
        {
            //bearAnimator.Play(latterAnimName);
            StartCoroutine(AudioController.GetInstance().SetAudioClipByName("����", false, 
                AudioController.GetInstance().CreateAudio(),delegate() {
                    UIManager.GetInstance().GameScene();
                }));
        });
    }
    public void OnDisable()
    {
        tween.Kill();
        bear.localEulerAngles = bearStartRot;
        StopAllCoroutines();
    }
}
