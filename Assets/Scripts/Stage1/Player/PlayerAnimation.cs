using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    public string[] staticDirections = {"StaticN", "StaticN", "StaticW", "StaticS", "StaticS", "StaticS", "StaticE", "StaticN"};
    public string[] walkDirections = {"WalkN", "WalkN", "WalkW", "WalkS", "WalkS", "WalkS", "WalkE", "WalkN"};
    public string[] boostDirections = { "BoostN", "BoostN", "BoostW", "BoostS", "BoostS", "BoostS", "BoostE", "BoostN" };

    int lastDirection;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void SetDirection(Vector2 _direction, bool _isBoosting)
    {
        string[] directionArray = null;
        if (_direction.magnitude < 0.1)
        {
            directionArray = staticDirections;
        }
        else
        {
            if (_isBoosting)
            {
                directionArray = boostDirections;
            }
            else
            {
                directionArray = walkDirections;
                
            }
            lastDirection = DirectionToIndex(_direction);
        }
        anim.Play(directionArray[lastDirection], 0);
    }

    private int DirectionToIndex(Vector2 _direction)
    {
        Vector2 norDir = _direction.normalized;

        float step = 360 / 8;
        float offset = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        angle += offset;
        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
}
