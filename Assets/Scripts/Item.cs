using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _coast;
    [SerializeField]private int _positionX;
    [SerializeField]private int _positionY;

    public string Label => _label;
    public int Coast => _coast;
    public int PositionX => _positionX;
    public int PositionY => _positionY;

    public void SetPosition(int positionX, int positionY)
    {
        _positionX = positionX;
        _positionY = positionY;        
    }

    public Vector3 GetPosition()
    {
        return new Vector3(_positionX, PositionY);
    }
}
