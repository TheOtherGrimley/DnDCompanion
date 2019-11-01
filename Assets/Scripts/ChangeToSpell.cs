using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeToSpell : ChangePage
{
    public GameObject SpellLevelButton;

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

            SpellDetailUtil sdu = temp.GetComponent<SpellDetailUtil>(); //Only one GetC. means better performance
            sdu.Title.text = s.name;
            sdu.Description.text = s.desc.Replace("</p><p>", "\n\n").Replace("<p>","").Replace("</p>", "");
            sdu.Range.text = s.range;
            sdu.Ritual.text = s.ritual;
            sdu.Duration.text = s.duration;
            sdu.Concentration.text = s.concentration;
            sdu.Cast.text = s.casting_time;
            sdu.Level.text = s.level;
            sdu.Classes.text = s.classes;
            int MinSpellLevel = int.TryParse(s.level[0].ToString(), out MinSpellLevel) ? int.Parse(s.level[0].ToString()) : 0;
            for(int i = MinSpellLevel; i < 9; i++)
            {
                GameObject sp_level = Instantiate(SpellLevelButton, sdu.SpellButtonsParent);
                sp_level.name = i.ToString();
                sp_level.GetComponent<Text>().text = i.ToString();
            }
            

            BookController.Instance.NextPage();
        }
        if (Direction == Dir.Backwards) { }
    }
}