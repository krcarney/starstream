using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    Vector3 startPos;
    Transform tf;
    Vector3 endOfScreen;
    private Color starColor;
    private Color trailColor;
    private SpriteRenderer spriteR;
    Rigidbody2D rb;

    // Use this for initialization
    void Awake ()
    {
        // wake up and create an associated star object
        StarSpawner.Star star = new StarSpawner.Star();
        tf = GetComponent<Transform>();

        //set the current position to object's spawnpoint
        tf.position = star.spawnpoint;

        // set sprite's color to object's color
        this.GetComponent<SpriteRenderer>().color = star.starColor;

        // grab velocity from the object
        this.GetComponent<Rigidbody2D>().velocity = star.speed;
        
        // set colors for beginning and end of trail
        trailColor = star.starColor;
        trailColor.a -= 0.1f;
        this.GetComponent<TrailRenderer>().startColor = trailColor;
        trailColor.a -= 0.15f;
        this.GetComponent<TrailRenderer>().endColor = trailColor;
        
        //establish travel time of star
        float travelTime = Random.Range(6.0f, 15f);

        //find the start position
        startPos = new Vector3(tf.position.x, tf.position.y, tf.position.z);

        // find the end of the screen
        endOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 1, Camera.main.nearClipPlane));
        //travel = MoveToPosition(tf,new Vector3(startPos.x, endOfScreen.y + 3f, startPos.z), travelTime);
        //StartCoroutine(travel);
    }

    private void Update()
    {
        // destroy the object at the end of the screen
        if (tf.position.y >= 8f)
        {
            Destroy(this.gameObject);
        }
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
}
