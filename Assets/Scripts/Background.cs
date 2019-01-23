using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private GameObject bg1;
    [SerializeField] private GameObject bg2;

    [SerializeField] private float speed;
    [SerializeField] private float width = 12.8f;

    // Update is called once per frame
    void Update() {
        Vector2 moveVector = Time.deltaTime * speed * Vector2.left;
        bg1.transform.localPosition += (Vector3) moveVector;
        bg2.transform.localPosition += (Vector3) moveVector;

        if (bg1.transform.localPosition.x <= -width) {
            bg1.transform.localPosition += new Vector3(2 * width, 0, 0);
        }

         if (bg2.transform.localPosition.x <= -width) {
            bg2.transform.localPosition += new Vector3(2 * width, 0, 0);
        }
    }
}
