using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SpriteAnimator
{
    /// <summary>
    /// Sprites of the animation
    /// </summary>
    [SerializeField]
    private Sprite[] _sprites;

    /// <summary>
    /// Time between each frame
    /// </summary>
    [SerializeField]
    private float _timer_cd;

    /// <summary>
    /// Should the animation loop?
    /// </summary>
    [SerializeField]
    private bool _is_looping = true;

    /// <summary>
    /// Timer to know when we change sprite
    /// </summary>
    private float _count_timer = 0;

    /// <summary>
    /// Index of the sprite that should be displayed
    /// </summary>
    private int _current_index = 0;

    private void Awake()
    {
        _count_timer = _timer_cd;
    }

    /// <summary>
    /// Update the animation
    /// </summary>
    /// <returns></returns>
    public Sprite UpdateAnimation()
    {
        _count_timer -= Time.deltaTime;
        if (_count_timer <= 0)
        {
            _current_index = (_current_index + 1) % _sprites.Length;
            _count_timer = _timer_cd;

        }

        return _sprites[_current_index];
    }

    /// <summary>
    /// Is this object able to be animated with its current parameters?
    /// </summary>
    /// <returns></returns>
    public bool CanBeAnimated()
    {
        if (_sprites.Length == 0)
        {
            return false;
        }

        if (_timer_cd <= 0)
        {
            return false;
        }

        if (_current_index >= _sprites.Length && !_is_looping)
        {
            return false;
        }

        return true;
    }
}