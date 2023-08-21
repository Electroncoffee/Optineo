//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GridManager : MonoBehaviour
//{
//    [SerializeField] private int rows = 10;
//    [SerializeField] private int cols = 10;
//    [SerializeField] private float tilesize = 1;


//    // Start is called before the first frame update
//    void Start()
//    {
//        GenerateGrid();
//    }
//    private void GenerateGrid()
//    {
//        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("FHTile"));
//        for (int row = 0; row < rows; row++)
//        {
//            for (int col = 0; col < cols; col++)
//            {
//                GameObject tile = (GameObject)Instantiate(referenceTile, transform);

//                float posX = col * tilesize;
//                float posY = row * -tilesize;

//                tile.transform.position = new Vector2(posX, posY);
//            }
//        }

//        Destroy(referenceTile);

//        float gridW = cols * tilesize;
//        float gridH = rows * tilesize;

//        transform.position = new Vector2(-gridW / 2 + tilesize / 2, gridH / 2 - tilesize / 2);

//    }

//}
