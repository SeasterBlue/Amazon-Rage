using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationIdle : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveDistance = 1.0f; // Distance to move up and down
    public float moveDuration = 0.01f;
    Vector3 actualPosition;
    bool isDown = true;
    bool isUp = false;
    void Start()
    {
       actualPosition = transform.position;
       transform.DOLocalMoveY(actualPosition.y + moveDistance, moveDuration).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void AnimationWithAll()
    {
 
    }
}
