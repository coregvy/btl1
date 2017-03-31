
using System;

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
}
