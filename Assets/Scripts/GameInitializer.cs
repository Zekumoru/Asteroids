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

    // Start is called before the first frame update
    private void Start()
    {
        Instantiate(Resources.Load<GameObject>(@"Prefabs/Ship"), 
            new Vector3(ScreenUtils.ScreenMiddleWidth, 
            ScreenUtils.ScreenMiddleHeight, ScreenUtils.ScreenZ), 
            Quaternion.identity);
    }
}
