using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyManager : MonoBehaviour
{

    public Material texture;
    float gameSpeed;
   

    private void Awake()
    {
        texture.mainTextureOffset = new Vector2(texture.mainTextureOffset.x, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        gameSpeed= Time.deltaTime * 2f;

        float speedSky = 0.05f;
        texture.mainTextureOffset = texture.mainTextureOffset + new Vector2(0,-speedSky*gameSpeed);
    }

}
