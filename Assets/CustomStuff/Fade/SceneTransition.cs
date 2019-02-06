using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    public Renderer screenFade;
    public float transitionTime;
    public string SceneToLoad;

    // Use this for initialization
    void Start () {
        StartCoroutine(OnNewScene());
	}
	
	// Update is called once per frame
	public void LoadScene () {
        StartCoroutine(LoadNewScene());
    }

    IEnumerator LoadNewScene()
    {
        float timeLeft = transitionTime;
        while (timeLeft >= 0.0f)
        {
            timeLeft -= Time.deltaTime;
            float rescaledProgress = (transitionTime - timeLeft) / transitionTime;
            screenFade.material.SetFloat("_Scale", rescaledProgress);

            yield return null;

        }

        // Load the scene now
        SceneManager.LoadScene(SceneToLoad);
    }

    IEnumerator OnNewScene()
    {
        float timeLeft = transitionTime;
        while (timeLeft >= 0.0f)
        {
            timeLeft -= Time.deltaTime;
            float rescaledProgress = 1 - (transitionTime - timeLeft) / transitionTime;
            screenFade.material.SetFloat("_Scale", rescaledProgress);

            yield return null;

        }

        // Load the scene now
    }
}
