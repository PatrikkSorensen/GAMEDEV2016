using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlatformPuzzleEvent : MonoBehaviour {

    //TODO: Refactor this script
	public GameObject switch1, switch2, storyWall;
	HexagonSwitchScript switchScript1, switchScript2;
    bool eventTriggered = false;
    private AudioSource[] audioSources;
	  
	void Start()
	{
        audioSources = storyWall.GetComponents<AudioSource>();
		switchScript1 = switch1.GetComponent<HexagonSwitchScript>();
		switchScript2 = switch2.GetComponent<HexagonSwitchScript>();
	}
	void Update()
	{
		if (switchScript1.getStatus() && switchScript1.getStatus() && !eventTriggered)
		{
			triggerEvent(); 
		}
	}
	void triggerEvent()
	{
        // Transform
		DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        storyWall.transform.DOMove(new Vector3(0.0f, 10.0f, 0), 10).SetRelative().SetLoops(1, LoopType.Incremental);
        eventTriggered = true;

        // Audio
        audioSources[0].Play();
        audioSources[1].Play();
        StartCoroutine(playPostEventSound());
	}

    IEnumerator playPostEventSound()
    {
        yield return new WaitForSeconds(10);
        audioSources[1].Stop();
    }
}
