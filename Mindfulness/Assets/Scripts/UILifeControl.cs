using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILifeControl : MonoBehaviour
{
    [SerializeField()]
    Image Empty;

    [SerializeField()]
    Image Current;

    public bool Active = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Add()
    {
        Active = true;
        UpdateLife();
    }
    public void Remove()
    {
        Active = false;
        UpdateLife();
    }

    public void UpdateLife()
    {
        if (Current != null && Empty != null)
        {
            Current.enabled = Active;
            Empty.enabled = !Active;
        }
    }

}
