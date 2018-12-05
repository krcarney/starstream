using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarSpawner : MonoBehaviour
{
    public GameObject star;
    public static int starsOnScreen;
    Vector3 camera_point;
    Camera cam;
    static Color blueStar = new Color(0.1803f, 0.7137255f, 0.9254903f);
    static Color orgStar = new Color(0.9245283f, 0.554f, 0.364f);

    public class Star
    {
        public Color starColor;
        public Vector3 spawnpoint;
        public float speed;
        public GameObject starPrefab = Resources.Load<GameObject>("Prefabs/star");
        public Star(Vector3 aSpawnpoint, Color aStarColor, float aSpeed)
        {
            starColor = aStarColor;
            spawnpoint = aSpawnpoint;
            speed = aSpeed;
        }

        public void Create()
        {
            Instantiate(starPrefab, spawnpoint, Quaternion.identity);
        }
    }

    // Use this for initialization
    void Awake ()
    {
        cam = Camera.main;
        camera_point = cam.transform.position;
        StartCoroutine(Launch());
    }

    public void SpawnStar(Star spawn)
    {
        Vector3 spawnpoint = spawn;
        Instantiate(star, spawnpoint, Quaternion.identity);
        Debug.Log("Star created");
        starsOnScreen += 1;
    }

    // grabs a random spawnpoint at the bottom of the screen
    public Vector3 SelectSpawnPosition()
    {
        float x = Random.Range(0.0f, 1.0f);
        Vector3 spawn = cam.ViewportToWorldPoint(new Vector3(x, 0.0f, 0.0f));
        spawn = spawn + new Vector3(0f, 0f, 10f);
        return spawn;
    }


    public Color SelectColor()
    {
        int colorChangeIndicator = Random.Range(0, 100);
        float alpha = Random.Range(0.15f, 1.0f);
        if (colorChangeIndicator <= 5)
        {
            orgStar.a = alpha;
            return orgStar;
        }
        else
        {
            blueStar.a = alpha;
            return blueStar;
        }
    }

    public float SelectSpeed()
    {
        float minSpeed = 2.5f;
        float maxSpeed = 10f;
        return Random.Range(minSpeed, maxSpeed);
    }




    public IEnumerator Launch()
    {
        float time = 0.5f;
        int numOfStars = 5;

        while (true)
        {
            int i = 0;
            yield return new WaitForSeconds(time);

            while (i < numOfStars)
            {
                Star newStar = new Star(SelectSpawnPosition(), SelectColor(), SelectSpeed());
                newStar.Create();
            }
        }
    }
}
