using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/**
 * TeleportationHandler
 * Author: Steven Yi
 * Description: Handles teleportation of user when gaze intersects with the floor. 
 * Shows a teleportation target as well as hides the Reticle to signify to user that interaction 
 * is different for teleportation versus triggering an interactive object.
 *
 * This script also implements IPointerClickHandler directly, rather than using an EventTrigger component 
 * and routing the event to this script.  This, together with using Update(), is done to show an alternative
 * to EventTrigger.  For an optimization, a Coroutine might be used for the Raycasting that begins on PointerEnter and 
 * ends on PointerExit. 
 */
public class TeleportationHandler : MonoBehaviour, IPointerClickHandler {

    public GameObject player;
    public GameObject teleTarget;
    public GvrReticlePointer reticlePointer;

    private Vector3 fakeHide = new Vector3(0.0f, -20.0f, 0.0f);

	void Start () {
	}

    /**
     *  Finds the Position where the Ray from the Camera intersects the floor (the GameObject this script is attached to).
     *  If it does not intersect with this floor, returns fakeHide, which gets used to move the teleTarget below the 
     *  floor.
     */
    Vector3 GetIntersection()
    {

        // Get a reference to the camera tagged as MainCamera
        Camera camera = Camera.main;

        // Create Ray pointing from the Main Camera's position and in the direction of the Camera's rotation
        Ray ray = new Ray(camera.transform.position, camera.transform.rotation * Vector3.forward);
        // The RayCastHit result from checking the Raycast.  Will be set with an object value as it is used as 
        // an out parameter to Physics.Raycast()
        RaycastHit hit;

        // Check if Raycast has hit and object...
        if(Physics.Raycast(ray, out hit))
        {
            // ... then check if the object hit is the gameObject this script is attached to (the floor)...
            if(hit.collider.gameObject == gameObject)
            {
                return hit.point;
            }
        }
        return fakeHide;
    }


    /*
     * Update the position of the teletarget according to where the camera hits the floor.
     * Fake hides the target if the camera is not currently hitting the floor by moving the position
     * below the floor.
     */
    void LateUpdate()
    {
        Vector3 hitPosition = GetIntersection();
        teleTarget.transform.position = hitPosition;

        reticlePointer.GetComponent<Renderer>().enabled = (hitPosition == fakeHide);
    }

    /**
     * Teleport the player to the x/z position of the intersection with the floor. Keeps the 
     * player's current y so that the head position remains constant.
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 hitPosition = GetIntersection();
        bool found = hitPosition != fakeHide;

        if (found)
        {
            player.transform.position = new Vector3(hitPosition.x, hitPosition.y + 1.8f, hitPosition.z);
        }
    }
}
