using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    /// <summary>
    /// Interval between each color change
    /// </summary>
    public float _flashes_interval = 0.2f;

    /// <summary>
    /// How longe should the indicator last?
    /// </summary>
    public float _up_time = 0.5f;

    private SpriteRenderer _renderer;

    /// <summary>
    /// Timer to calculate flashes interval
    /// </summary>
    private float _flashes_interval_timer;

    /// <summary>
    /// Data about the asteroid to spawn
    /// </summary>
    private GameObject _asteroid_to_spawn;
    private Vector3 _asteroid_direction;
    private float _asteroid_velocity;


    void Start()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
        _flashes_interval_timer = _flashes_interval;
    }

    void Update()
    {
        _up_time -= Time.deltaTime;
        if (_up_time <= 0)
        {
            GameObject.Destroy(this.gameObject);
            SpawnAsteroid();
        }

        _flashes_interval_timer -= Time.deltaTime;
        if (_flashes_interval_timer <= 0)
        {
            Flashes();
            _flashes_interval_timer = _flashes_interval;
        }
    }

    /// <summary>
    /// Spawn the asteroid, and set its direction and velocity
    /// </summary>
    private void SpawnAsteroid()
    {
        GameObject asteroid = Instantiate(_asteroid_to_spawn, this.transform.position, Quaternion.identity);
        AsteroidController asteroid_controller = asteroid.GetComponent<AsteroidController>();
        asteroid_controller.Direction = _asteroid_direction;
        asteroid_controller.Velocity = _asteroid_velocity;
    }

    /// <summary>
    /// Flashes the sprite from red to white
    /// </summary>
    private void Flashes()
    {
        if (_renderer.color == Color.red)
        {
            _renderer.color = Color.black;
        }
        else
        {
            _renderer.color = Color.red;
        }
    }

    //------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------

    public void SetAsteroidToSpawn(GameObject asteroid, Vector3 direction, float velocity)
    {
        _asteroid_to_spawn = asteroid;
        _asteroid_direction = direction;
        _asteroid_velocity = velocity;
    }
}
