using UnityEngine;
using System.Collections;

public class FlimsyWallReset : MonoBehaviour {

    private Vector3 startingPosition;
    private Quaternion startingRotation;

    // Use this for initialization

    void Awake()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    // Update is called once per frame

    public void Reset()
    {
        transform.position = startingPosition;
        transform.rotation = startingRotation;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        Awake();
    }
}
