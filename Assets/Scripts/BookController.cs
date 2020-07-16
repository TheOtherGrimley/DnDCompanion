using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    #region Variables
    Animator _anim;

    [Header("Canvas links")]
    // We'll need to refer to these from the touch input class
    // Canvases have 2 sections we alternate to give the impression of a larger book

    int _ActiveSection;
    public CanvasSectionLinks[] CanvasSections = new CanvasSectionLinks[2];

    [Space][Header("Page meshes")]
    // These are stored references to this books inner page meshes

    public GameObject LeftPageMesh;
    Material leftPage_l {
        get
        {
            return LeftPageMesh.GetComponent<SkinnedMeshRenderer>().materials[1];
        }
        set
        {
            Material[] mats = LeftPageMesh.GetComponent<SkinnedMeshRenderer>().materials;
            mats[1] = value;
            LeftPageMesh.GetComponent<SkinnedMeshRenderer>().materials = mats;
        }
    }
    Material leftPage_r {
        get
        {
            return LeftPageMesh.GetComponent<SkinnedMeshRenderer>().materials[0];
        }
        set
        {
            Material[] mats = LeftPageMesh.GetComponent<SkinnedMeshRenderer>().materials;
            mats[0] = value;
            LeftPageMesh.GetComponent<SkinnedMeshRenderer>().materials = mats;
        }
    }

    public GameObject LeftBackMesh;
    Material left_back
    {
        get
        {
            return LeftBackMesh.GetComponent<MeshRenderer>().materials[0];
        }
        set
        {
            Material[] mats = LeftBackMesh.GetComponent<MeshRenderer>().materials;
            mats[0] = value;
            LeftBackMesh.GetComponent<MeshRenderer>().materials = mats;
        }
    }

    public GameObject RightPageMesh;
    Material rightPage_l {
        get
            {
            return RightPageMesh.GetComponent<SkinnedMeshRenderer>().materials[0];
        }
        set
            {
            Material[] mats = RightPageMesh.GetComponent<SkinnedMeshRenderer>().materials;
            mats[0] = value;
            RightPageMesh.GetComponent<SkinnedMeshRenderer>().materials = mats;
        }
    }
    Material rightPage_r {
        get
            {
            return RightPageMesh.GetComponent<SkinnedMeshRenderer>().materials[1];
        }
        set {
            Material[] mats = RightPageMesh.GetComponent<SkinnedMeshRenderer>().materials;
            mats[1] = value;
            RightPageMesh.GetComponent<SkinnedMeshRenderer>().materials = mats;
        }
    }

    public GameObject RightBackMesh;
    Material right_back {
        get
        {
            return RightBackMesh.GetComponent<MeshRenderer>().materials[0];
        }
        set
        {
            Material[] mats = RightBackMesh.GetComponent<MeshRenderer>().materials;
            mats[0] = value;
            RightBackMesh.GetComponent<MeshRenderer>().materials = mats;
        }
    }

    [Space][Header("Book cover componenets")]
    //These are stored references that will be used to customise the cover
    public GameObject Sigil;
    public GameObject Bevels;
    public Material CoverTexture;

    // Could probably pull this into a static global objects all controllers refer to rather than 4 seperate lists.
    [Space] public List<HouseBook> Books;

    public int ActiveSection {
        get => _ActiveSection;
        set {
            if(value == 0 || value == 1) // We only have 2 active sections so this should never be anything other than 0 or 1
                _ActiveSection = value;
        }
    }

    int _bookState; // 0: Left back exposed; 1: middle; 2: Right back exposed
    int _activeRightPage;

    public SectionMaterials SectMaterials;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (Books.Count < 1)
            Debug.LogError("No books have been added bro");
        if (Books[0].Pages.Count % 2 != 0)
            Debug.LogError("Book doesn't have even pages");

        _anim = this.GetComponent<Animator>();
    }

    public void OpenBook()
    {
        _anim.SetTrigger("Open");
        ActiveSection = 0; // Will always open to cover

        // Remove all children (bar next page / prev page btns)
        _ClearCanvas(CanvasSections[ActiveSection]);

        Instantiate(Books[0].Pages[0], CanvasSections[ActiveSection].LeftPageCanvas.transform);
        Instantiate(Books[0].Pages[1], CanvasSections[ActiveSection].RightPageCanvas.transform);

        if (Books[0].Pages.Count > 2)
            CanvasSections[(ActiveSection)].NextPageButton.SetActive(true);

        _activeRightPage = 1;

        _bookState = 1;
    }

    public void NextPage()
    {
        switch (_bookState)
        {
            case 0:
            case 1:
                _ClearCanvas(CanvasSections[(ActiveSection + 1) % 2]);
                Instantiate(Books[0].Pages[_activeRightPage + 1], CanvasSections[(ActiveSection + 1) % 2].LeftPageCanvas.transform);
                Instantiate(Books[0].Pages[_activeRightPage + 2], CanvasSections[(ActiveSection + 1) % 2].RightPageCanvas.transform);

                if (Books[0].Pages.Count > (_activeRightPage + 3))
                    CanvasSections[(ActiveSection + 1) % 2].NextPageButton.SetActive(true);
                else
                    CanvasSections[(ActiveSection + 1) % 2].NextPageButton.SetActive(false);

                CanvasSections[(ActiveSection)].NextPageButton.SetActive(false);
                CanvasSections[(ActiveSection)].PrevPageButton.SetActive(false);

                _activeRightPage = _activeRightPage + 2;
                ActiveSection = (ActiveSection + 1) % 2; // Modulo makes sure we aren't assigning an out of bound number
                _bookState += 1;
                break;
            case 2:
                _ClearCanvas(CanvasSections[(ActiveSection + 1) % 2]);

                if(ActiveSection == 1)
                {
                    rightPage_r = SectMaterials.s1_left;
                    right_back = SectMaterials.s1_right;
                    leftPage_r = SectMaterials.s2_left;
                    rightPage_l = SectMaterials.s2_right;
                }
                else
                {
                    rightPage_r = SectMaterials.s2_left;
                    right_back = SectMaterials.s2_right;
                    leftPage_r = SectMaterials.s1_left;
                    rightPage_l = SectMaterials.s1_right;
                }

                Instantiate(Books[0].Pages[_activeRightPage + 1], CanvasSections[(ActiveSection + 1) % 2].LeftPageCanvas.transform);
                Instantiate(Books[0].Pages[_activeRightPage + 2], CanvasSections[(ActiveSection + 1) % 2].RightPageCanvas.transform);

                if (Books[0].Pages.Count > (_activeRightPage + 3))
                {
                    CanvasSections[(ActiveSection + 1) % 2].NextPageButton.SetActive(true);
                    //CanvasSections[(ActiveSection + 1) % 2].PrevPageButton.SetActive(true);
                }
                else
                {
                    CanvasSections[(ActiveSection + 1) % 2].NextPageButton.SetActive(false);
                    //CanvasSections[(ActiveSection + 1) % 2].PrevPageButton.SetActive(true);

                }

                CanvasSections[(ActiveSection)].NextPageButton.SetActive(false);
                CanvasSections[(ActiveSection)].PrevPageButton.SetActive(false);
                _activeRightPage = _activeRightPage + 2;
                ActiveSection = (ActiveSection + 1) % 2; // Modulo makes sure we aren't assigning an out of bound number
                break;
        }

        _anim.SetTrigger("next_page");

        //TODO: CHANGE TAGS OF THE INNER MESH WHEN THE PAGES TURN SO THE CORRECT CANVAS IS HIT
    }

    void _ClearCanvas(CanvasSectionLinks canvas)
    {
        for (int i = 2; i < canvas.LeftPageCanvas.transform.childCount; i++)
            Destroy(canvas.LeftPageCanvas.transform.GetChild(i).gameObject);
        for (int i = 2; i < canvas.RightPageCanvas.transform.childCount; i++)
            Destroy(canvas.RightPageCanvas.transform.GetChild(i).gameObject);
    }

}

#region Structs
[Serializable]
public struct CanvasSectionLinks
{
    public Camera LeftPageCamera;
    public Camera RightPageCamera;
    public Canvas LeftPageCanvas;
    public Canvas RightPageCanvas;
    public GameObject NextPageButton;
    public GameObject PrevPageButton;
}

[Serializable]
public struct HouseBook
{
    public string HouseTitle;
    public List<GameObject> Pages;
    public GameObject SigilObject;
    public Material HouseTexture;
}

[Serializable]
public struct SectionMaterials
{
    public Material s1_left;
    public Material s1_right;
    public Material s2_left;
    public Material s2_right;
}
#endregion