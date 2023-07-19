using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteFollow : MonoBehaviour
{
    [SerializeField]
    private Transform track;
    private Transform[] beziers;

    private float timer;
    public float moveSpeed;

    private int currentBezier;
    private bool coroutineActive;

    // Start is called before the first frame update
    void Start()
    {
        currentBezier = 0;
        timer = 0f;
        moveSpeed = 0.6f;
        coroutineActive = false;

        beziers = new Transform[track.childCount];
        //Gets all of the beziers in the track
        for(int i = 0; i < track.childCount; i++) {
            beziers[i] = track.GetChild(i).transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!coroutineActive)
        {
            StartCoroutine(FollowRoute(currentBezier));
        }
    }

    private IEnumerator FollowRoute(int bezierNum)
    {
        //prevents triggering the coroutine every frame
        coroutineActive = true;


        float size = GetBezierLength(beziers[bezierNum])/10;

        Vector3[] cPoints = new Vector3[4];


        //gets all of the control points in the bezier curve
        for(int i = 0; i< 4 ; i++)
            cPoints[i] = beziers[bezierNum].GetChild(i).position;
        


        while (timer < 1)
        {
            //Moves the follow object along the bezier curve with a speed relative to its size
            timer += Time.deltaTime * moveSpeed/ size;
            transform.position =  Mathf.Pow(1 - timer, 3) * cPoints[0] + 3 * Mathf.Pow(1 - timer, 2) * timer * cPoints[1] + 3 * (1 - timer) * Mathf.Pow(timer, 2) * cPoints[2] + Mathf.Pow(timer, 3) * cPoints[3];
            yield return new WaitForEndOfFrame();
        }

        timer = 0;
        currentBezier ++;

        if (currentBezier > beziers.Length - 1)
        {
            currentBezier = 0;
        }

        coroutineActive = false;

    }


    //Gets the approximate length of the bezier curve to modify the speed of the follow object
    private float GetBezierLength(Transform activeBezier)
    {
        Vector3 currentPosition;
        Vector3 previousPosition = activeBezier.GetChild(0).position;


        float bezierSize = 0f;
        for (float t = 0; t <= 1; t += 0.0002f)
        {
            currentPosition = Mathf.Pow(1 - t, 3) * activeBezier.GetChild(0).position + 3 * Mathf.Pow(1 - t, 2) * t * activeBezier.GetChild(1).position + 3 * (1 - t) * Mathf.Pow(t, 2) * activeBezier.GetChild(2).position + Mathf.Pow(t, 3) * activeBezier.GetChild(3).position;

            bezierSize += Vector3.Distance(previousPosition, currentPosition);
            previousPosition = currentPosition;
        }

        return bezierSize;
    }
}
