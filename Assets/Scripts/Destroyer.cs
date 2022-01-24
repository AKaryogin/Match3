using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private ItemMover _itemMover;
    [SerializeField] private Score _score;

    private void OnEnable()
    {
        _board.Restarted += OnDestroyItems;
        _itemMover.Destroyed += OnDestroyItems;
    }

    private void OnDisable()
    {
        _board.Restarted -= OnDestroyItems;
        _itemMover.Destroyed -= OnDestroyItems;
    }

    private void OnDestroyItems(List<Item> items, Board board)
    {        
        foreach(var item in items)
        {
            _score.AddScore(item.Coast);
            board.Items[item.PositionX, item.PositionY] = null;
            Destroy(item.gameObject);            
        }
    }

    private void OnDestroyItems(Item[,] items, int row, int column)
    {
        for(int i = 0; i < row; i++)
        {
            for(int j = 0; j < column; j++)
            {
                Destroy(items[j, i].gameObject);
                items[j, i] = null;
            }
        }
    }
}
