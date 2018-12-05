using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    Vector3 startPos;
    Vector3 distance;
    Transform tf;
    Vector3 endOfScreen;
    private Color starColor;
    private Color trailColor;
    private IEnumerator travel;
    private SpriteRenderer spriteR;
    private string bigSpriteName = "star_big";



    // Use this for initialization
    void Awake ()
    {
        tf = GetComponent<Transform>();
        int colorChangeIndicator = Random.Range(0, 100);
        int spriteSelector = Random.Range(0, 100);
        spriteR = this.GetComponent<SpriteRenderer>();
        var bigSprite = Resources.Load<Sprite>("Sprites/star_big");

        if (spriteSelector <= 4)
        {
            Debug.Log("Making a big star");
            Vector2 spriteSize = new Vector2(spriteR.bounds.size.x, spriteR.bounds.size.y);
            Vector3 spriteLocation = spriteR.transform.position;
            Camera.main.GetComponent<StarSpawner>().SpawnStar(new Vector3 (spriteLocation.x + spriteSize.x, spriteLocation.y, spriteLocation.z));

            //this.GetComponent<SpriteRenderer>().sprite = bigSprite;
            //var trailR = this.GetComponent<TrailRenderer>();
            //trailR.startWidth = trailR.startWidth * 2.5f;
            //trailR.endWidth = trailR.startWidth;
        }

        starColor = this.GetComponent<SpriteRenderer>().color;
        if (colorChangeIndicator <= 5)
        {
            starColor = new Color(0.9245283f, 0.554f, 0.364f);
        }
        starColor.a = Random.Range(0.15f, 1.0f);
        this.GetComponent<SpriteRenderer>().color = starColor;

        // set colors for beginning and end of trail
        trailColor = this.GetComponent<TrailRenderer>().startColor;
        trailColor = starColor;
        trailColor.a -= 0.1f;
        float tailDifference = 0.15f;
        this.GetComponent<TrailRenderer>().startColor = trailColor;
        this.GetComponent<TrailRenderer>().endColor = new Color(trailColor.r, trailColor.g, trailColor.b, trailColor.a - tailDifference);
        
        //establish travel time of star
        float travelTime = Random.Range(6.0f, 15f);

        //find the start position
        startPos = new Vector3(tf.position.x, tf.position.y, tf.position.z);

        // find the end of the screen
        endOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 1, Camera.main.nearClipPlane));
        travel = MoveToPosition(tf,new Vector3(startPos.x, endOfScreen.y + 3f, startPos.z), travelTime);
        StartCoroutine(travel);
    }

    private void Update()
    {
        // destroy the object at the end of the screen
        if (!GetComponent<TrailRenderer>().isVisible)
        {
            StarSpawner.starsOnScreen -= 1;
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
