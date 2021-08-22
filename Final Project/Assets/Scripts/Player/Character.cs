using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed;
    public bool IsMoving { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<CharacterAnimator>();
        SetPositionAndSnapToTile(transform.position);
    }

    public void SetPositionAndSnapToTile(Vector2 pos)
    {
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.5f;
        transform.position = pos;
    }


    public IEnumerator Move(Vector2 moveVector, Action OnMoveOver=null)
    {
        Animator.MoveX = Mathf.Clamp(moveVector.x, -1f, 1f);
        Animator.MoveY = Mathf.Clamp(moveVector.y, -1f, 1f);
        
        var targetPos = transform.position;
        targetPos.x += moveVector.x;
        targetPos.y += moveVector.y;

        if (!IsPathClear(targetPos))
        {
            yield break;
        }

        IsMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        IsMoving = false;

        OnMoveOver?.Invoke();
    }

    private bool IsPathClear(Vector3 targetPos)
    {
        var diff = targetPos - transform.position;
        var direction = diff.normalized;
        if(Physics2D.BoxCast(transform.position + direction, new Vector2(0.2f, 0.2f), 0f, direction, diff.magnitude - 1, GameLayers.i.SolidLayer | GameLayers.i.CharacterLayer) == true)
        {
            return false;
        }
        return true;

    }

    public void HandleUpdate()
    {
        Animator.IsMoving = IsMoving;
    }

    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.i.SolidLayer | GameLayers.i.CharacterLayer) != null)
        {
            return false;
        }
        return true;
    }

    public void LookTowards(Vector3 targetPos)
    {
        var xDiff = Mathf.Floor(targetPos.x) - Mathf.Floor(transform.position.x);
        var yDiff = Mathf.Floor(targetPos.y) - Mathf.Floor(transform.position.y);

        if(xDiff == 0 || yDiff == 0)
        {
            Animator.MoveX = Mathf.Clamp(xDiff, -1f, 1f);
            Animator.MoveY = Mathf.Clamp(yDiff, -1f, 1f);
        }
        else
        {
            Debug.LogError("Error in LookTowards");
        }
    }

    public CharacterAnimator Animator { get; private set; }
}
