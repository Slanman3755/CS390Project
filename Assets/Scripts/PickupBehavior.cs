using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PickupBehavior : MonoBehaviour {

    public SteamVR_Action_Boolean PickupAction;
    public SteamVR_Action_Boolean BundleAction;

    private GameObject collidingObject = null;
    private GameObject objectInHand = null;

    private List<GameObject> bundle = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Teleport.instance.enabled = !(BundleAction.active && BundleAction.state);
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

        if(BundleAction.active) {
            if (collidingObject && BundleAction.state)
            {
                Hold();
            }
            else if (bundle.Count > 0 && !BundleAction.state)
            {
                ReleaseBundle();
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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        collidingObject = null;
    }

    void Grab()
    {
        collidingObject.GetComponent<Rigidbody>().WakeUp();
        objectInHand = collidingObject;
        objectInHand.transform.SetParent(this.transform);
        objectInHand.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Release()
    {
        objectInHand.transform.SetParent(null);
        objectInHand = null;
    }

    void Hold() 
    {
        collidingObject.GetComponent<Rigidbody>().WakeUp();
        bundle.Add(collidingObject);
        collidingObject.transform.SetParent(this.transform);
    }

    void ReleaseBundle() 
    {
        foreach (GameObject go in bundle)
        {
            go.transform.SetParent(null);
        }
        bundle.Clear();
    }
}
