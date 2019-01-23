using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour
{
    public GameObject alienAnimation;

    private float timeLived = 0f;
    private float timeBeforeSplit = 0.05f;

    private bool isFinished = false;

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
        if ((Mathf.Abs(transform.position.x) > 30 || Mathf.Abs(transform.position.y) > 30) && !isFinished) {
            Destroy(gameObject);
        }

        RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position + transform.up * 2, LayerMask.GetMask("Goal"));
        if (hit.collider != null && isFinished == false && !hit.collider.gameObject.GetComponent<Goal>().isEnd) {
            Time.timeScale = 0.2f;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.up);
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
            if (col.gameObject.GetComponent<Goal>().isEnd == false) {
                col.gameObject.GetComponent<Goal>().isEnd = true;
                isFinished = true;
                Time.timeScale = 1f;
                StartCoroutine("Finish");
            }
        }
    }

    IEnumerator Finish() {
        alienAnimation.GetComponent<Animator>().SetTrigger("go");
        alienAnimation.GetComponent<AudioSource>().PlayDelayed(0.7f);
        yield return new WaitForSeconds(5f);
        Camera.main.GetComponent<LevelManager>().GoToLevel(Camera.main.GetComponent<LevelManager>().nextLevel);
    }
}
