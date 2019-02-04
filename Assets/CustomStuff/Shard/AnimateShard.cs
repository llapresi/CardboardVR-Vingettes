using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateShard : MonoBehaviour {

    public float amplitude;
    public float frequency;
    float elapsedTime;
    Vector3 basePosition;

	// Use this for initialization
	void Start () {
        basePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        elapsedTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime * frequency;
        transform.position = basePosition + new Vector3(0.0f, Mathf.Sin(elapsedTime) * amplitude, 0.0f);
    }
}
