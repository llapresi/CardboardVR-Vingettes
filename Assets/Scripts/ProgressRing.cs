using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressRing : MonoBehaviour {
    public Image ringImage;

    public static ProgressRing current { get; private set; }

    private GvrReticlePointer rp;

	// Use this for initialization
	void Start () {
        ProgressRing.current = this;
        ringImage.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        rp = GvrPointerInputModule.FindInputModule().Impl.Pointer as GvrReticlePointer;
        RectTransform rt = GetComponent<RectTransform>();
        Vector3 pos = new Vector3(rt.localPosition.x, rt.localPosition.y, 
            rp.ReticleDistanceInMeters);
        rt.anchoredPosition3D = pos;
	}
}
