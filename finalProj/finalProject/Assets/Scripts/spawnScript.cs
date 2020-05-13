using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour
{

    public GameObject spherePrefab;
    float respawnTime = .8f;
    private Vector3 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0));
        StartCoroutine(sphereDrop());
    }

    // Update is called once per frame
    IEnumerator sphereDrop()
    {
		while (true)
		{
            yield return new WaitForSeconds(respawnTime);
            spawn();
        }
    }

    private void spawn() {
        GameObject a = Instantiate(spherePrefab) as GameObject;
        a.transform.position = new Vector3(Random.Range(-10,10), screenBounds.y*3,0);
    }
}



