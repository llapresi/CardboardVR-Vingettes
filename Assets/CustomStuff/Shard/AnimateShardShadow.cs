using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateShardShadow : MonoBehaviour {

    public float amplitude;
    public float frequency;
    float elapsedTime;
    Vector3 baseScale;

	// Use this for initialization
	void Start () {
        baseScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        elapsedTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime * frequency;
        transform.localScale = baseScale + new Vector3(Mathf.Sin(elapsedTime) * amplitude, Mathf.Sin(elapsedTime) * amplitude, 0.0f);
    }
}
