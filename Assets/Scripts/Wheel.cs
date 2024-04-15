using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Wheel : Activatable
{

    [SerializeField] private float _speed = 1f;
    [SerializeField] AudioSource _as;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!active)
            return;

        int direction = 0;
        if (Input.GetKey(KeyCode.A))
        {
            direction = 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction = -1;
        }

        if (direction != 0)
        {
            if(!_as.isPlaying)
            {
                _as.time = Random.Range(0f, _as.clip.length);
                _as.Play();
            }
        }
        else
        {
            _as.Stop();
        }

        transform.Rotate(Vector3.forward, direction * _speed * 100 * Time.deltaTime);

        transform.position += Vector3.left * direction * _speed * Time.deltaTime;
        // if moves out of screen by camera main size

        float size = Camera.main.orthographicSize * Camera.main.aspect;

        if (Mathf.Abs(transform.position.x) + transform.localScale.x > size + 2f)
        {
            transform.position += Vector3.left * (size + 1.8f) * 2f * (transform.position.x > 0 ? 1 : -1);
        }


    }

    override protected void Deactivate()
    {
        base.Deactivate();
        _as.Stop();
    }
}