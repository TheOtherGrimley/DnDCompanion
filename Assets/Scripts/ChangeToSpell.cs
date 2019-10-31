using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToSpell : ChangePage
{
    public override void Change()
    {
        Page pageController;
        if (!isSpellButton)
            pageController = PageParent.transform.GetComponentInParent<Page>();
        else
            pageController = this.transform.parent.parent.parent.GetComponentInParent<Page>();

        if (Direction == Dir.Forward)
        {
            for (int i = pageController.Neighbours[1].transform.childCount; i > 0; i--)
                Destroy(pageController.Neighbours[1].transform.GetChild(i - 1).gameObject);
            GameObject temp = Instantiate(PageToLoad, pageController.Neighbours[1].transform);

            SpellDeets s = this.GetComponent<SpellDeets>();

            SpellDetailUtil sdu = temp.GetComponent<SpellDetailUtil>();
            sdu.Title.text = s.name;
            sdu.Description.text = s.desc;

            BookController.Instance.NextPage();
        }
        if (Direction == Dir.Backwards) { }
    }
}