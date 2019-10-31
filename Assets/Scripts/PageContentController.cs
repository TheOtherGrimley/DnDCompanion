using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageContentController : MonoBehaviour
{
    public enum DataType{ JSON, Templated };
    public List<Page> Pages;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public struct Page
    {
        public string Title;
        public GameObject prefab;
    }
}
