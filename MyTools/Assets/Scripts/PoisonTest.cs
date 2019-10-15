using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTest : MonoBehaviour
{
    public GameObject poison;
    public GameObject safe;

    // Start is called before the first frame update
    void Start()
    {
        init_bg();
        timer2_Tick();
        isStart = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(!isStart)
        {
            return;
        }
        if(!timer2enable)
        {
            timer1_Tick();
        }else
        {
            timer2_Tick();
        }
        
    }

    //理想点，但是用于画图的话还要再减去圆的r
    private Vector2 mPoint_outer;

    private Vector2 mPoint_inner;

    private float mRadius_outer = 0;

    private float mRadius_inner = 0;

    private DrawHelper mDrawHelper = null;

    public bool isStart = false;

    private bool timer1enable = false;
    private bool timer2enable = false;

    private void init_bg()
    {


        mPoint_outer = Vector2.zero;

        mDrawHelper = new DrawHelper();

        mRadius_outer = mDrawHelper.LevelList[0];
    }


    private void timer1_Tick()
    {

        mRadius_outer-=(float)10/30;


        if (!mDrawHelper.isIntersect(mPoint_outer, mRadius_outer, mPoint_inner, mRadius_inner))
        {
            poison.transform.localScale = new Vector3(mRadius_outer, mRadius_outer, 1);
            poison.transform.localPosition = new Vector3(mPoint_outer.x/60, mPoint_outer.y/60, 0);
        }
        else
        {
            if (mRadius_outer != mRadius_inner)  //外圈和内圈圆心重合,半径相同
            {
                // 图三过程
                // k = y/x
                // y = kx
                // x^2+y^y = 1
                // x^2 = 1/(k^2+1)
                float k = (mPoint_outer.y - mPoint_inner.y) / (mPoint_outer.x - mPoint_inner.x);

                float x_off = 1 * (float)Mathf.Sqrt(1 / (k * k + 1));

                // k<0  x+x_off
                mPoint_outer.x += 1 * (mPoint_outer.x < mPoint_inner.x ? 1 : -1) * x_off;
                mPoint_outer.y = k * (mPoint_outer.x - mPoint_inner.x) + mPoint_inner.y;

                //reCalcPoint_outer(mPoint_outer, mPoint_inner);

                poison.transform.localScale = new Vector3(mRadius_outer, mRadius_outer, 1);
                poison.transform.localPosition = new Vector3(mPoint_outer.x/60, mPoint_outer.y/60, 0);
            }
            else
            {
                poison.transform.localScale = new Vector3(mRadius_outer, mRadius_outer, 1);
                poison.transform.localPosition = new Vector3(mPoint_outer.x/60, mPoint_outer.y/60, 0);
                timer2enable = true;
              
            }
        }
    }

    /// <summary>
    /// 后台时间表，用于毒圈（外圈）和内圈重合后，暂停时间以及生成下一个内圈圆心和内圈半径
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void timer2_Tick()
    {
        if (mDrawHelper.Level < mDrawHelper.LevelList.Count - 1 && mRadius_outer == mDrawHelper.LevelList[mDrawHelper.Level])
        {
            mDrawHelper.Level++;
            mRadius_inner = mDrawHelper.LevelList[mDrawHelper.Level];
            mPoint_inner = mDrawHelper.PointOfRandom(mPoint_outer, mRadius_outer, mRadius_inner);

            safe.transform.localScale = new Vector3(mRadius_inner, mRadius_inner, 1);
            safe.transform.localPosition = new Vector3(mPoint_inner.x/60, mPoint_inner.y/60, 0);
        }
        else
        {
            timer1enable = false;
            timer2enable = false;
        }
    }

}
