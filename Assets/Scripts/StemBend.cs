using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemBend : MonoBehaviour
{
    public Vector3 velocity;
    private Vector3 lastPosition;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = transform.position - lastPosition;
        lastPosition = transform.position;
    }
}
