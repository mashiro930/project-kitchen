using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBack : MonoBehaviour
{
    [SerializeField] private int delay = 6;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedLoadBack());
    }

    private IEnumerator DelayedLoadBack()
    {

        yield return new WaitForSeconds(delay);

        Loader.LoadBack();
    }
}
