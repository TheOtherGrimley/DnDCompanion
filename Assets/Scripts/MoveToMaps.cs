using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMaps : MonoBehaviour
{
    [SerializeField]
    Transform MapsInitTarget;
    [SerializeField]
    float MovSpeed = 2f;
    Vector3 LastBookLoc_pos;
    Quaternion LastBookLoc_rot;
    GameObject MainCam;
    Animator anim;
    bool _isAnimating = false;


    // Start is called before the first frame update
    void Start()
    {
        MainCam = Camera.main.gameObject;
        anim = GameObject.FindGameObjectWithTag("BookParent").GetComponent<Animator>();
    }

    public void MoveTowardsMaps()
    {
        if (!_isAnimating)
        {
            LastBookLoc_pos =  MainCam.transform.position;
            LastBookLoc_rot =  MainCam.transform.rotation;

            StartCoroutine(TransformAnimToMap());
        }
    }

    public void MoveFromMaps()
    {
        if(!_isAnimating)
            StartCoroutine(TransformAnimFromMap());
    }

    IEnumerator TransformAnimToMap()
    {
        _isAnimating = true;
        anim.enabled = false;

        float t = 0;
        while (t < 1)
        {
            MainCam.transform.position = Vector3.Lerp(LastBookLoc_pos, MapsInitTarget.position, t);
            MainCam.transform.rotation = Quaternion.Lerp(LastBookLoc_rot, MapsInitTarget.rotation, t);
            t += Time.deltaTime * (MovSpeed*10);
            yield return new WaitForEndOfFrame();
        }

        _isAnimating = false;
    }

    IEnumerator TransformAnimFromMap()
    {
        _isAnimating = true;
        Transform currentTransform = MainCam.transform;

        float t = 0;
        while (t < 0.18)
        {
            MainCam.transform.position = Vector3.Lerp(currentTransform.position, LastBookLoc_pos, t);
            MainCam.transform.rotation = Quaternion.Lerp(currentTransform.rotation, LastBookLoc_rot, t);
            t += Time.deltaTime * MovSpeed;
            yield return new WaitForEndOfFrame();
        }

        _isAnimating = false;
        anim.enabled = true;
    }
}
