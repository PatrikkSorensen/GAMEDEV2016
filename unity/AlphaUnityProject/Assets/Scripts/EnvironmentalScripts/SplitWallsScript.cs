using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SplitWallsScript : MonoBehaviour {
    public Transform wall1, wall2;

    private bool isSceneFinished, isScenePlaying = false;

    public bool IsSceneFinished
    {
        get { return isSceneFinished; }
        set {isSceneFinished = value;}
    }

    public IEnumerator SplitWalls()
    {
        if (isScenePlaying)
            yield break; 
        else 
            isScenePlaying = true; 

        // NOTE: 4 is the number of rows per column of the split walls. 
        // This is VERY hardcoded, please don't use anywhere else 
        for(int i = 3; i>=0; i--)
        {
            wall1.GetChild(i).transform.DOMove(new Vector3(-7.0f, 0.0f, 0.0f), 2.0f).SetRelative().SetLoops(1, LoopType.Incremental);
            wall2.GetChild(i).transform.DOMove(new Vector3(7.0f, 0.0f, 0.0f), 2.0f).SetRelative().SetLoops(1, LoopType.Incremental);
            yield return new WaitForSeconds(1.0f);
        }

        yield return new WaitForSeconds(1.0f);

        for (int i = 7; i >= 4; i--)
        {
            wall1.GetChild(i).transform.DOMove(new Vector3(-7.0f, 0.0f, 0.0f), 2.0f).SetRelative().SetLoops(1, LoopType.Incremental);
            wall2.GetChild(i).transform.DOMove(new Vector3(7.0f, 0.0f, 0.0f), 2.0f).SetRelative().SetLoops(1, LoopType.Incremental);
            yield return new WaitForSeconds(1.0f);
        }

        yield return new WaitForSeconds(1.0f);

        for(int i = 0; i<wall1.childCount; i++)
        {
            wall1.GetChild(i).DOMove(new Vector3(0.0f, -5.0f, 0.0f), 2.0f).SetRelative().SetLoops(1, LoopType.Yoyo);
            wall2.GetChild(i).DOMove(new Vector3(0.0f, -5.0f, 0.0f), 2.0f).SetRelative().SetLoops(1, LoopType.Yoyo);
        }

        IsSceneFinished = true; 
    }
}
