using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beside : MonoBehaviour
{
    public bool isLeft = true;
    public bool nowBeside = false;
    public AnimalState animalState = AnimalState.run;
    private AnimalItem animalItem;
    public Tweener transRot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void CreateAnimal()
    {
        transform.localEulerAngles = new Vector3(-46, 0, isLeft ? -10 : 10);
        animalItem = ObjectController.GetInstance().CreateRandomAnimal(transform).GetComponent<AnimalItem>();
        animalState = AnimalState.run;
        StartCoroutine(WaitFixed());
    }
    private IEnumerator WaitFixed() {
        yield return new WaitForFixedUpdate();
        float moveTime1 = Random.Range(1f, 1.5f);
        transRot = transform.DORotate(new Vector3(10, 0, isLeft ? -20 : 20), moveTime1).SetEase(Ease.Linear).OnComplete(delegate ()
        {
            animalState = AnimalState.idle;
            //animalItem.transform.GetChild(0).GetComponent<MeshCollider>().enabled = true;
        });
        //float idleTime = Random.Range(1f, 1.5f);
        yield return new WaitForSeconds(moveTime1 + 2f);
        if (animalState == AnimalState.idle)
        {
            animalState = AnimalState.hide;
            animalItem.transform.localEulerAngles = new Vector3(0, isLeft ? 90f : -90f, 0);
            transform.DOLocalRotate(new Vector3(10, 0, isLeft ? -60f : 60f), 1f).SetEase(Ease.Linear).OnComplete(delegate() {
                ObjectController.GetInstance().CreateAnimalBeside(isLeft);
                Destroy(animalItem.gameObject);
            });
        }
    }
    public void ResetState() {
        ObjectController.GetInstance().CreateAnimalBeside(isLeft);
        Destroy(animalItem.gameObject);
    }
}

public enum AnimalState { 
    run,
    idle,
    hide,
    click
}
