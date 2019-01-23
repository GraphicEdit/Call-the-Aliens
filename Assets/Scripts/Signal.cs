using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour
{
    private float timeLived = 0f;
    private float timeBeforeSplit = 0.05f;

    [SerializeField] private AudioClip[] absorbSounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = GetComponent<Rigidbody2D>().velocity.normalized;
        timeLived += Time.deltaTime;
        if (Mathf.Abs(transform.position.x) > 30 || Mathf.Abs(transform.position.y) > 30) {
            Destroy(gameObject);
        }
    }
    

    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log(timeLived);
        
    }

     void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("BlackHoleCenter")) {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(absorbSounds[Random.Range(0, absorbSounds.Length)]);
            Destroy(gameObject);
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Relay") && timeLived > timeBeforeSplit) {
            GameObject newSignal1 = Instantiate(gameObject);
            newSignal1.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
            newSignal1.transform.Rotate(Vector3.forward, 15f);
            newSignal1.GetComponent<Rigidbody2D>().velocity = newSignal1.GetComponent<Rigidbody2D>().velocity.magnitude * newSignal1.transform.up;
            GameObject newSignal2 = Instantiate(gameObject);
            newSignal2.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
            newSignal2.transform.Rotate(Vector3.forward, -15f);
            newSignal2.GetComponent<Rigidbody2D>().velocity = newSignal2.GetComponent<Rigidbody2D>().velocity.magnitude * newSignal2.transform.up;
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Goal")) {
            Camera.main.GetComponent<LevelManager>().GoToLevel(Camera.main.GetComponent<LevelManager>().nextLevel);
        }
    }
}
