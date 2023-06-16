using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject placeHoldersparent;
    public static List<GameObject> allPlaceHolders =new List<GameObject>();
    public GameObject blankBlockPrefab;
    public List<Sprite> Sprites;
    public List<Sprite> player1Blocks;
    public List<Sprite> player2Blocks;
    public List<Sprite> player3Blocks;
    public List<Sprite> player4Blocks;

    private void Start()
    {
        Sprites.AddRange(Sprites);
        StartGame();
        for (int i = 0; i < placeHoldersparent.transform.childCount; i++)
        {
            allPlaceHolders.Add(placeHoldersparent.transform.GetChild(i).gameObject);
        }
    }

    public void StartGame()
    {
        for (int i = 0; i < 14; i++)
        {
            player1Blocks.AddRange(new[] { Sprites[Random.Range(0, Sprites.Count)] });
            player2Blocks.AddRange(new[] { Sprites[Random.Range(0, Sprites.Count)] });
            player3Blocks.AddRange(new[] { Sprites[Random.Range(0, Sprites.Count)] });
            player4Blocks.AddRange(new[] { Sprites[Random.Range(0, Sprites.Count)] });
        }

        player1Blocks.AddRange(new[] { Sprites[Random.Range(0, Sprites.Count)] });
        for (int i = 0; i < player1Blocks.Count; i++)
        {
            Transform parent = placeHoldersparent.transform.GetChild(i); // holder daki herbir childa taşları yerleştiriyoruz
            GameObject block = Instantiate(blankBlockPrefab, Vector3.zero, quaternion.identity);
            block.GetComponent<Block>().SetBlockSprite(player1Blocks[i]);
            block.transform.parent = parent;
        }
    }
    public void ReorderBlocks()
    {
        bool _noOneMoreChild = true;
        while (_noOneMoreChild)
        {

            for (int i = 0; i < placeHoldersparent.transform.childCount; i++)
            {

                if (placeHoldersparent.transform.GetChild(i).childCount > 1 && placeHoldersparent.transform.GetChild(i).name != "placeHolder")
                {
                    _noOneMoreChild = true;
                    if (i != placeHoldersparent.transform.childCount - 1)
                        placeHoldersparent.transform.GetChild(i).GetChild(0).parent = placeHoldersparent.transform.GetChild(i + 1);
                    else
                        placeHoldersparent.transform.GetChild(i).GetChild(0).parent = placeHoldersparent.transform.GetChild(0);
                }
                else
                {

                    _noOneMoreChild = false;

                }
            }
        }
    }

   
}