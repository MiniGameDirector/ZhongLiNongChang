using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region ����
    private static UIManager instance;
    public static UIManager GetInstance() {
        if (instance == null)
        {
            instance = new UIManager();
        }
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Transform uiCanvas;

    public Button btnGameOver, btnRealOver, btnReturn;
    //�����л���ť
    public Button btnStartScene, btnGameScene;

    public Button btnStartGame, btnTurn;
    public Transform imgStart;

    public GameObject initObj;
    public Transform panelRecord;
    public Transform panelOver;
    public Text txtScoreBan, txtScore;
    // Start is called before the first frame update
    void Start()
    {
        InitButtonEvent();
    }
    /// <summary>
    /// ��ʼ���¼�
    /// </summary>
    private void InitButtonEvent() {
        //��ʼ��Ϸ
        btnStartGame.onClick.AddListener(StartScene);
        //����Ŀ¼
        btnTurn.onClick.AddListener(TurnMenuEvent);
        //��Ϸ����
        btnGameOver.onClick.AddListener(GameOverEvent);
        //��Ϸ������ť
        btnStartScene.onClick.AddListener(StartScene);
        //��Ϸ������ť
        btnGameScene.onClick.AddListener(GameScene);
        //�����˳���Ϸ��ť
        btnRealOver.onClick.AddListener(GameOverRealEvent);
        //�ָ���Ϸ
        btnReturn.onClick.AddListener(RestartGame);
    }
    /// <summary>
    /// ����������ť�¼�
    /// </summary>
    private void StartScene() {
        ClearText();
        imgStart.gameObject.SetActive(false);
        Text text1 = btnStartScene.transform.GetChild(0).GetComponent<Text>();
        text1.fontStyle = FontStyle.Bold;
        text1.color = Color.red;

        Text text2 = btnGameScene.transform.GetChild(0).GetComponent<Text>();
        text2.fontStyle = FontStyle.Normal;
        text2.color = Color.white;
        initObj.SetActive(true);
        panelRecord.gameObject.SetActive(false);
        ObjectController.GetInstance().GameScene(false);
    }
    /// <summary>
    /// ������Ϸ������ť�¼�
    /// </summary>
    public void GameScene() {
        Text text1 = btnGameScene.transform.GetChild(0).GetComponent<Text>();
        text1.fontStyle = FontStyle.Bold;
        text1.color = Color.red;

        Text text2 = btnStartScene.transform.GetChild(0).GetComponent<Text>();
        text2.fontStyle = FontStyle.Normal;
        text2.color = Color.white;
        initObj.SetActive(false);
        panelRecord.gameObject.SetActive(true);
        ObjectController.GetInstance().GameScene();
        AudioController.GetInstance().audioSource.clip = null;
    }
    /// <summary>
    /// ��Ϸ�����¼�
    /// </summary>
    public void GameOverEvent() {
        AudioController.GetInstance().DisableOther();
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName("����1������", false, AudioController.GetInstance().CreateAudio(),delegate() {
            StartCoroutine(AudioController.GetInstance().SetAudioClipByName("��������", false, null, GameOverRealEvent));
            AudioController.GetInstance().DisableOther();
        }));
        panelOver.gameObject.SetActive(true);
    }
    /// <summary>
    /// �˳���Ϸ
    /// </summary>
    private void GameOverRealEvent() {
        Application.Quit();
    }
    /// <summary>
    /// ����Ŀ¼�¼�
    /// </summary>
    private void TurnMenuEvent() {
        Application.Quit();
    }
    /// <summary>
    /// �ָ���Ϸ��ť�¼�
    /// </summary>
    private void RestartGame()
    {
        AudioController.GetInstance().DisableOther();
        AudioController.GetInstance().audioSource.clip = null;
        panelOver.gameObject.SetActive(false);
    }
    private void ClearText() {
        txtScore.text = "0";
        txtScoreBan.text = "0";
    }
   
    /// <summary>
    /// �ȷְ�
    /// </summary>
    /// <param name="xxObird">0 - ��Ӯ 1 - ��Ӯ</param>
    public void AddScore() {
        txtScore.text = TextAnim(txtScoreBan);
    }
    private string TextAnim(Text text) {
        Vector2 txtSize = text.transform.parent.GetComponent<RectTransform>().sizeDelta;
        text.transform.parent.GetComponent<RectTransform>().DOSizeDelta(txtSize * 1.2f, 0.2f).SetEase(Ease.Linear).OnComplete(delegate ()
        {
            text.transform.parent.GetComponent<RectTransform>().DOSizeDelta(txtSize, 0.2f).SetEase(Ease.Linear);
        });
        int dishuNum = int.Parse(text.text);
        dishuNum++;
        text.GetComponent<CountingNumber>().ChangeTo(dishuNum, 0.2f);
        return dishuNum.ToString();
    }
}
