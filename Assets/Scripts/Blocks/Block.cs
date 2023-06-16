using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class Block : MonoBehaviour
{
    public string type = ""; //taşın rengini 
    public string number = ""; //Taşın sayısını
    public bool isJoker = false;
    public float LerpValue = 5f;
    public bool draggable = true;
    private SpriteRenderer spriteRenderer;

    private GameManager _gameManager;

    private bool _isMovewithMouse = false; //test amaçlı

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManager = Object.FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        if (draggable)
            _isMovewithMouse = true;
    }

    private void OnMouseUp()
    {
        if (draggable)
        {
            _isMovewithMouse = false;
            this.transform.parent = FindNearstPlaceHolder().transform;
            _gameManager.ReorderBlocks();
        }
    }

    public GameObject FindNearstPlaceHolder()
    {
        List<(float, Transform)> distanceTransformList = new List<(float, Transform)>();
        foreach (var item in GameManager.allPlaceHolders)
        {
            float distance = Vector3.Distance(item.transform.position, this.transform.position);
            distanceTransformList.Add((distance,item.transform));
        }

        return distanceTransformList.OrderBy(x => x.Item1).FirstOrDefault().Item2.gameObject;
    }
    void Update()
    {
        if (_isMovewithMouse)
        {
            this.transform.position =
                Vector3.Lerp(this.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(5),
                    Time.deltaTime * LerpValue);
        }

        else //parent varlığını kontrol
        {
            if (transform.parent) // eğer parent vaarsa parnet konumuna git
            {
                this.transform.position =
                    Vector3.Lerp(this.transform.position, this.transform.parent.position, Time.deltaTime * LerpValue);
            }
        }
    }

    public void SetBlockSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        type = sprite.name.Split('_')[0];
        number = sprite.name.Split('_')[1];
    }
}