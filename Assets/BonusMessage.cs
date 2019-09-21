using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BonusMessage : MonoBehaviour
{
    public float _show_cd;
    public float _flow_speed = 5f;
    private Text _display;

    private float _timer_msg_showed;
    void Start()
    {
        _display = this.GetComponent<Text>();
        _timer_msg_showed = _show_cd;
    }

    void Update()
    {
        _timer_msg_showed -= Time.deltaTime;
        if (_timer_msg_showed <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            DisplayBonus();
        }
    }

    private void DisplayBonus()
    {
        Vector3 new_position = _display.transform.position;
        new_position.y += _flow_speed * Time.deltaTime;
    }

    public void SetTextDisplay(string text)
    {
        _display.text = text;
    }
}