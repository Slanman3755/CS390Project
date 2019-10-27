
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PickupBundle : MonoBehaviour
{

    public SteamVR_Action_Boolean PickupAction;
    public SteamVR_Action_Boolean BundleAction;

    private GameObject collidingObject = null;
    private List<GameObject> objectsInHand = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Teleport.instance.enabled = !(PickupAction.active && PickupAction.state);
        if (PickupAction.active)
        {
            if (collidingObject && PickupAction.state)
            {
                Hold();
            }
            else if (objectsInHand.Count > 0 && !PickupAction.state)
            {
                Release();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            collidingObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        collidingObject = null;
    }

    void Hold()
    {
        collidingObject.GetComponent<Rigidbody>().WakeUp();
        objectsInHand.Add(collidingObject);
        collidingObject.transform.SetParent(this.transform);
        collidingObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Release()
    {
        foreach(GameObject go in objectsInHand) {
            go.transform.SetParent(null);
        }
        objectsInHand.Clear();
    }
}
