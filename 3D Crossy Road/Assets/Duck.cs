
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Duck : MonoBehaviour
{
    [SerializeField] [Range(min: 0,max: 1)] float moveDuration = 0.1f;
    [SerializeField] [Range(min: 0,max: 1)] float jumpHeight = 0.5f;
    // Update is called once per frame
    void Update()
    {
        if(DOTween.IsTweening(targetOrId: transform))
            return;

        Vector3 direction = Vector3.zero;

        if(Input.GetKeyDown(key: KeyCode.W) || Input.GetKeyDown(key: KeyCode.UpArrow))
        {
            direction += Vector3.forward;
        }
        else if(Input.GetKeyDown(key: KeyCode.S) || Input.GetKeyDown(key: KeyCode.DownArrow))
        {
            direction += Vector3.back;
        }
        else if(Input.GetKeyDown(key: KeyCode.D) || Input.GetKeyDown(key: KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }
        else if(Input.GetKeyDown(key: KeyCode.A) || Input.GetKeyDown(key: KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }

            if(direction == Vector3.zero)
                return;

            Move(direction: direction);
    }
    public void Move(Vector3 direction)
    {
        
        transform.DOJump(endValue: transform.position+direction,jumpPower: jumpHeight,numJumps: 1 ,duration: moveDuration);
        
        transform.forward = direction;
    }
}
