using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    /// <summary>
    /// Speed of the rocket
    /// </summary>
    [SerializeField]
    [Range(0.01f, 10f)]
    private float _speed = 1f;

    /// <summary>
    /// Angular speed of the rocket
    /// </summary>
    [SerializeField]
    [Range(0.1f, 500f)]
    private float _angular_speed = 10f;

    /// <summary>
    /// angular speed added to simulate draft
    /// </summary>
    [SerializeField]
    [Range(0.0f, 5000f)]
    private float _added_angular_speed_for_draft = 3f;

    /// <summary>
    /// How strong/weak the inertia should be? 
    /// The nearest to 0 the value is, the weaker the inertia will be
    /// </summary>
    [SerializeField]
    [Range(0.00001f, 0.2f)]
    private float _inertia_coeff;

    /// <summary>
    /// Pivot point of the rocket's sprite
    /// </summary>
    [SerializeField]
    private Transform _pivot;

    /// <summary>
    /// Current direction
    /// </summary>
    private Vector3 _target = Vector3.zero;

    private Rigidbody2D _body;

    private class IntertiaVector
    {
        public float _lifetime = 1f;
        public Vector3 _initial_strength;

        public IntertiaVector(Vector3 initial_strength)
        {
            _initial_strength = initial_strength;
        }

    }
    private List<IntertiaVector> _inertia_vectors = new List<IntertiaVector>();

    private void Start()
    {
        _body = this.GetComponent<Rigidbody2D>();
        _target = _pivot.position;
    }

    private void Update()
    {
        UpdatePosition();
        IntertiaStrength();
    }



    /// <summary>
    /// Add strength of the inertia to the rocket
    /// </summary>
    private void IntertiaStrength()
    {
        List<IntertiaVector> inertia_vectors_to_remove = new List<IntertiaVector>();
        foreach (IntertiaVector inertia_vector in _inertia_vectors)
        {
            if (!UpdateInertiaVector(inertia_vector))
            {
                inertia_vectors_to_remove.Add(inertia_vector);
            }
        }

        foreach (IntertiaVector inertia_vector in inertia_vectors_to_remove)
        {
            _inertia_vectors.Remove(inertia_vector);
        }
    }

    /// <summary>
    /// Update lifetime of one inertia vector, and add its strength 
    /// to the rocket if necessary
    /// </summary>
    /// <param name="inertia_vector">inertia vector to be applyied to the rocket</param>
    /// <returns>return true if lifetime is not expired, else false</returns>
    private bool UpdateInertiaVector(IntertiaVector inertia_vector)
    {
        inertia_vector._lifetime -= Time.deltaTime;
        if (inertia_vector._lifetime <= 0)
        {
            return false;
        }

        _pivot.position += inertia_vector._initial_strength * inertia_vector._lifetime * _inertia_coeff;
        return true;
    }

    /// <summary>
    /// Update position of the rocket
    /// </summary>
    private void UpdatePosition()
    {
        if (Input.mousePosition != null || Input.mousePosition != Vector3.zero)
        {
            _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _target.z = 0f;
        }

        float angular_step = GetAngleStep();
        _pivot.right = Quaternion.Euler(0f, 0f, angular_step) * _pivot.right;
        Vector3 strength_added = _pivot.right * -_speed * Time.deltaTime;
        _pivot.position += strength_added;

        IntertiaVector inertia_vector = new IntertiaVector(strength_added);
        _inertia_vectors.Add(inertia_vector);

    }

    /// <summary>
    /// Get angle step between rocket and target
    /// </summary>
    /// <returns></returns>
    private float GetAngleStep()
    {
        Vector3 target_direction = (_target - _pivot.position);
        float angle = Vector3.Angle(_pivot.up, target_direction);
        float real_angular_speed = _angular_speed;

        if (Mathf.Abs(angle - 90) < _angular_speed * Time.deltaTime)
        {
            real_angular_speed = angle;
        }
        real_angular_speed += _added_angular_speed_for_draft * Mathf.Abs(angle - 90) / 180f;

        print(Mathf.Abs(angle - 90));
        if (angle < 90)
        {
            return Time.deltaTime * -real_angular_speed;
        }
        else
        {
            return Time.deltaTime * real_angular_speed;
        }
    }
}
