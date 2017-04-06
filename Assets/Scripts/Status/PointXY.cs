using System;
using System.Collections.Generic;

[Serializable]
public class PointXY
{
    public int x;
    public int y;
    public PointXY() { }
    public PointXY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public bool inRange(PointXY point, int range)
    {
        if (dist(point) <= range) return true;
        return false;
    }

    public int dist(PointXY point)
    {
        var diffX = Math.Abs(x - point.x);
        var diffY = Math.Abs(y - point.y);
        return diffX + diffY;
    }
    public static IEnumerable<PointXY> getRange(PointXY point, int range)
    {
        yield return point;
        var tp = new PointXY();
        for (int dx = -range; dx <= range; ++dx)
        {
            tp.x = point.y + dx;
            int distY = range - Math.Abs(dx);
            for (int dy = -distY; dy <= distY; ++dy)
            {
                tp.y = point.x + dy;
                yield return tp;
            }
        }
    }
    public static bool operator== (PointXY pt1, PointXY pt2)
    {
        return pt1.x == pt2.x && pt1.y == pt2.y;
    }
    public static bool operator!= (PointXY pt1, PointXY pt2)
    {
        return !(pt1 == pt2);
    }
    public override bool Equals(object obj)
    {
        if(obj is PointXY)
        {
            return (obj as PointXY) == this;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override string ToString()
    {
        return "{x:" + x + ",y:" + y + "}";
    }
}
