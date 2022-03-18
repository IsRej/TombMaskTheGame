using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject _levelObjectsParent;
    [Header("UI Panel")]
    public GameObject _uIMenu;
    public GameObject _uiWin;
    public GameObject _uILose;
    public GameObject _uiPause;
    public GameObject _uICollective;
    public GameObject _uiPauseBtnGame;
    [Header("Text")]
    public Text _collectivesText;
    [Header("Win Buttons")]
    public Button _nextBtnWin;
    public Button _menuBtnWin;
    [Header("Lose Buttons")]
    public Button _retryBtnLose;
    public Button _menuBtnLose;
    [Header("Integer Values")]
    public int _levelNo;
    public int _collectivesNo;

    private void OnEnable()
    {
        ActionEventHandler.OnPlayerWin += CallGameWin;
        ActionEventHandler.OnPlayerDie += CallGameLose;
        ActionEventHandler.OnCollectCollectives += AddCollectives;
    }

    private void OnDisable()
    {
        ActionEventHandler.OnPlayerWin -= CallGameWin;
        ActionEventHandler.OnPlayerDie -= CallGameLose;
        ActionEventHandler.OnCollectCollectives -= AddCollectives;
    }

    private void Start()
    {
        SceneTransition.Instance.AnimateIn();
        _uiPauseBtnGame.SetActive(false);
        GetCollectives();
        _retryBtnLose.onClick.AddListener(OnRetryCalled);
        _menuBtnWin.onClick.AddListener(OnMenuCalled);
        _menuBtnLose.onClick.AddListener(OnMenuCalled);
        _nextBtnWin.onClick.AddListener(OnNextLevelCalled);
    }

    public void OpenRectTransform(RectTransform Rect)
    {
        Rect.localScale = new Vector3(0,0,0);
        Rect.DOScale(Vector3.one, 0.25f);
    }

    public void CloseRectTransform(RectTransform Rect)
    {
        Rect.localScale = new Vector3(1, 1, 1);
        Rect.DOScale(Vector3.zero, 0.25f).OnComplete(() => Rect.gameObject.SetActive(false));
    }

    public void PauseGame()
    {
        _uiPause.SetActive(true);
        _uiPauseBtnGame.SetActive(false);
        OpenRectTransform(_uiPause.GetComponent<RectTransform>());
    }

    public void ResumeGame()
    {
        _uiPauseBtnGame.SetActive(true);
        _uiPause.SetActive(false);
        CloseRectTransform(_uiPause.GetComponent<RectTransform>());
    }

    public void GetCollectives()
    {
        if (PlayerPrefs.HasKey("Collectives"))
        {
            _collectivesNo = PlayerPrefs.GetInt("Collectives");
            _collectivesText.text = _collectivesNo.ToString();
        }
        else
        {
            _collectivesNo = 0;
            PlayerPrefs.SetInt("Collectives", _collectivesNo);
            _collectivesText.text = _collectivesNo.ToString();
        }
    }

    public void AddCollectives()
    {
        _collectivesNo += 1;
        PlayerPrefs.SetInt("Collectives", _collectivesNo);
        _collectivesText.text = _collectivesNo.ToString();
    }

    public void StartGame(int LevelNo)
    {
        SceneTransition.Instance.AnimateOut();
        _uiPauseBtnGame.SetActive(true);
        _uIMenu.SetActive(false);
        _levelNo = LevelNo;
        Invoke("Transition",0.5f);
    }

    public void Transition()
    {
        LevelCreator.Instance.SelectLevel(_levelNo);
        SceneTransition.Instance.AnimateIn();
    }

    public void CallGameLose()
    {
        _uiPauseBtnGame.SetActive(false);
        _uILose.SetActive(true);
        OpenRectTransform(_uILose.GetComponent<RectTransform>());
    }

    public void CallGameWin()
    {
        _levelNo += 1;
        _uiPauseBtnGame.SetActive(false);
        _uiWin.SetActive(true);
        OpenRectTransform(_uiWin.GetComponent<RectTransform>());
        if (_levelNo == 4)
        {
            _nextBtnWin.interactable = false;
        }
    }

    public void OnRetryCalled()
    {
        SceneTransition.Instance.AnimateOut();
        CloseRectTransform(_uILose.GetComponent<RectTransform>());
        ClearLastLevel();
        _uiWin.SetActive(false);
/*        _uILose.SetActive(false);*/
        _uiPauseBtnGame.SetActive(true);
        Invoke("Transition", 0.5f);
    }

    public void OnMenuCalled()
    {
        SceneTransition.Instance.AnimateOut();
        ClearLastLevel();
        _uiPauseBtnGame.SetActive(false);
        _uiWin.SetActive(false);
        _uiPause.SetActive(false);
        _uILose.SetActive(false);
        OpenRectTransform(_uIMenu.GetComponent<RectTransform>());
        Invoke("EnableMenu", 0.5f);
    }

    public void EnableMenu()
    {
        SceneTransition.Instance.AnimateIn();
        _uIMenu.SetActive(true);
    }

    public void OnNextLevelCalled()
    {
        SceneTransition.Instance.AnimateOut();
        ClearLastLevel();
        _levelNo += 1;
        _uiWin.SetActive(false);
        _uILose.SetActive(false);
        _uiPauseBtnGame.SetActive(true);
        Invoke("Transition", 0.5f);
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
