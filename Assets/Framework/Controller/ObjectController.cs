using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectController : MonoBehaviour
{
    #region ����
    private static ObjectController instance;
    public static ObjectController GetInstance() {
        if (instance == null)
        {
            instance = new ObjectController();
        }
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
       
    }


    // Update is called once per frame
    void Update()
    {
       
    }


    /// <summary>
    /// ������Ϸ�¼�
    /// </summary>
    /// <param name="startGame"></param>
    public void GameScene(bool startGame = true) {
        AudioController.GetInstance().DisableOther();
        if (startGame)
        {
            Debug.Log("������Ϸ");
            //���������λ��
        }
        else
        {
            Debug.Log("�˳���Ϸ");
            //���������λ��
        }
    }
}
