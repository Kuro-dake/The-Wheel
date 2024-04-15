using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TerrainTools;

public class Flow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Play());
    }

    // Update is called once per frame
    [SerializeField] int clap_count = 50;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            // StartCoroutine(Applause(clap_count));
        }
    }

    [SerializeField] private List<AudioClip> claps;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Wheel _wheel;
    [SerializeField] private Block _block;

    [SerializeField]float clap_delay_from = .2f, clap_delay_to = .5f;
    [SerializeField] RectTransform button;
    IEnumerator Applause(int n)
    {
        for(int i = 0; i < n; i++)
        {
            OneShotSound s = new GameObject("clap", new[]{typeof(OneShotSound),typeof(AudioSource)})
                .GetComponent<OneShotSound>();
            
            s.GetComponent<AudioSource>().clip = claps[Random.Range(0, claps.Count)];
            s.GetComponent<AudioSource>().Play();
            s.GetComponent<AudioSource>().pitch = Random.Range(.9f, 1.05f);
            s.GetComponent<AudioSource>().volume = Random.Range(.5f, .5f);

            yield return new WaitForSeconds(Random.Range(clap_delay_from, clap_delay_to));

        }
    }
    
    IEnumerator Play()
    {
        yield return null;
        yield return this["Ladies and gentlemen! \nWelcome to the presentation of my latest invention!"];
        
        yield return this["I give you...", false];
        yield return new WaitForSeconds(1.5f);
        _wheel.visible = true;
        yield return this["The wheel!"];
        
        yield return this["This marvel of engineering solves \nproblems as old as the human race itself!"];
        yield return this["Let me show you by an example:", false];
        yield return new WaitForSeconds(1.5f);
        _block.visible = true;
        yield return this["A block has just appeared on your screen \nto help us demonstrate our point."];
        yield return this["Click on the block to activate it.", false];
        _block.GetComponent<Collider2D>().enabled = true;
        while (!_block.active)
        {
            yield return null;
        }
        yield return this["Now, press the A and D to move the block.", false];
        float t = 4f;
        while (t > 0f)
        {
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                t -= Time.deltaTime;
            }
            yield return null;
        }

        yield return this["As you can see, the block won't move no matter what we do."];
        yield return this["Enter: the solution!"];
        yield return this["The wheel!"];
        yield return this["Click on the wheel to activate it.", false];
        _wheel.GetComponent<Collider2D>().enabled = true;
        while (!_wheel.active)
        {
            yield return null;
        }
        yield return this["Now, press the A and D to move the wheel.", false];
        t = 4f;
        while (t > 0f)
        {
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                t -= Time.deltaTime;
            }
            yield return null;
        }
        yield return this["As you can see, the wheel moves as expected!\nAll the block problems solved in one fell swoop!"];
        yield return this["Isn't it fascinating?!", false];
        yield return new WaitForSeconds(3f);
        yield return this["", false];
        
        button.gameObject.SetActive(true);

        while (!button.GetComponent<Dialogue>().clicked)
        {
            yield return null;
        }

        yield return this["...", false];
        yield return new WaitForSeconds(2f);
        yield return this["yes.", false];
        yield return new WaitForSeconds(2f);
        yield return this["unfortunately.", false];
        yield return new WaitForSeconds(2f);
        yield return this["", false];
        _wheel.visible = false;
        _block.visible = false;
        Activatable.DeactivateAll();
        
        yield return new WaitForSeconds(4f);
        
        yield return this["<size=160%>The wheel</size>\n" +
                          "<size=70%>By kuro@dizztal\n\n" +
                          "Created for Ludum dare 55</size> \n\n" +
                          "<size=50%>14 April 2024 11:30 PM - 15 April 2024 02:10 AM\n"+
                          "After spending the whole weekend \ntrying to reinvent the wheel</size>\n\n" +
                          "<size=60%>Thank you for interacting</size>", false];

    }

    [SerializeField] private float text_change_duration;

    IEnumerator this[string to, bool wait_for_spacebar = true]
    {
        get
        {
            float t = 0f;
            float speed = 2f/text_change_duration;
            while (t < 1f)
            {
                t += Time.deltaTime * speed;
                _text.color = Color.Lerp(Color.white, Color.clear, t);
                yield return null;
            }

            if (wait_for_spacebar)
            {
                to += "\n\n<size=20>~ spacebar ~</size>";
            }
            _text.text = to;
            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * speed;
                _text.color = Color.Lerp(Color.clear, Color.white, t);
                yield return null;
            }
            if (wait_for_spacebar)
            {
                yield return StartCoroutine(WaitForSpacebar());
            }
            
        }
    }

    IEnumerator WaitForSpacebar()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
    }
}
