using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePage : MonoBehaviour
{
    public GameObject PageParent;
    public GameObject PageToLoad;
    public Dir Direction;
    public enum Dir { Forward, Backwards };

    [SerializeField]
    bool isSpellButton = false;

    public void Change()
    {
        Page pageController = PageParent.transform.GetComponentInParent<Page>();
        if(Direction == Dir.Forward)
        {
            for (int i = pageController.Neighbours[1].transform.childCount; i > 0; i--)
                Destroy(pageController.Neighbours[1].transform.GetChild(i - 1).gameObject);
            GameObject temp = Instantiate(PageToLoad, pageController.Neighbours[1].transform);
            if (isSpellButton)
                temp.GetComponent<InitSpellDetail>().LoadSpell(GetComponent<Text>().text);

            BookController.Instance.NextPage();
        }
        if (Direction == Dir.Backwards)
        {
            for (int i = pageController.Neighbours[0].transform.childCount; i > 0; i--)
                Destroy(pageController.Neighbours[0].transform.GetChild(i - 1).gameObject);
            Instantiate(PageToLoad, pageController.Neighbours[0].transform);
            BookController.Instance.PrevPage();

        }
    }
}
