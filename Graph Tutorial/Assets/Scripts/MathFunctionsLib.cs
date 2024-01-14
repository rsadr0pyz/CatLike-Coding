
using UnityEngine;
using static UnityEngine.Mathf;

public static class MathFunctionsLib
{


    public enum FunctionName{
        Plane,
        Wave,
        MultiWave,
        Ripple, 
        Sphere,
        OddSphere,
        OddSphereHorizontal,
        OddSphereVertical,
        Torus,
        WeirdTorus
    }
    public delegate Vector3 MathFunction(float x, float z, float t);

    private static MathFunction[] functions = {Plane, Wave, MultiWave, Ripple, Sphere, OddSphere, OddSphereHorizontal, OddSphereVertical, Torus, WeirdTorus};

    public static MathFunction GetFunctionByName(FunctionName name){
        return functions[(int)name];
    }


    //Functions
    public static Vector3 Wave(float u, float v, float t)
    {
        Vector3 vec;
        vec.x = u;
        vec.y = Sin(u + v - t);
        vec.z = v;

        return vec;
    }

    public static Vector3 MultiWave(float u, float v, float t)
    {
        Vector3 vec;
        vec.x = u;

        vec.y = Sin(u - t);
        vec.y += Sin(2*v - 2*t)/2;
        vec.y += Sin(u + v - t/4);

        float globalMax = 2.5f;
        vec.y = vec.y/globalMax;

        vec.z = v;

        return vec;
    }

    public static Vector3 Ripple(float u, float v, float t = 0)
    {
        Vector3 vec;

        vec.x = u;

        float distanceFromOrigin = Sqrt(u*u + v*v);
        vec.y = Sin(4*distanceFromOrigin - 4*t)/(1 + distanceFromOrigin);

        vec.z = v;

        return vec;
    }

    public static Vector3 Sphere(float u, float v, float t = 0){
        Vector3 vec;

        float sphere_radius = Sin(t);
        float circle_radius = sphere_radius * Cos(PI * v);

        vec.x = circle_radius * Cos(PI * u * 2);
        vec.z = circle_radius * Sin(PI * u * 2);

        vec.y = sphere_radius * Sin(PI * v);

        return vec;
    }


    public static Vector3 OddSphere(float u, float v, float t = 0){
        Vector3 vec;

        float sphere_radius = 1;
        float circle_radius = sphere_radius*Cos(PI * v);

        circle_radius = 0.85f*circle_radius + 0.15f*circle_radius*Sin(16*PI* (u+v) + t) ;

        vec.x = circle_radius * Cos(PI * u * 2);
        vec.z = circle_radius * Sin(PI * u * 2);

        vec.y = sphere_radius * Sin(PI * v);

        return vec;
    }

    public static Vector3 OddSphereHorizontal(float u, float v, float t = 0){
        Vector3 vec;

        float sphere_radius = 1;
        float circle_radius = sphere_radius*Cos(PI * v);

        circle_radius = 0.85f*circle_radius + 0.15f*circle_radius*Sin(16*PI*u + t) ;

        vec.x = circle_radius * Cos(PI * u * 2);
        vec.z = circle_radius * Sin(PI * u * 2);

        vec.y = sphere_radius * Sin(PI * v);

        return vec;
    }

    public static Vector3 OddSphereVertical(float u, float v, float t = 0){
        Vector3 vec;

        float sphere_radius = 1;
        float circle_radius = sphere_radius*Cos(PI * v);

        circle_radius = 0.85f*circle_radius + 0.15f*circle_radius*Sin(16*PI*v + t) ;

        vec.x = circle_radius * Cos(PI * u * 2);
        vec.z = circle_radius * Sin(PI * u * 2);

        vec.y = sphere_radius * Sin(PI * v);

        return vec;
    }

    public static Vector3 Torus(float u, float v, float t = 0){
        Vector3 vec;

        float bigR = 3, smallR = 1;

        vec.x = bigR * Cos(PI * u) + Cos(PI * u) * smallR * Cos(PI * v);
        vec.z = bigR * Sin(PI * u) + Sin(PI * u) * smallR * Cos(PI * v);

        vec.y = smallR * Sin(PI * v);
        return vec;
    }

    public static Vector3 WeirdTorus(float u, float v, float t = 0){
        Vector3 vec;

        float bigR = 7/10f + Sin(PI * (6*u + t/2))/10, smallR = 3/20f + Sin(PI * (8*u + 4*v + 2*t))/20;

        vec.x = bigR * Cos(PI * u) + Cos(PI * u) * smallR * Cos(PI * v);
        vec.z = bigR * Sin(PI * u) + Sin(PI * u) * smallR * Cos(PI * v);

        vec.y = smallR * Sin(PI * v);
        return vec;
    }

    public static Vector3 Plane(float u, float v, float t = 0){
        return new Vector3(u, 0, v);
    }

}
