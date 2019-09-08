using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RocketAnimator : MonoBehaviour
{
    /// <summary>
    /// Standard flying animation
    /// </summary>
    public SpriteAnimator _normal_flying;

    /// <summary>
    /// Contains every animations
    /// </summary>
    private List<SpriteAnimator> _animations = new List<SpriteAnimator>();

    /// <summary>
    /// Sprite Renderer of the rocket
    /// </summary>
    private SpriteRenderer _renderer;
    void Awake()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
        _animations.Add(_normal_flying);
    }

    void Update()
    {
        UpdateAnimations();
    }

    /// <summary>
    /// Update every animation
    /// </summary>
    private void UpdateAnimations()
    {
        foreach (SpriteAnimator animation in _animations)
        {
            if (animation.CanBeAnimated())
            {
                _renderer.sprite = animation.UpdateAnimation();
            }
        }
    }
}
