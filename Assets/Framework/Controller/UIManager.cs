using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region 单例
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
    //场景切换按钮
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
    /// 初始化事件
    /// </summary>
    private void InitButtonEvent() {
        //开始游戏
        btnStartGame.onClick.AddListener(StartScene);
        //返回目录
        btnTurn.onClick.AddListener(TurnMenuEvent);
        //游戏结束
        btnGameOver.onClick.AddListener(GameOverEvent);
        //游戏开场按钮
        btnStartScene.onClick.AddListener(StartScene);
        //游戏场景按钮
        btnGameScene.onClick.AddListener(GameScene);
        //最终退出游戏按钮
        btnRealOver.onClick.AddListener(GameOverRealEvent);
        //恢复游戏
        btnReturn.onClick.AddListener(RestartGame);
    }
    /// <summary>
    /// 开场场景按钮事件
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
    /// 进入游戏场景按钮事件
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
    /// 游戏结束事件
    /// </summary>
    public void GameOverEvent() {
        AudioController.GetInstance().DisableOther();
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName("开场1秒音乐", false, AudioController.GetInstance().CreateAudio(),delegate() {
            StartCoroutine(AudioController.GetInstance().SetAudioClipByName("结束语音", false, null, GameOverRealEvent));
            AudioController.GetInstance().DisableOther();
        }));
        panelOver.gameObject.SetActive(true);
    }
    /// <summary>
    /// 退出游戏
    /// </summary>
    private void GameOverRealEvent() {
        Application.Quit();
    }
    /// <summary>
    /// 返回目录事件
    /// </summary>
    private void TurnMenuEvent() {
        Application.Quit();
    }
    /// <summary>
    /// 恢复游戏按钮事件
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
    /// 比分扳
    /// </summary>
    /// <param name="xxObird">0 - 黄赢 1 - 蓝赢</param>
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
