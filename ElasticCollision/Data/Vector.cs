using System;
public class Vector
{
    public double x { get; }
    public double y { get; }

    private Vector(double X, double Y)
    {
        x = X;
        y = Y;
    }

    public override bool Equals(object other)
    {
        var o = other as Vector;
        if (o == null)
        {
            return false;
        }
        else
        {
            return (x == o.x) && (y == o.y);
        }
    }
    public double magnitude() => Math.Sqrt(x * x + y * y);

    public static Vector vec(double x, double y) => new Vector(x, y);

    public static Vector operator +(Vector l, Vector r)
    {
        var x = l.x + r.x;
        var y = l.y + r.y;
        return vec(x, y);
    }

    public static Vector operator -(Vector v) => vec(-v.x, -v.y);

    public static Vector operator -(Vector l, Vector r) => l + (-r);

    public static double distance(Vector l, Vector r) => (l - r).magnitude();
}
