using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ItemMover : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private MatchChecker _matchChecker;

    private Item _item;
    private Vector3 _swipeBegin;
    private Vector3 _swipeEnd;    
    private Vector3 _targetPosition;

    public event UnityAction<List<Item>, Board> Destroyed;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _swipeBegin = Input.mousePosition;
            _item = SelectItem(_swipeBegin);
        }

        if(Input.GetMouseButtonUp(0))
        {
            _swipeEnd = Input.mousePosition;

            if(_item != null)
            {
                SetTargetPosition(_swipeBegin, _swipeEnd);
                Move(_item.transform.position, _targetPosition);
            }
        }
        
    }

    private Item SelectItem(Vector3 screenPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(ray.origin, Vector3.forward);

        if(raycastHit2D.collider != null)
        {
            if(raycastHit2D.collider.TryGetComponent(out Item item))
            {
                return item;
            }
        }

        return null;
    }

    private void SetTargetPosition(Vector3 begin,Vector3 end)
    {
        if(Mathf.Abs(end.x - begin.x) > Mathf.Abs(end.y - begin.y))
        {
            if(begin.x > end.x)
                _targetPosition = new Vector2(_item.PositionX - 1, _item.PositionY);
            else
                _targetPosition = new Vector2(_item.PositionX + 1, _item.PositionY);
        }
        else
        {            
            if(begin.y > end.y)
                _targetPosition = new Vector2(_item.PositionX, _item.PositionY - 1);            
            else            
                _targetPosition = new Vector2(_item.PositionX, _item.PositionY + 1);
        }
    }

    private void Move(Vector3 currentPosition, Vector3 targetPosition)
    {
        if(targetPosition.x >= 0 && targetPosition.y >= 0 && targetPosition.x < _board.Column && targetPosition.y < _board.RowBoard)
        {
            SwipeItems(_item, _board.Items[(int)targetPosition.x, (int)targetPosition.y], currentPosition, targetPosition);
            StartCoroutine(Match(currentPosition, targetPosition));
        }
    }

    private void SwipeItems(Item originalItem, Item targetItem, Vector3 currentPosition, Vector3 targetPosition)
    {
        targetItem.transform.DOMove(currentPosition, 0.5f);
        targetItem.SetPosition((int)currentPosition.x, (int)currentPosition.y);

        originalItem.transform.DOMove(targetPosition, 0.5f);
        originalItem.SetPosition((int)targetPosition.x, (int)targetPosition.y);

        _board.Items[(int)currentPosition.x, (int)currentPosition.y] = targetItem;
        _board.Items[(int)targetPosition.x, (int)targetPosition.y] = originalItem;
    }

    private void SwipeItemToEmpty(Item originalItem, Vector3 currentPosition, Vector3 targetPosition)
    {
        originalItem.transform.DOMove(targetPosition, 0.5f);
        originalItem.SetPosition((int)targetPosition.x, (int)targetPosition.y);

        _board.Items[(int)currentPosition.x, (int)currentPosition.y] = null;
        _board.Items[(int)targetPosition.x, (int)targetPosition.y] = originalItem;
    }

    private IEnumerator Match(Vector3 currentPosition, Vector3 targetPosition)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

        yield return waitForSeconds;

        List<Item> items = _matchChecker.GetAllMatch(_item);
        items.Add(_item);
        

        if(items.Count >= 3)
        {            
            Destroyed?.Invoke(items, _board);            
            SwipeEmptyCells(items, _board);
            _board.FillEmptyCells();
        }
        else
        {
            SwipeItems(_item, _board.Items[(int)currentPosition.x, (int)currentPosition.y], targetPosition, currentPosition);
        }
    }

    private void SwipeEmptyCells(List<Item> items, Board board)
    {
        foreach(var item in items)
        {
            if(board.Items[item.PositionX, item.PositionY] == null)
            {
                int itemPositionY = item.PositionY;
                for(int i = itemPositionY + 1; i < board.Row; i++)
                {
                    if(board.Items[item.PositionX, i] != null)
                    {
                        SwipeItemToEmpty(board.Items[item.PositionX, i], new Vector3(item.PositionX, i), new Vector3(item.PositionX, itemPositionY));
                        itemPositionY++;
                    }
                }
            }
        }
    }
}
