using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _container;    
    [SerializeField] private Item[] _templates;

    public Item Spawn(int positionX, int positionY)
    {
        int randomIndex = Random.Range(0, _templates.Length);

        Item item = Instantiate(_templates[randomIndex], _container.transform);
        item.transform.position = new Vector2(positionX, positionY);
        item.SetPosition(positionX, positionY);       

        return item;
    }
}
