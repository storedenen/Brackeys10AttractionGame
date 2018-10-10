using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {
    public GameObject Enemy;
    public int Points = 0;
    public UnityEngine.UI.Text Ptext;

    IEnumerator CoSpawn() {
        while (true) {
            GameObject newEnemy = Instantiate(Enemy);
            newEnemy.transform.position = new Vector3(Random.value * 10, 5.2f, 0f);
            newEnemy = Instantiate(Enemy);
            newEnemy.transform.position = new Vector3(Random.value * -10, 5.2f, 0f);
            yield return new WaitForSeconds(1f);
        }
    }

	// Use this for initialization
	void Start () {
        Physics.gravity = 2f * Vector3.down;
        Ptext = FindObjectOfType<UnityEngine.UI.Text>();
        StartCoroutine(CoSpawn());
	}
	
	// Update is called once per frame
	void Update () {
        Ptext.text = "Points: " + Points;
	}

    private void FixedUpdate() {
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * 5);
        transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * 5);
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<Rigidbody>().AddForce((transform.position - other.transform.position).normalized * 10f);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
