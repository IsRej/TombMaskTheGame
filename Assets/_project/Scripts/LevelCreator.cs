using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public static LevelCreator Instance = null;

    public Texture2D[] _allMaps;

    public Texture2D _gameplayMap;

    public DetectAndSpawn[] _colorDetectors;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectLevel(int LevelNo)
    {
        _gameplayMap = _allMaps[LevelNo];
        CreateLevel();
    }

    private void CreateLevel()
    {
        for (int Width = 0; Width < _gameplayMap.width; Width++)
        {
            for (int Height = 0; Height < _gameplayMap.height; Height++)
            {
                DetectAndGenerateMap(Width,Height);
            }
        }
    }

    private void DetectAndGenerateMap(int Width, int Height)
    {
        Color pixelColor = _gameplayMap.GetPixel(Width, Height);

        //  Alpha Color 0 = Transparent
        if (pixelColor.a == 0)
        {
            return;
        }
        foreach (DetectAndSpawn ColorDetector in _colorDetectors)
        {
            if (ColorDetector._pixelColor.Equals(pixelColor))
            {
                Vector2 position = new Vector2(Width, Height);
                Instantiate(ColorDetector._prefabToSpawn, position, Quaternion.identity, transform);
            }
        }
    }
}
