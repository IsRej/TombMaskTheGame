using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class GameManager : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject _levelObjectsParent;
    [Header("UI Panel")]
    public GameObject _uIMenu;
    public GameObject _uiWin;
    public GameObject _uILose;
    [Header("Win Buttons")]
    public Button _retryBtnWin;
    public Button _nextBtnWin;
    public Button _menuBtnWin;
    [Header("Lose Buttons")]
    public Button _retryBtnLose;
    public Button _menuBtnLose;
    [Header("Integer Values")]
    public int _levelNo;

    private void OnEnable()
    {
        ActionEventHandler.OnPlayerWin += CallGameWin;
        ActionEventHandler.OnPlayerDie += CallGameLose;
    }

    private void OnDisable()
    {
        ActionEventHandler.OnPlayerWin -= CallGameWin;
        ActionEventHandler.OnPlayerDie -= CallGameLose;
    }

    private void Start()
    {
        _retryBtnWin.onClick.AddListener(OnRetryCalled);
        _retryBtnLose.onClick.AddListener(OnRetryCalled);
        _menuBtnWin.onClick.AddListener(OnMenuCalled);
        _menuBtnLose.onClick.AddListener(OnMenuCalled);
        _nextBtnWin.onClick.AddListener(OnNextLevelCalled);
    }

    public void StartGame(int LevelNo)
    {
        _uIMenu.SetActive(false);
        _levelNo = LevelNo;
        LevelCreator.Instance.SelectLevel(_levelNo);
    }

    public void CallGameLose()
    {
        _uILose.SetActive(true);
    }

    public void CallGameWin()
    {
        _uiWin.SetActive(true);
        if (_levelNo == 4)
        {
            _nextBtnWin.interactable = false;
        }
    }

    public void OnRetryCalled()
    {
        ClearLastLevel();
        LevelCreator.Instance.SelectLevel(_levelNo);
        _uiWin.SetActive(false);
        _uILose.SetActive(false);
    }

    public void OnMenuCalled()
    {
        _uiWin.SetActive(false);
        _uILose.SetActive(false);
        _uIMenu.SetActive(true);
    }

    public void OnNextLevelCalled()
    {
        ClearLastLevel();
        LevelCreator.Instance.SelectLevel(_levelNo + 1);
        _levelNo += 1;
        _uiWin.SetActive(false);
        _uILose.SetActive(false);
    }

    public void ClearLastLevel()
    {
        Transform[] LevelObjects = _levelObjectsParent.transform.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < LevelObjects.Length; i++)
        {
            if (i != 0)
            {
                Destroy(LevelObjects[i].gameObject);
            }
        }
    }
}
