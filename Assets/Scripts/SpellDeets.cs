using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpellDeets : MonoBehaviour
{
    public new string name; // We want to hide the object name for reasons i guess
    public string desc;
    public string page;
    public string range;
    public string components;
    public string material;
    public string ritual;
    public string duration;
    public string concentration;
    public string casting_time;
    public string level;
    public string school;
    public string classes;

    public void SetSpell(LoadSpellButtons.Spell s)
    {
        name = s.name;
        desc = s.desc;
        page = s.page;
        range = s.range;
        components = s.components;
        material = s.material;
        ritual = s.ritual;
        duration = s.duration;
        concentration = s.concentration;
        casting_time = s.casting_time;
        level = s.level;
        school = s.school;
        classes = s.classes;
    }
}
