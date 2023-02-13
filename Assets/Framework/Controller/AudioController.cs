using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    #region ����
    private static AudioController instance;
    public static AudioController GetInstance() {
        if (instance == null)
        {
            instance = new AudioController();
        }
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public AudioSource audioSource;
    public GameObject intantAudio;
    public List<AudioClip> audioClips = new List<AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        //audioSource.clip = audioClips[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// ��Ϸ��ʼ����Ч
    /// </summary>
    public void StartGame() {
        //������Чʱ���е㳤�����Խ���һ����ʱ��audiosource
        //StartCoroutine(SetAudioClipByName("boss����_0001", false, CreateAudio()));
    }
    /// <summary>
    /// ���ص��������Ч
    /// </summary>
    public void DisableOther() {
        for (int i = 1; i < audioSource.transform.parent.childCount; i++)
        {
            Destroy(audioSource.transform.parent.GetChild(i).gameObject);
        }
    }
    /// <summary>
    /// ͨ��audioclip������������audio
    /// </summary>
    /// <param name="_clip"></param>
    public IEnumerator SetAudioClipByName(string _clip, bool isLoop = false, AudioSource _audioSource = null, Action complete = null, float waittime = -1f) {
        for (int i = 0; i < audioClips.Count; i++)
        {
            if (audioClips[i].name == _clip)
            {
                if (_audioSource == null)
                {
                    audioSource.clip = audioClips[i];
                    audioSource.Play();
                    audioSource.loop = isLoop;
                }
                else
                {
                    _audioSource.gameObject.SetActive(true);
                    _audioSource.clip = audioClips[i];
                    _audioSource.Play();
                    _audioSource.loop = isLoop;
                }
                
            }
        }
        //�ȴ���Ƶ������ɺ�ִ�к�������
        yield return new WaitForSeconds(waittime >= 0 ? waittime : (_audioSource == null ? audioSource.clip.length : _audioSource.clip.length + 0.5f));
        if (_audioSource != null && !isLoop)
        {
            Destroy(_audioSource.gameObject);
        }
        complete?.Invoke();
    }
    /// <summary>
    /// �����µ�audio
    /// </summary>
    /// <returns></returns>
    public AudioSource CreateAudio() {
        GameObject go = Instantiate(intantAudio, audioSource.transform.parent) as GameObject;
        AudioSource audio = go.GetComponent<AudioSource>();
        return audio;
    }
}
