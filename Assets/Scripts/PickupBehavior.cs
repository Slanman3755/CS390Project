using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PickupBehavior : MonoBehaviour {

    public SteamVR_Action_Boolean PickupAction;
    
    private GameObject collidingObject = null;
    private GameObject objectInHand = null;

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
            if (collidingObject && !objectInHand && PickupAction.state)
            {
                Grab();
            } else if (objectInHand && !PickupAction.state)
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

    void Grab()
    {
        objectInHand = collidingObject;
        objectInHand.transform.SetParent(this.transform);
        objectInHand.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Release()
    {
        objectInHand.GetComponent<Rigidbody>().isKinematic = false;
        objectInHand.transform.SetParent(null);
        objectInHand = null;
    }
}
