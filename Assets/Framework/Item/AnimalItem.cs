using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalItem : MonoBehaviour
{
    private List<string> anims = new List<string>();
    public Animator animator;
    public Vector3 itemScale = new Vector3(80, 80, 80);
    public bool isBig = false;
    // Start is called before the first frame update
    void Start()
    {
        anims.Add("Run");
        anims.Add("Jump");

        animator = transform.GetComponent<Animator>();
        animator.Play(anims[Random.Range(0, 2)]);
        MeshCollider mc = transform.GetChild(0).gameObject.AddComponent<MeshCollider>();
        mc.sharedMesh = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh;
        //mc.enabled = false;
        mc.convex = true;
        mc.isTrigger = true;
        transform.GetChild(0).gameObject.AddComponent<AnimalClick>();
        isBig = itemScale == new Vector3(80, 80, 80);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
