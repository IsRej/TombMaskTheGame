using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterController : MonoBehaviour
{
    private Vector2 _startTouchPosition;
    private Vector2 _currentPosition;
    private Vector2 _endTouchPosition;
    private bool _stopTouch = false;
    private bool _canMove = true;

    public float _distanceFromWallPivot;
    public float _swipeRange;
    public float _tapRange;
    public LayerMask _obstacleLayer;
    public LayerMask _spikeLayer;
    public LayerMask _finishLayer;

    // Update is called once per frame
    void Update()
    {
        Swipe();
    }

    public void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = _currentPosition - _startTouchPosition;

            if (!_stopTouch)
            {
                if (_canMove)
                {
                    if (Distance.x < -_swipeRange)
                    {
                        Debug.Log("Left");
                        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 100f, _obstacleLayer);
                        if (hitWall)
                        {
                            _canMove = false;
                            transform.DOLocalMoveX(hitWall.transform.position.x + _distanceFromWallPivot, 0.5f).OnComplete(() => _canMove = true);
                            return;
                        }
                        RaycastHit2D hitSpike = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 100f, _spikeLayer);
                        if (hitSpike)
                        {
                            _canMove = false;
                            transform.DOLocalMoveX(hitSpike.transform.position.x + _distanceFromWallPivot, 0.5f).OnComplete(() => ActionEventHandler.OnPlayerDie());
                            return;
                        }
                        RaycastHit2D hitFinishLine = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 100f, _finishLayer);
                        if (hitFinishLine)
                        {
                            _canMove = false;
                            transform.DOLocalMoveX(hitFinishLine.transform.position.x + _distanceFromWallPivot, 0.5f).OnComplete(() => ActionEventHandler.OnPlayerWin());
                            return;
                        }
                        _stopTouch = true;
                    }
                    else if (Distance.x > _swipeRange)
                    {
                        Debug.Log("Right");
                        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 100f, _obstacleLayer);
                        if (hitWall)
                        {
                            _canMove = false;
                            transform.DOLocalMoveX(hitWall.transform.position.x - _distanceFromWallPivot, 0.5f).OnComplete(() => _canMove = true);
                            return;
                        }
                        RaycastHit2D hitSpike = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 100f, _spikeLayer);
                        if (hitSpike)
                        {
                            _canMove = false;
                            transform.DOLocalMoveX(hitSpike.transform.position.x - _distanceFromWallPivot, 0.5f).OnComplete(() => ActionEventHandler.OnPlayerDie());
                            return;
                        }
                        RaycastHit2D hitFinish = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 100f, _finishLayer);
                        if (hitFinish)
                        {
                            _canMove = false;
                            transform.DOLocalMoveX(hitFinish.transform.position.x - _distanceFromWallPivot, 0.5f).OnComplete(() => ActionEventHandler.OnPlayerWin());
                            return;
                        }
                        _stopTouch = true;
                    }
                    else if (Distance.y > _swipeRange)
                    {
                        Debug.Log("Up");
                        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 100f, _obstacleLayer);
                        if (hitWall)
                        {
                            _canMove = false;
                            transform.DOLocalMoveY(hitWall.transform.position.y - _distanceFromWallPivot, 0.5f).OnComplete(() => _canMove = true);
                            return;
                        }
                        RaycastHit2D hitSpike = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 100f, _spikeLayer);
                        if (hitSpike)
                        {
                            _canMove = false;
                            transform.DOLocalMoveY(hitSpike.transform.position.y - _distanceFromWallPivot, 0.5f).OnComplete(() => ActionEventHandler.OnPlayerDie());
                            return;
                        }
                        RaycastHit2D hitFinish = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 100f, _finishLayer);
                        if (hitFinish)
                        {
                            _canMove = false;
                            transform.DOLocalMoveY(hitFinish.transform.position.y - _distanceFromWallPivot, 0.5f).OnComplete(() => ActionEventHandler.OnPlayerWin());
                            return;
                        }
                        _stopTouch = true;
                    }
                    else if (Distance.y < -_swipeRange)
                    {
                        Debug.Log("Down");
                        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 100f, _obstacleLayer);
                        if (hitWall)
                        {
                            _canMove = false;
                            transform.DOLocalMoveY(hitWall.transform.position.y + _distanceFromWallPivot, 0.5f).OnComplete(() => _canMove = true);
                            return;
                        }
                        RaycastHit2D hitSpike = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 100f, _spikeLayer);
                        if (hitSpike)
                        {
                            _canMove = false;
                            transform.DOLocalMoveY(hitSpike.transform.position.y + _distanceFromWallPivot, 0.5f).OnComplete(() => ActionEventHandler.OnPlayerDie());
                            return;
                        }
                        RaycastHit2D hitFinish = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 100f, _finishLayer);
                        if (hitFinish)
                        {
                            _canMove = false;
                            transform.DOLocalMoveY(hitFinish.transform.position.y + _distanceFromWallPivot, 0.5f).OnComplete(() => ActionEventHandler.OnPlayerWin());
                            return;
                        }
                        _stopTouch = true;
                    }
                }
            }

        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _stopTouch = false;

            _endTouchPosition = Input.GetTouch(0).position;

            Vector2 Distance = _endTouchPosition - _startTouchPosition;

            if (Mathf.Abs(Distance.x) < _tapRange && Mathf.Abs(Distance.y) < _tapRange)
            {
                Debug.Log("Tap");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(collision.gameObject);
        }
    }
}
