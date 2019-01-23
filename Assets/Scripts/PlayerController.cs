using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject dishMount;
    [SerializeField] private GameObject signalSpawn;
    [SerializeField] private GameObject signalPrefab;

    [SerializeField] private AudioClip[] signalSounds;

    [SerializeField] private float signalForce;

    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update() {
        HandleDishRotation();

        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    void HandleDishRotation() {
        Vector3 newDishDirection = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2) transform.position;
        newDishDirection = newDishDirection.normalized;
        dishMount.transform.up = newDishDirection;
    }

    void Shoot() {
        GetComponent<AudioSource>().PlayOneShot(signalSounds[Random.Range(0, signalSounds.Length)]);
        Vector2 direction = dishMount.transform.up;
        GameObject newSignal = Instantiate(signalPrefab);
        newSignal.transform.position = signalSpawn.transform.position;
        newSignal.GetComponent<Rigidbody2D>().AddForce(direction * signalForce, ForceMode2D.Impulse);
    }
}
