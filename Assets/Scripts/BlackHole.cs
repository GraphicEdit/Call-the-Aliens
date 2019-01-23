using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float pullStrength = 100f;
    // Update is called once per frame
    void Update() {
        // rotate
        transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed);
    }
}
