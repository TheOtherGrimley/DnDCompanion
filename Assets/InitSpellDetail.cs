using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitSpellDetail : MonoBehaviour
{
    [HideInInspector]
    public Spell spellToLoad;

    [SerializeField]
    Text Title;

    [SerializeField]
    Text Detail;

    public void LoadSpell(string spellName)
    {
        foreach (Spell s in LoadSpellButtons.spells)
            if (s.name == spellName)
            {
                spellToLoad = s;
                Title.text = spellToLoad.name;
                Detail.text = (
                    "Spell level: " + s.level + "\n" +
                    "Range: " + s.range + "\n" +
                    "Casting time: " + s.casting_time + "\n" +
                    "Concentration: " + s.concentration + "\n" +
                    "Duration: " + s.duration + "\n" +
                    "Classes: " + s.classes + "\n\n" +
                    "Description: " + s.desc.Replace("<p>", "").Replace("</p", "\n")
                    );
            }
    }
}
