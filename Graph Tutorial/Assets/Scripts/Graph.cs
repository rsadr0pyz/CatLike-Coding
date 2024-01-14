using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    private Transform pointPrefab;

    [SerializeField, Range(-20, 20)]
    private float sizeX = 1, sizeZ = 1, time = 0;

    [SerializeField, Range(-5, 5)]
    private float speed = 0;

    [SerializeField, Range(10, 300)]
    private int resolution = 25;
    private int _resolution = 25;

    [SerializeField, Range(0.01f, 2f)]
    private float pointScale = 1;
    private float _pointScale = 1;

    [SerializeField]
    private MathFunctionsLib.FunctionName functionName;


    private Transform[,] points;

    private void Awake()
    {
        _resolution = resolution;
        InstantiatePoints();
    }


    // Start is called before the first frame update
    private void Start()
    {
        GraphFunction();

    }

    private void Update()
    {
        time += speed * Time.deltaTime;
        UpdateObjects();
        GraphFunction(time);
    }

    private void GraphFunction(float time = 0){

        float stepSizeU = sizeX/(resolution-1); //-1 because one point is used in the startPos.
        float stepSizeV = sizeZ/(resolution-1);

        float startUPos = -sizeX/2;
        float startVPos = -sizeZ/2;


        MathFunctionsLib.MathFunction func = MathFunctionsLib.GetFunctionByName(functionName);

        for(int i = 0; i<resolution; i++)
        {
            for(int k = 0; k<resolution; k++)
            {
                float u = startUPos + k*stepSizeU;
                float v = startVPos + i*stepSizeV;

                points[i, k].localPosition = func(u, v, time);           
            }

        }
    }

    private void InstantiatePoints(){
        points = new Transform[resolution, resolution];

        for(int i = 0; i<resolution; i++)
        {
            for(int k = 0; k<resolution; k++)
            {
                points[i, k] = Instantiate(pointPrefab, transform);
                points[i, k].localScale = Vector3.one * pointScale;
            }

        }
    }

    private void UpdateObjects(){
        if(_resolution != resolution)
        {
            int diff = resolution - _resolution;
            Transform[,] newPoints = new Transform[resolution, resolution];

            if(diff < 0)
            {

                for(int i = 0; i < _resolution; i++)
                {
                    for(int k = 0; k < _resolution; k++)
                    {
                        if(k < resolution && i < resolution)
                        {
                            newPoints[i, k] = points[i, k];
                        }
                        else
                        {
                            Destroy(points[i,k].gameObject);
                        }
                    }

                }
            }
            else
            {
                for(int i = 0; i < resolution; i++)
                {
                    for(int k = 0; k < resolution; k++)
                    {
                        if(k < _resolution && i < _resolution)
                        {
                            newPoints[i, k] = points[i, k];
                        }
                        else
                        {
                            newPoints[i, k] = Instantiate(pointPrefab, transform);
                            newPoints[i, k].localScale *= pointScale;
                        }
                    }

                }
            }
            
            points = newPoints;
            _resolution = resolution;
        }

        if(_pointScale != pointScale)
        {
            foreach(Transform point in points){
                point.localScale = Vector3.one * pointScale;
            }

            _pointScale = pointScale;
        }
    }
}
