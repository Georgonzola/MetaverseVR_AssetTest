using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteFollow : MonoBehaviour
{
    [SerializeField]
    private Transform track;
    private Transform[] beziers;
    private Vector3 objectPosition;

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
        coroutineActive = true;



        float size = GetBezierLength(beziers[bezierNum])/10;

        Vector3 control1 = beziers[bezierNum].GetChild(0).position;
        Vector3 control2 = beziers[bezierNum].GetChild(1).position;
        Vector3 control3 = beziers[bezierNum].GetChild(2).position;
        Vector3 control4 = beziers[bezierNum].GetChild(3).position;

        while (timer < 1)
        {
            timer += Time.deltaTime * moveSpeed/ size;

            objectPosition = Mathf.Pow(1 - timer, 3) * control1 + 3 * Mathf.Pow(1 - timer, 2) * timer * control2 + 3 * (1 - timer) * Mathf.Pow(timer, 2) * control3 + Mathf.Pow(timer, 3) * control4;

            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        timer = 0;
        currentBezier += 1;

        if (currentBezier > beziers.Length - 1)
        {
            currentBezier = 0;
        }

        coroutineActive = false; ;

    }

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
