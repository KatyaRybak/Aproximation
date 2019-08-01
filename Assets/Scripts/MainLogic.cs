using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainLogic : MonoBehaviour
{
    public GameObject pointPrefab;
    public int N,K;
    //N - количество заданных точек
 
    public float[] x, y;
    public static MainLogic instance;
    List<GameObject> pointsList;
    Vector2 offset;
    Vector2 spawnPoint;
    private void Awake()
    {
        if (FindObjectsOfType<MainLogic>().Length>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }

    }

    void Start()
    {
       
        InitialiseVariables();
    }

    void InitialiseVariables()
    {
        N = 5;
        x = new float[N];
        y = new float[N];
        pointsList = new List<GameObject>();
        spawnPoint = new Vector2(1, 0);
        offset = new Vector2(1, 0);
        for (int i = 0; i < 5; i++)
        {
            Vector2 presPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject newPoint = Instantiate(pointPrefab, spawnPoint, Quaternion.identity);
            pointsList.Add(newPoint);
            spawnPoint += offset;
        }

        SavePointsPositions();
    }


    private void SavePointsPositions()
    {
        int index = 0;
        x = new float[pointsList.Count];
        foreach (GameObject point in pointsList)
        {
            
            x[index] = point.transform.position.x;
            index++;
        }
    
    }

}
