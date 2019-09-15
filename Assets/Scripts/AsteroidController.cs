using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    /// <summary>
    /// vecolicty of the asteroid
    /// </summary>
    private float _velocity = 1f;
    public float Velocity
    {
        get { return _velocity; }
        set { _velocity = value; }
    }
    /// <summary>
    /// Direction where the asteroid is going
    /// </summary>
    private Vector3 _direction;
    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    /// <summary>
    /// true = clockwise, false = counterclockwise
    /// </summary>
    private bool _rotation_way;

    /// <summary>
    /// How long the rocket has been near this asteroid?
    /// </summary>
    private float _near_bonus_time = 0f;

    /// <summary>
    /// Renderer of the asteroid
    /// </summary>
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (IsOutOfBound())
        {
            GameObject.Destroy(this.gameObject);
        }
        MoveAsteroid();

        if (IsRocketNear())
        {
            _near_bonus_time += Time.deltaTime;
            AddBonus();
        }
        else
        {
            _near_bonus_time = 0;
        }
    }

    private void AddBonus()
    {
        ScoreCount.Instance.AddMultiplicator(Time.deltaTime / 100f);
    }

    /// <summary>
    /// Move the asteroid through the screen
    /// </summary>
    private void MoveAsteroid()
    {
        Vector3 new_position = this.transform.position;
        new_position += _direction * _velocity * Time.deltaTime;
        this.transform.position = new_position;

        Vector3 new_rotation = this.transform.rotation.eulerAngles;
        if (_rotation_way)
        {
            new_rotation.z += _velocity * Time.deltaTime;
        }
        else
        {
            new_rotation.z += -_velocity * Time.deltaTime;
        }
        this.transform.rotation = Quaternion.Euler(new_rotation);
    }

    /// <summary>
    /// Is the asteroid out of bounds of the screen?
    /// </summary>
    private bool IsOutOfBound()
    {
        return !_renderer.isVisible;
    }

    /// <summary>
    /// Is the rocket near this asteroid?
    /// </summary>
    /// <returns></returns>
    private bool IsRocketNear()
    {
        float distance = Vector3.Distance(RocketController.Instance.GetPosition(), this.transform.position);
        if (distance < AsteroidManager.Instance._near_bonus_distance)
        {
            return true;
        }

        return false;
    }

}