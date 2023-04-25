using UnityEngine;

/// <summary>
/// point2  point3
/// point4  point1
/// </summary>
public class MyRectangle
{
    public Vector3 Point1 { get; private set; }
    public Vector3 Point2 { get; private set; }
    public Vector3 Point3 { get; private set; }
    public Vector3 Point4 { get; private set; }

    public MyRectangle(Vector3 point1, Vector3 point2)
    {
        Point1 = point1;
        Point2 = point2;
        Point3 = new Vector3(point1.x, point1.y, point2.z);
        Point4 = new Vector3(point2.x, point2.y, point1.z);
    }
}