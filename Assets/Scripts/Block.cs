using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Activatable
{
    // Update is called once per frame
    [SerializeField] List<NamedObject> keybinds;
    [SerializeField] AudioSource _as;
    
    [SerializeField] private float rotation, y;
    
    protected override void Update()
    {
        base.Update();
        
        int dir = 0;
        
        if(Input.GetKey(KeyCode.A) && active)
        {
            dir += 1;
        }
        if(Input.GetKey(KeyCode.D) && active)
        {
            dir -= 1;
        }

        float _rotation = 15f + dir * rotation;
        float _y = Mathf.Abs(dir) * y;
        
        transform.localRotation = Quaternion.Euler(Vector3.MoveTowards(transform.localRotation.eulerAngles, new Vector3(0,0,_rotation), 100 * Time.deltaTime));
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0,_y,0), 1 * Time.deltaTime);

        
        if (!active)
        {
            Hide();
            return;
        }

        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            OneShotSound s = new GameObject("clap", new[]{typeof(OneShotSound),typeof(AudioSource)})
                .GetComponent<OneShotSound>();
            
            s.GetComponent<AudioSource>().clip = _as.clip;
            s.GetComponent<AudioSource>().Play();
            s.GetComponent<AudioSource>().pitch = Random.Range(.9f, 1.05f);
            s.GetComponent<AudioSource>().volume = _as.volume;
            
            
        }
            
    }

    void Hide()
    {
        keybinds.ForEach(p =>p.second.SetActive(false));
    }
    
    protected override void Deactivate()
    {
        base.Deactivate();
        Hide();
    }
}

[System.Serializable]
public class NamedObject : Pair<KeyCode, GameObject>
{
    public NamedObject(KeyCode key, GameObject obj) : base(key, obj) { }
}