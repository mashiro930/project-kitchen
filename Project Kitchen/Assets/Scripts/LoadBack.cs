using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBack : MonoBehaviour
{
    [SerializeField] private int delay = 6;
    private Coroutine loadCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        loadCoroutine = StartCoroutine(DelayedLoadBack());
    }

    private void Update()
    {
        if (Input.anyKey) {
            StopCoroutine(loadCoroutine);
            Loader.LoadBack();
        }
    }

    private IEnumerator DelayedLoadBack()
    {

        yield return new WaitForSeconds(delay);

        Loader.LoadBack();
    }
}
