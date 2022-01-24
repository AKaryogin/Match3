using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private Slider _progressBar;

    private void OnEnable()
    {
        _progressBar.value = 0;
        StartCoroutine(SetValueProgressBar());
        _board.Restart();
    }

    private IEnumerator SetValueProgressBar()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

        for(int i = 0; i < 4; i++)
        {
            _progressBar.value += 0.25f;
            yield return waitForSeconds;
        }
        
        gameObject.SetActive(false);
    }
}
