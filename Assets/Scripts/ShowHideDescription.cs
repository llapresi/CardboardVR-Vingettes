using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideDescription : MonoBehaviour {

    public float finalScale;
    public float transitionTime;
    bool isCurrentlyShown;
    bool currentlyTransitioning = false;

	// Use this for initialization
	void Start () {
        isCurrentlyShown = false;
        transform.localScale = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleCanvas()
    {
        if(!currentlyTransitioning)
        {
            StartCoroutine(ShowHideCanvas());
        }
    }

    IEnumerator ShowHideCanvas()
    {
        // Track if corroutine is running
        currentlyTransitioning = true;

        // Setup on start and end scales
        Vector3 startScale;
        Vector3 endScale;
        if (!isCurrentlyShown)
        {
            startScale = Vector3.zero;
            endScale = new Vector3(finalScale, finalScale, finalScale);
        }
        else
        {
            startScale = new Vector3(finalScale, finalScale, finalScale);
            endScale = Vector3.zero;
        }

        float timeLeft = transitionTime;
        while (timeLeft >= 0.0f)
        {
            timeLeft -= Time.deltaTime;
            float rescaledProgress = (transitionTime - timeLeft) / transitionTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, Mathf.SmoothStep(0f, 1f, rescaledProgress));

            yield return null;

        }

        // Set final scale
        transform.localScale = endScale;
        isCurrentlyShown = !isCurrentlyShown;

        // Track if corroutine is running
        currentlyTransitioning = false;
    }
}
