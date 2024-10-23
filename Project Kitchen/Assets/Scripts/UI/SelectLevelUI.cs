using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelUI : MonoBehaviour
{
    [SerializeField] private Button Level1Button;
    [SerializeField] private Button Level2Button;

    // Start is called before the first frame update
    void Start()
    {
        Level1Button.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Level1Scene);
        });
        Level2Button.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Level2Scene);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
