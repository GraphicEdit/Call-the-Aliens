using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private GameObject[] satellites;
    [SerializeField] private float[] satellitesSpeeds;

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < satellites.Length; i++) {
            GameObject satellite = satellites[i];
            satellite.transform.Rotate(Vector3.forward, Time.deltaTime * satellitesSpeeds[i]);
        }
    }
}
