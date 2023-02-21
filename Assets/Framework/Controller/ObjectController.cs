using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectController : MonoBehaviour
{
    #region 单例
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


    public Transform bear;
    public GameObject bgAudio;
    public List<GameObject> animalGos = new List<GameObject>();

    public Transform left, right;
    public Beside nowBeside;
    public GameObject bom, bom1, bom2;
    // Start is called before the first frame update
    void Start()
    {
        nowBeside = left.GetComponent<Beside>();
    }


    // Update is called once per frame
    void Update()
    {
       
    }


    /// <summary>
    /// 进出游戏事件
    /// </summary>
    /// <param name="startGame"></param>
    public void GameScene(bool startGame = true) {
        AudioController.GetInstance().DisableOther();
        bgAudio.SetActive(startGame);
        if (startGame)
        {
            Debug.Log("进入游戏");
            bear.gameObject.SetActive(false);
            StopAllCoroutines();
            CreateAnimalBeside(false);
            //设置相机的位置
        }
        else
        {
            Debug.Log("退出游戏");

            StartCoroutine(AudioController.GetInstance().SetAudioClipByName("突然--卡通", false, null, delegate () {
                bear.gameObject.SetActive(true);
            }));
            
            //设置相机的位置
        }
    }
    /// <summary>
    /// 随机创建小动物
    /// </summary>
    /// <param name="animalPa"></param>

    public AnimalItem CreateRandomAnimal(Transform animalPa) {
        GameObject go = Instantiate(animalGos[Random.Range(0, animalGos.Count)]);
        AnimalItem animalItem = go.GetComponent<AnimalItem>();
        go.transform.parent = animalPa;
        go.transform.localPosition = new Vector3(0, 68, 0);
        go.transform.localEulerAngles = Vector3.zero;
        go.transform.localScale = animalItem.itemScale;
        return animalItem;
    }

    public void CreateAnimalBeside(bool isLeft) {
        if (isLeft)
        {
            nowBeside = right.GetComponent<Beside>();
        }
        else
        {
            nowBeside = left.GetComponent<Beside>();
        }
        nowBeside.CreateAnimal();
    }

    public void CreateBom(int bomIndex,Transform animalItem,Vector3 targetPos) {
        if (bomIndex == 0)
        {
            GameObject go = Instantiate(bom, animalItem);
            go.transform.position = targetPos;
        }
        else if(bomIndex == 1)
        {
            GameObject go = Instantiate(bom1, animalItem);
            go.transform.position = targetPos;
        }
        else
        {
            GameObject go = Instantiate(bom2, animalItem);
            go.transform.position = targetPos;
        }
    }
}
