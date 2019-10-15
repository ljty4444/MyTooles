using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawHelper
{
    private List<int> levelList = new List<int>();

    private int level = 0;

    private int interval = 2;    //每次时间间隔 以秒为单位

    private UnityEngine.Random random = new UnityEngine.Random();

    public List<int> LevelList
    {
        get
        {
            return levelList;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public int Interval
    {
        get
        {
            return interval;
        }
    }

    public DrawHelper()
    {
        init();
    }

    private void init()
    {
        levelList.Add(600);
        levelList.Add(400);
        levelList.Add(240);
        levelList.Add(130);
        levelList.Add(70);
        levelList.Add(30);
        levelList.Add(15);
    }

    //public void drawCircle(Pen pen, PointF point, float radius)
    //{
    //    //x, y 这个点为 把整个圆（椭圆）包含在其中的最小正方形（矩形）的左上角那个点
    //    //DrawEllipse 函数中width和height指的是直径

    //    Rectangle mRect = new Rectangle((int)(point.X - radius), (int)(point.Y - radius), (int)(radius * 2), (int)(radius * 2));

    //    mGraphics.DrawEllipse(pen, mRect);
    //}

    /// <summary>
    /// 在圆心为point，半径为radius的圆内，产生一个半径为radius_inner的圆的圆心
    /// </summary>
    /// <param name="point">外圆圆心</param>
    /// <param name="radius_outer">外圆半径</param>
    /// <param name="radius_inner">内圆半径</param>
    /// <returns>内圆圆心</returns>
    public Vector2 PointOfRandom(Vector2 point, float radius_outer, float radius_inner)
    {
        int x = Random.Range((int)(point.x - (radius_outer - radius_inner)), (int)(point.x + (radius_outer - radius_inner)));
        int y = Random.Range((int)(point.y - (radius_outer - radius_inner)), (int)(point.y + (radius_outer - radius_inner)));

        while (!isInRegion(x - point.x, y - point.y, radius_outer - radius_inner))
        {
            x = Random.Range((int)(point.x - (radius_outer - radius_inner)), (int)(point.x + (radius_outer - radius_inner)));
            y = Random.Range((int)(point.y - (radius_outer - radius_inner)), (int)(point.y + (radius_outer - radius_inner)));
        }

        Vector2 p = new Vector2(x, y);
        return p;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x_off">与大圆圆心的x方向偏移量</param>
    /// <param name="y_off">与大圆圆心的y方向偏移量</param>
    /// <param name="distance">大圆与小圆半径的差</param>
    /// <returns>判断点是否在范围内</returns>
    public bool isInRegion(float x_off, float y_off, float distance)
    {
        if (x_off * x_off + y_off * y_off <= distance * distance)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 判断两个圆是否重合，或者是相内切
    /// </summary>
    /// <param name="p_outer">外圆圆心</param>
    /// <param name="r_outer">外圆半径</param>
    /// <param name="p_inner">内圆圆心</param>
    /// <param name="r_inner">内圆半径</param>
    /// <returns>是否相内切</returns>
    public bool isIntersect(Vector2 p_outer, float r_outer, Vector2 p_inner, float r_inner)
    {
        //判定条件：两圆心的距离 + r_inner = r_outer

        float distance = (float)Mathf.Sqrt((p_outer.x - p_inner.x) * (p_outer.x - p_inner.x) + (p_outer.y - p_inner.y) * (p_outer.y - p_inner.y));

        if (distance + r_inner >= r_outer)
        {
            return true;
        }
        return false;
    }
}
