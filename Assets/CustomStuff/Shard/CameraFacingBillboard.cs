using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
    public Camera m_Camera;
    public float pushfowardAmount = 0.3f;
    Vector3 basePosition;

    private void Start()
    {
        basePosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        Vector3 targetPostition = new Vector3(m_Camera.transform.position.x,
                                        this.transform.position.y,
                                        m_Camera.transform.position.z);
        transform.LookAt(targetPostition);
        Vector3 dir = (this.transform.position - m_Camera.transform.position).normalized;

        transform.position = basePosition + (-dir * pushfowardAmount);
    }
}
