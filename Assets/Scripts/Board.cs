using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Spawner))]
public class Board : MonoBehaviour
{
    [SerializeField] private int _row;
    [SerializeField] private int _column;
    [SerializeField] private int _rowBoard;
    [SerializeField] private Score _score;

    private Spawner _spawner;
    private Item[,] _items;

    public event UnityAction<Item[,],int,int> Restarted;

    public int Row => _row;
    public int RowBoard => _rowBoard;
    public int Column => _column;    
    public Item[,] Items => _items;

    private void Start()
    {
        _spawner = GetComponent<Spawner>();
        _items = new Item[_column, _row];

        Initialize();
    }

    private void Initialize()
    {
        for(int i = 0; i < _row; i++)
        {
            for(int j = 0; j < _column; j++)
            {
                _items[j,i] = _spawner.Spawn(j,i);                
            }
        }
    }

    public void FillEmptyCells()
    {
        for(int i = _row - 1; i > _rowBoard; i--)
        {
            for(int j = 0; j < _column; j++)
            {
                if(_items[j, i] == null)
                    _items[j, i] = _spawner.Spawn(j, i);
            }
        }
    }

    public void Restart()
    {
        Restarted?.Invoke(_items, _row, _column);
        _score.Restart();
        Initialize();
    }
}
