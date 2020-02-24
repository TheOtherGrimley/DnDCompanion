using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    public static BookController Instance;

    /// <summary>
    /// If Page is 0, you're on the left page, if 1 you're on the right.
    /// </summary>
    public static int Page;

    /// <summary>
    /// If section is 0 you're on L/R 1, if 1 you're on L/R 2
    /// </summary>
    public static int Section;


    Animator anim;

    [SerializeField]
    List<GameObject> Canvases;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();

        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void NextPage()
    {
        SectionUpdate();
        if (Page == 0)
        {
            anim.SetTrigger("Next Page");
            StartCoroutine(ChangeSectorAndPage(Section, 1));
            //Page = 1;
        }
        else if (Page == 1 && Section == 0)
        {
            anim.SetTrigger("Flip right");
            StartCoroutine(ChangeSectorAndPage(1, 0));
            //Page = 0;
            //Section = 1;
        }
    }

    public void PrevPage()
    {
        SectionUpdate();
        if (Page == 1)
        {
            anim.SetTrigger("Prev Page");
            //Page = 0;
            StartCoroutine(ChangeSectorAndPage(Section, 0));
        }
        else if (Page == 0 && Section == 1)
        {
            anim.SetTrigger("Flip Left");
            StartCoroutine(ChangeSectorAndPage(0, 1));
            //Page = 1;
            //Section = 0;
        }
    }

    void SectionUpdate()
    {
        if(Section == 0)
        {
            StartCoroutine(FadePageIn(Canvases[0]));
            StartCoroutine(FadePageIn(Canvases[1]));
            StartCoroutine(FadePageOut(Canvases[2]));
            StartCoroutine(FadePageOut(Canvases[3]));
            //Canvases[0].SetActive(true);
            //Canvases[1].SetActive(true);
            //Canvases[2].SetActive(false);
            //Canvases[3].SetActive(false);
        }
        if (Section == 1)
        {
            StartCoroutine(FadePageOut(Canvases[0]));
            StartCoroutine(FadePageOut(Canvases[1]));
            StartCoroutine(FadePageIn(Canvases[2]));
            StartCoroutine(FadePageIn(Canvases[3]));
            //Canvases[0].SetActive(false);
            //Canvases[1].SetActive(false);
            //Canvases[2].SetActive(true);
            //Canvases[3].SetActive(true);
        }
    }

    IEnumerator ChangeSectorAndPage(int sect, int page)
    {
        yield return new WaitForSeconds(0.1f);
        Page = page;
        Section = sect;
    }

    IEnumerator FadePageOut(GameObject Canvas)
    {
        Debug.Log("HIT");
        float t = 0.2f;
        Text[] texts = Canvas.transform.GetComponentsInChildren<Text>();
        Image[] images = Canvas.transform.GetComponentsInChildren<Image>();
        Color c = new Color(0, 0, 0, 0f);
        while (t > 0)
        {
            foreach(Text txt in texts)
            {
                txt.color = Color.Lerp(txt.color, c, t);
            }
            foreach(Image img in images)
            {
                img.color = Color.Lerp(img.color, c, t);
            }
            t -= t / Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Canvas.SetActive(false);
    }

    IEnumerator FadePageIn(GameObject Canvas)
    {
        float t = 0.2f;
        Text[] texts = Canvas.transform.GetComponentsInChildren<Text>();
        Image[] images = Canvas.transform.GetComponentsInChildren<Image>();
        while (t > 0)
        {
            foreach(Text txt in texts)
            {
                txt.color = Color.Lerp(txt.color, new Color(txt.color.r, txt.color.g, txt.color.b, 1f), t);
            }
            foreach(Image img in Canvas.transform.GetComponentsInChildren<Image>())
            {
                img.color = Color.Lerp(img.color, new Color(img.color.r, img.color.g, img.color.b, 1f), t);
            }
            t -= t / Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Canvas.SetActive(true);
    }
}
