using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Langraange : MonoBehaviour
{
    int N;
    float[] x, y;
    Point[] startPoints;
    // Start is called before the first frame update
    void Start()
    {
        SetStartValues();
       
    }

    public void SetStartValues()
    {
        N = 5;
        x = new float[N];
        y = new float[N];
        GetComponent<LineRenderer>().positionCount = 0;

    }

    float CalculatePolinom(float pointX)
    {
        float pointY = 0.0f;
        for(int i = 0; i<N;i++)
        {
            float numerator = 1;
            float denominator = 1;
            for(int j=0; j<N; j++)
            {
                if (i != j)
                {
                    numerator *= pointX - x[j];
                    denominator *= x[i] - x[j];
                }

            }
            pointY += y[i] * numerator / denominator;
        }
        return pointY;
    }

    public void DrawGraf()
    {
        startPoints = FindObjectsOfType<Point>();
        for (int i = 0; i< startPoints.Length; i++)
        {
            x[i] = startPoints[startPoints.Length - 1 - i].transform.position.x;
            y[i] = startPoints[startPoints.Length - 1 - i].transform.position.y;
        }
 
        int index = 0;
        for(float i = 0; i <= startPoints.Length+1; i+=0.2f)
        {
             GetComponent<LineRenderer>().positionCount = index+1;
             GetComponent<LineRenderer>().SetPosition(index, new Vector2(i, CalculatePolinom(i)));
             index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
