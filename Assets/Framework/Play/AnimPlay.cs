using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlay : MonoBehaviour
{
    public bool isInit = false;
    public string initAudioName;
    private void OnEnable()
    {
        if (string.IsNullOrEmpty(initAudioName))
        {
            return;
        }
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName(initAudioName));
        AudioController.GetInstance().DisableOther();
    }
    /// <summary>
    /// ����������ɺ󲥷�����
    /// </summary>
    /// <param name="_audioClip"></param>
    public void AnimCompleteToAudio(string _audioClip) {
        if (string.IsNullOrEmpty(_audioClip))
        {
            return;
        }
        initAudioName = _audioClip;
        Debug.Log(_audioClip);
        //StartCoroutine(AudioController.GetInstance().SetAudioClipByName(_audioClip, false, null, AnimCompleteToDisplay));
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName(_audioClip, false, null, null));
        isInit = true;
    }
    /// <summary>
    /// ����������ɺ�����С��
    /// </summary>
    public void AnimCompleteToDisplay() {
        transform.parent.gameObject.SetActive(false);
        UIManager.GetInstance().GameScene();
    }
}
