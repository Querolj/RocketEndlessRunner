using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public int _min_font_size = 50;
    public int _max_font_size = 100;

    public Text _final_score_text;
    /// <summary>
    /// Score added by rocket speed
    /// </summary>
    private float _score = 0f;

    /// <summary>
    /// Multiplicator for the score when having bonus
    /// </summary>
    private float _multiplicator = 1;

    /// <summary>
    /// Result of _score * _multiplicator
    /// </summary>
    private int _final_score;

    /// <summary>
    /// Text that is displaying the score
    /// </summary>
    private Text _score_text;

    /// <summary>
    /// Initial font size of _score_text
    /// </summary>
    private int _initial_font_size = 35;

    public static ScoreCount Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _score_text = this.GetComponent<Text>();
        _initial_font_size = _score_text.fontSize;
    }

    void Update()
    {
        if (RocketController.Instance.IsDead)
        {
            _final_score_text.text = "Score : " + _final_score;
            return;
        }

        float softmax_velocity = Mathf.Exp(RocketController.Instance.ConcretVelocity * 100f); //Mathf.Exp(RocketController.Instance.ConcretVelocity) / 10f;
        softmax_velocity /= 20f;
        _score += softmax_velocity;
        _final_score = Mathf.RoundToInt(_score * _multiplicator);

        DisplayScore();

    }

    private void DisplayScore()
    {
        _score_text.text = Mathf.RoundToInt(_score) + " x " + System.Math.Round(_multiplicator, 2);
        int new_font_size = Mathf.RoundToInt(_initial_font_size * (RocketController.Instance.ConcretVelocity / 0.02f));
        if (new_font_size < _min_font_size)
        {
            new_font_size = _min_font_size;
        }
        else if (new_font_size > _max_font_size)
        {
            new_font_size = _max_font_size;
        }

        _score_text.fontSize = new_font_size;
        // this.transform.localScale = _initial_scale * (RocketController.Instance.ConcretVelocity / 0.01f);
    }
    //------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------
    public void AddMultiplicator(float add_mult)
    {
        _multiplicator += add_mult;
    }
}