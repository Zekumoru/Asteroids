using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    // Awake is called on instantiated
    private void Awake()
    {
        ScreenUtils.Initialize();
    }
}
