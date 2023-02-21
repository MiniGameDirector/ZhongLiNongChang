using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalClick : MonoBehaviour
{
    private int clickNum = 0;
    RaycastHit hit;
    Vector3 hitPos;
    private bool isClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseUp()
    {
        Debug.Log("�����");
        transform.parent.parent.GetComponent<Beside>().animalState = AnimalState.click;
        //�������Ļ��λ��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            hitPos = hit.point;
        }
        if (!isClicked)
        {
            isClicked = true;
            if (!transform.parent.GetComponent<AnimalItem>().isBig)
            {
                ObjectController.GetInstance().nowBeside.GetComponent<Beside>().transRot.Kill();
                ObjectController.GetInstance().CreateBom(0, transform.parent, hitPos);
                StartCoroutine(AudioController.GetInstance().SetAudioClipByName("��ͨ����", false,
                    AudioController.GetInstance().CreateAudio()));
                StartCoroutine(AudioController.GetInstance().SetAudioClipByName("С���ﰤ��+����", false,
                    AudioController.GetInstance().CreateAudio(), delegate () {
                        transform.parent.parent.GetComponent<Beside>().ResetState();
                        UIManager.GetInstance().AddScore();
                    }));
                transform.parent.GetComponent<AnimalItem>().animator.Play("Die");
            }
            else
            {
                if (clickNum == 0)
                {
                    ObjectController.GetInstance().nowBeside.GetComponent<Beside>().transRot.Pause();
                    StartCoroutine(AudioController.GetInstance().SetAudioClipByName("��ͨ����", false,
                    AudioController.GetInstance().CreateAudio(), delegate ()
                    {
                        isClicked = false;
                        ObjectController.GetInstance().nowBeside.GetComponent<Beside>().transRot.Play();
                    }, 0.2f));
                    ObjectController.GetInstance().CreateBom(1, transform.parent, hitPos);
                    transform.parent.GetComponent<AnimalItem>().animator.Play("Damage");
                    clickNum += 1;
                }
                else
                {
                    ObjectController.GetInstance().nowBeside.GetComponent<Beside>().transRot.Kill();
                    ObjectController.GetInstance().CreateBom(2, transform.parent, hitPos);
                    StartCoroutine(AudioController.GetInstance().SetAudioClipByName("��ͨ����", false,
                    AudioController.GetInstance().CreateAudio()));
                    StartCoroutine(AudioController.GetInstance().SetAudioClipByName("����ﵹ��", false,
                        AudioController.GetInstance().CreateAudio(), delegate () {
                            transform.parent.parent.GetComponent<Beside>().ResetState();
                            UIManager.GetInstance().AddScore();
                        }));
                    transform.parent.GetComponent<AnimalItem>().animator.Play("Die");
                }
            }
        }
        
    }
}
