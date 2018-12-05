using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarSpawner : MonoBehaviour
{
    public GameObject star;
    public static int starsOnScreen;
    Vector3 camera_point;

    public class Star
    {
        public Color starColor;
        public Vector3 spawnpoint;
        public Vector2 speed;
        public GameObject starPrefab = Resources.Load<GameObject>("Prefabs/star");

        public Star()
        {
            starColor = SelectColor();
            spawnpoint = SelectSpawnPosition();
            speed = SelectSpeed();
        }

        public Star(Vector3 aSpawnpoint, Vector2 aSpeed)
        {
            spawnpoint = aSpawnpoint;
            speed = aSpeed;
        }

        public Star(Vector3 aSpawnpoint, Vector2 aSpeed, Color aStarColor)
        {
            starColor = aStarColor;
            spawnpoint = aSpawnpoint;
            speed = aSpeed;
        }

        // grabs a random spawnpoint at the bottom of the screen
        public Vector3 SelectSpawnPosition()
        {
            float x = Random.Range(0.0f, 1.0f);
            Vector3 spawn = Camera.main.ViewportToWorldPoint(new Vector3(x, 0.0f, 0.0f));
            spawn += new Vector3(0f, 0f, 10f);
            return spawn;
        }

        // grab either a blue or orange color for the star
        public Color SelectColor()
        {
            Color blueStar = new Color(0.1803f, 0.7137255f, 0.9254903f);
            Color orgStar = new Color(0.9245283f, 0.554f, 0.364f); ;

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

        // select a speed for a star
        public Vector2 SelectSpeed()
        {
            float minSpeed = 2.5f;
            float maxSpeed = 10f;
            float speed = Random.Range(minSpeed, maxSpeed);
            return new Vector2(0f, speed);
        }
    }

    // Use this for initialization
    void Awake ()
    {
        Camera cam = Camera.main;
        camera_point = cam.transform.position;
        StartCoroutine(Launch());
    }

    //public void SpawnStar(Star star)
    //{
    //    Vector3 spawnpoint = spawn;
    //    Instantiate(star, spawnpoint, Quaternion.identity);
    //    Debug.Log("Star created");
    //    starsOnScreen += 1;
    //}

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
                Star newStar = new Star();
                Instantiate(newStar.starPrefab, newStar.spawnpoint, Quaternion.identity);
            }
        }
    }
}
