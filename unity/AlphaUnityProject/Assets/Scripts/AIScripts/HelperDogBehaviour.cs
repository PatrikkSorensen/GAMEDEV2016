using UnityEngine;
using System.Collections;

public class HelperDogBehaviour : Agent
{
    public AudioClip bootupClip;
    public GameObject lightstationMother;


	void Update () {
        if (!IsActive)
            return;


        if (!isBusy)
            StartCoroutine(MoveAndFix());
        Debug.DrawLine(transform.position, dest, Color.white);
    }

    override
    public void ActivateAgent()
    {
        base.ActivateAgent();
        
        audioSource.clip = bootupClip;
        audioSource.Play();
        //anim.SetBool("isActive", IsActive);
    }

    IEnumerator MoveAndFix()
    {
        Vector3 destination = lightstationMother.transform.position + new Vector3(Random.Range(-15.0f, 15.0f), 0.0f, Random.Range(-15.0f, 15.0f));
        MoveAgent(destination);

        while (IsBusy)
        {
            if (Vector3.Distance(transform.position, dest) < 0.5f)
            {
                IsBusy = false;
                Debug.Log("Reached destination!");
            }

            yield return new WaitForSeconds(5.0f);
        }

        yield return null; 
    }
}
