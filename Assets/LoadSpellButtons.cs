using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSpellButtons : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    TextAsset RawSpells;

    Spell[] spells;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(RawSpells.text);

        SpellList listing = JsonUtility.FromJson<SpellList>(RawSpells.text);
        spells = listing.jsonSpellData.ToArray();

        foreach (Spell s in spells)
        {
            GameObject temp = Instantiate(prefab, this.transform);
            temp.GetComponent<Text>().text = s.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    public struct Spell
    {
        public string name;
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
    }

    [Serializable]
    public struct SpellList
    {
        public List<Spell> jsonSpellData;
    }
}