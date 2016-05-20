using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubertHealth : MonoBehaviour
{
    public GameObject PlayerOrigin;

    public GameObject LastCheckPoint;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitDeath()
    {
        FindObjectOfType<Fade>().FadeToLevel(-1);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 5);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Debug.Log("FROZE PLAYER");
        yield return new WaitForSeconds(FindObjectOfType<Fade>().FadeTime);
        Debug.Log("WAIT OVER");
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (go.GetComponent<SpiderMove>() != null)
            {
                go.GetComponent<SpiderMove>().Reset();
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("FlimsyWall"))
        {
            if (go.GetComponent<FlimsyWallReset>() != null)
            {
                go.GetComponent<FlimsyWallReset>().Reset();
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Spicer"))
        {
            if (go.GetComponent<FlimsyWallReset>() != null)
            {
                go.GetComponent<FlimsyWallReset>().Reset();
            }
        }
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Debug.Log("UNFROZE PLAYER");
        if (LastCheckPoint != null)
        {
            PlayerOrigin.transform.position = new Vector3(LastCheckPoint.transform.position.x, LastCheckPoint.transform.position.y, 0);
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
    }

    void OnLevelWasLoaded()
    {
        gameObject.transform.localPosition = (Vector3)Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == ("Enemy"))
        {
            StartCoroutine(WaitDeath());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == ("Goal"))
        {
            if (SceneManager.GetActiveScene().buildIndex >= 2)
            {
                PlayerPrefs.SetInt("Unlocked", SceneManager.GetActiveScene().buildIndex);
            }
            Debug.Log("Int = " + PlayerPrefs.GetInt("Unlocked") + " BuildIndex = " + SceneManager.GetActiveScene().buildIndex);
            FindObjectOfType<Fade>().FadeToLevel("+1");
        }
        if (col.gameObject.tag == ("CheckPoint"))
        {
            LastCheckPoint = col.gameObject;
        }
        if (col.gameObject.tag == ("Enemy"))
        {
            StartCoroutine(WaitDeath());
        }
    }

    void OnParticleCollision(GameObject other)
    {
        StartCoroutine(WaitDeath());
        Debug.Log("PARTICLE COLLISION");
    }
}
