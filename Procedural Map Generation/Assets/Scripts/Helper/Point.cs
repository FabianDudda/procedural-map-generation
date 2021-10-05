using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Point
{

    public int X { get; set; }  // Property to get & set the int X
    public int Y { get; set; }  // Property to get & set the int Y

    public Point(int x, int y)
    {
        this.X = x;             // X = x
        this.Y = y;             // Y = y
    }



    //-------------------------- For AStarPathfinding --------------------------//
    public static bool operator ==(Point first, Point second)
    {

        return first.X == second.X && first.Y == second.Y;
    }

    public static bool operator !=(Point first, Point second)
    {

        return first.X != second.X || first.Y != second.Y;
    }

    public static Point operator -(Point x, Point y)
    {

        return new Point(x.X - y.X, x.Y - y.Y);
    }

}
