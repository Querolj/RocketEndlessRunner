using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public Camera _main_camera;
    /// <summary>
    /// All asteroid objects in the game
    /// </summary>
    public GameObject[] _asteroids;

    /// <summary>
    /// Indicator GameObject to know where and in how many time a asteroid will spawn
    /// </summary>
    public GameObject _indicator_object;

    /// <summary>
    /// Velocity for every asteroid
    /// </summary>
    public float _base_asteroid_velocity = 1f;

    /// <summary>
    /// Min and max time between asteroid spawning
    /// </summary>
    public float _min_time_spawn = 3f;
    public float _max_time_spawn = 4f;

    /// <summary>
    /// When to reduce time spawn of asteroid?
    /// </summary>
    public float _reduce_time_spawn_cd = 1f;

    public float _minimal_time_spawn = 0.2f;
    /// <summary>
    /// How long should the indicator that a asteroid is coming should last?
    /// </summary>
    public float _indicator_time = 1f;

    /// <summary>
    /// How near should the rocket be to get a bonus?
    /// </summary>
    public float _near_bonus_distance = 0.25f;

    /// <summary>
    /// Timer to know when to spawn an indicator
    /// </summary>
    private float _timer_spawn;

    /// <summary>
    /// Timer to know when to reduce spawn timer
    /// </summary>
    private float _reduce_time_spawn_timer;

    /// <summary>
    /// Value to reduce _timer_spawn
    /// </summary>
    private float _timer_spawn_multiplior = 1f;

    /// <summary>
    /// Timer since the scene started
    /// </summary>
    private float _general_timer = 0f;

    private readonly Vector3[] _DIRECTIONS = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };

    /// <summary>
    /// Screen corner in world position
    /// </summary>
    private Vector3 _upper_left;
    public Vector3 UpperLeft
    {
        get { return _upper_left; }
    }
    private Vector3 _upper_right;
    public Vector3 UpperRight
    {
        get { return _upper_right; }
    }
    private Vector3 _lower_left;
    public Vector3 LowerLeft
    {
        get { return _lower_left; }
    }
    private Vector3 _lower_right;
    public Vector3 LowerRight
    {
        get { return _lower_right; }
    }

    public static AsteroidManager Instance;
    private void Awake()
    {
        Instance = this;
        // Screens coordinate corner location
        Vector3 upperLeftScreen = new Vector3(0, Screen.height, 0);
        Vector3 upperRightScreen = new Vector3(Screen.width, Screen.height, 0);
        Vector3 lowerLeftScreen = new Vector3(0, 0, 0);
        Vector3 lowerRightScreen = new Vector3(Screen.width, 0, 0);

        //Corner locations in world coordinates
        _upper_left = _main_camera.ScreenToWorldPoint(upperLeftScreen);
        _upper_right = _main_camera.ScreenToWorldPoint(upperRightScreen);
        _lower_left = _main_camera.ScreenToWorldPoint(lowerLeftScreen);
        _lower_right = _main_camera.ScreenToWorldPoint(lowerRightScreen);

        _timer_spawn = Random.Range(_min_time_spawn, _max_time_spawn);
        _reduce_time_spawn_timer = _reduce_time_spawn_cd;

    }

    void Update()
    {
        _general_timer += Time.deltaTime;
        _timer_spawn -= Time.deltaTime;
        _reduce_time_spawn_timer -= Time.deltaTime;

        if (_reduce_time_spawn_timer <= 0 && _timer_spawn_multiplior > 0.1f)
        {
            _timer_spawn_multiplior -= 0.025f;
            _reduce_time_spawn_timer = _reduce_time_spawn_cd;
        }

        if (_timer_spawn <= 0)
        {
            SpawnAsteroid();
            _timer_spawn = Random.Range(_min_time_spawn, _max_time_spawn);
            _timer_spawn *= _timer_spawn_multiplior;
            if (_timer_spawn <= _minimal_time_spawn)
            {
                _timer_spawn = _minimal_time_spawn;
            }
        }
    }

    /// <summary>
    /// Spawn an asteroid
    /// </summary>
    private void SpawnAsteroid()
    {
        //Choose spawning point
        int direction_index = Random.Range(0, _DIRECTIONS.Length);
        float random_lerp = Random.Range(0.1f, 0.9f);
        Vector3 spawning_point = Vector3.zero;
        Vector3 indicator_euler = Vector3.zero;
        switch (direction_index)
        {
            case 0://up
                spawning_point = Vector3.Lerp(_lower_left, _lower_right, random_lerp);
                break;
            case 1://down
                spawning_point = Vector3.Lerp(_upper_left, _upper_right, random_lerp);
                indicator_euler.z = 180f;
                break;
            case 2://left
                spawning_point = Vector3.Lerp(_upper_right, _lower_right, random_lerp);
                indicator_euler.z = 90f;
                break;
            case 3://right
                spawning_point = Vector3.Lerp(_upper_left, _lower_left, random_lerp);
                indicator_euler.z = -90f;
                break;
            default:
                Debug.LogError("ERROR SpawnAsteroid : unknown direction");
                break;
        }

        int asteroid_index = Random.Range(0, _asteroids.Length);
        spawning_point.z = 0f;

        //The indicator will spawn the asteroid after a set amount of time
        Indicator indicator = Instantiate(_indicator_object, spawning_point, Quaternion.Euler(indicator_euler)).GetComponentInChildren<Indicator>();
        indicator.SetAsteroidToSpawn(_asteroids[asteroid_index], _DIRECTIONS[direction_index], _base_asteroid_velocity);
    }

}
