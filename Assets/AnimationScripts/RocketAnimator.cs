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
    /// Standard flying animation
    /// </summary>
    public SpriteAnimator _low_flying_1;

    /// <summary>
    /// Sprite Renderer of the rocket
    /// </summary>
    private SpriteRenderer _renderer;
    void Awake()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
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
        if (RocketController.Instance.ConcretVelocity > 0.02f)
        {
            _renderer.sprite = _normal_flying.UpdateAnimation();
        }
        else
        {
            _renderer.sprite = _low_flying_1.UpdateAnimation();
        }

    }

}