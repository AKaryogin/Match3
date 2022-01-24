using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker : MonoBehaviour
{
    [SerializeField] private Board _board;

    private List<Item> _items = new List<Item>();

    public List<Item> GetAllMatch(Item originalItem)
    {
        _items.Clear();

        SearchMatchUp(originalItem);
        SearchMatchDown(originalItem);
        SearchMatchLeft(originalItem);
        SearchMatchRight(originalItem);

        return _items;
    }

    private void SearchMatchUp(Item originalItem)
    {
        List<Item> items = SearchMatch(originalItem, Vector3.up);

        if(items.Count >= 2)
        {
            foreach(var item in items)
            {
                _items.Add(item);
            }
        }else if(items.Count == 1 && Check(originalItem, _board.Items[(int)(originalItem.PositionX), (int)(originalItem.PositionY - 1)]))
        {
            _items.Add(items[0]);
        }
    }

    private void SearchMatchDown(Item originalItem)
    {
        List<Item> items = SearchMatch(originalItem, Vector3.down);

        if(items.Count >= 2)
        {
            foreach(var item in items)
            {
                _items.Add(item);
            }
        }
        else if(items.Count == 1 && Check(originalItem, _board.Items[(int)(originalItem.PositionX), (int)(originalItem.PositionY + 1)]))
        {
            _items.Add(items[0]);
        }
    }

    private void SearchMatchLeft(Item originalItem)
    {
        List<Item> items = SearchMatch(originalItem, Vector3.left);

        if(items.Count >= 2)
        {
            foreach(var item in items)
            {
                _items.Add(item);
            }
        }
        else if(items.Count == 1 && Check(originalItem, _board.Items[(int)(originalItem.PositionX + 1), (int)(originalItem.PositionY)]))
        {
            _items.Add(items[0]);
        }
    }

    private void SearchMatchRight(Item originalItem)
    {
        List<Item> items = SearchMatch(originalItem, Vector3.right);

        if(items.Count >= 2)
        {
            foreach(var item in items)
            {
                _items.Add(item);
            }
        }
        else if(items.Count == 1 && Check(originalItem, _board.Items[(int)(originalItem.PositionX - 1), (int)(originalItem.PositionY)]))
        {
            _items.Add(items[0]);
        }
    }

    private List<Item> SearchMatch(Item originalItem, Vector3 direction)
    {
        List<Item> items = new List<Item>();

        if((int)(originalItem.PositionX + direction.x) >= 0 && (int)(originalItem.PositionY + direction.y) >= 0 
            && (int)(originalItem.PositionX + direction.x) < _board.Column && (int)(originalItem.PositionY + direction.y) < _board.RowBoard)
        {            
            Item checkItem = _board.Items[(int)(originalItem.PositionX + direction.x), (int)(originalItem.PositionY + direction.y)];
            Item lastCheckItem;

            if(Check(originalItem, checkItem))
            {
                items.Add(checkItem);
                while(true)
                {
                    lastCheckItem = checkItem;
                    if((int)(lastCheckItem.PositionX + direction.x) >= 0 && (int)(lastCheckItem.PositionY + direction.y) >= 0
                        && (int)(lastCheckItem.PositionX + direction.x) < _board.Column && (int)(lastCheckItem.PositionY + direction.y) < _board.Row)
                    {
                        checkItem = _board.Items[(int)(lastCheckItem.PositionX + direction.x), (int)(lastCheckItem.PositionY + direction.y)];

                        if(Check(lastCheckItem, checkItem))
                            items.Add(checkItem);
                        else
                            return items;

                    }
                    else
                        return items;
                }
            }
        }

        return items;
    }
    private bool Check(Item originalItem, Item checkItem)
    {
        return originalItem.Label == checkItem.Label;
    }
}
