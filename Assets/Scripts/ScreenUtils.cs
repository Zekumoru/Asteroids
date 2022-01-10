using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenUtils
{
    #region Fields

    // cached for better performance
    static float screenTop;
    static float screenBottom;
    static float screenLeft;
    static float screenRight;
    static float screenMiddleWidth;
    static float screenMiddleHeight;
    static float screenZ;

    #endregion

    #region Properties

    public static float ScreenTop
    {
        get { return screenTop; }
    }

    public static float ScreenBottom
    {
        get { return screenBottom; }
    }

    public static float ScreenRight
    {
        get { return screenRight; }
    }

    public static float ScreenLeft
    {
        get { return screenLeft; }
    }

    public static float ScreenMiddleWidth
    {
        get { return screenMiddleWidth; }
    }

    public static float ScreenMiddleHeight
    {
        get { return screenMiddleHeight; }
    }

    public static float ScreenZ
    {
        get { return screenZ; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initialize screen utils' fields
    /// </summary>
    public static void Initialize()
    {
        // get screen upper right and lower left position, and center
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerLocation = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerLocation = new Vector3(
            Screen.width, Screen.height, screenZ);

        // convert to world location
        lowerLeftCornerLocation = 
            Camera.main.ScreenToWorldPoint(lowerLeftCornerLocation);
        upperRightCornerLocation = 
            Camera.main.ScreenToWorldPoint(upperRightCornerLocation);

        // assign class fields
        screenZ = lowerLeftCornerLocation.z;
        screenTop = upperRightCornerLocation.y;
        screenBottom = lowerLeftCornerLocation.y;
        screenLeft = lowerLeftCornerLocation.x;
        screenRight = upperRightCornerLocation.x;
        screenMiddleWidth = (screenLeft + screenRight) / 2;
        screenMiddleHeight = (screenBottom + screenTop) / 2;
    }

    #endregion
}
