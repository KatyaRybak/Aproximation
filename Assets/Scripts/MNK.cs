using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNK : MonoBehaviour
{

    int K,N;
    //K - степень полинома

    float[][] sums;
    float[] a, b, x, y;

    void readmatrix()
    {
        int i = 0, j = 0, k = 0;

        //init square sums matrix
        for (i = 0; i < K + 1; i++)
        {
            for (j = 0; j < K + 1; j++)
            {
                sums[i][j] = 0;
                for (k = 0; k < N; k++)
                {
                    sums[i][j] += Mathf.Pow(x[k], i + j);
                }
            }
        }
        //init free coefficients column
        for (i = 0; i < K + 1; i++)
        {
            for (k = 0; k < N; k++)
            {
                b[i] += Mathf.Pow(x[k], i) * y[k];
            }
        }
    }


    public void ApproximateFunction()
    {
        SetStartValues();
        GetPoints();
        Point[] points = FindObjectsOfType<Point>();
        N = points.Length;
        for (int i = 0; i < N; i++)
        {
            y[i] = points[i].transform.position.y;
        }

        readmatrix();
        diagonal();

        for (int k = 0; k < K + 1; k++)
        {
            for (int i = k + 1; i < K + 1; i++)
            {
                if (sums[k][k] == 0)
                {
                    Debug.LogError("Solution is not exist!");
                    return;
                }
                float M = sums[i][k] / sums[k][k];
                for (int j = k; j < K + 1; j++)
                {
                    sums[i][j] -= M * sums[k][j];
                }
                b[i] -= M * b[k];
            }
        }

        for (int i = K ; i >= 0; i--)
        {
            float s = 0;
            for (int j = i; j < K + 1; j++)
            {
                s = s + sums[i][j] * a[j];
            }
            a[i] = (b[i] - s) / sums[i][i];
        }

        DrawFunction();
    }

    private void DrawFunction()
    {
        LineRenderer line = GetComponent<LineRenderer>();

        line.positionCount =N*2+3;
        int index = 0;
        for (float i = 0; i <= N+1; i+=0.5f)
        {        
            float y = 0;
            for (int k = 0; k < K + 1; k++)
            {
                y += a[k] * Mathf.Pow(i, k);
            }

            line.SetPosition(index, new Vector2(i, y));
            index++;
        }
    }

    void diagonal()
    {
        int i, j, k;
        float temp = 0;
        for (i = 0; i < K + 1; i++)
        {
            if (sums[i][i] == 0)
            {
                for (j = 0; j < K + 1; j++)
                {
                    if (j == i) continue;
                    if (sums[j][i] != 0 && sums[i][j] != 0)
                    {
                        for (k = 0; k < K + 1; k++)
                        {
                            temp = sums[j][k];
                            sums[j][k] = sums[i][k];
                            sums[i][k] = temp;
                        }
                        temp = b[j];
                        b[j] = b[i];
                        b[i] = temp;
                        break;
                    }
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetStartValues();

    }

    public void SetStartValues()
    {

        K = 3;
        N = 5;
        a = new float[K + 1];
        b = new float[K + 1];
        sums = new float[K + 1][];
        for (int i = 0; i < K + 1; i++)
        {
            sums[i] = new float[K + 1];
        }
        x = new float[N];
        y = new float[N];
    }

    

    void GetPoints()
    {
        x = MainLogic.instance.x;
    }
    
}
