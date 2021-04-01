using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwichEventListener : MonoBehaviour
{
    public UnityEvent OnPushEvent;
    public UnityEvent OnReleseEvent;
    // Start is called before the first frame update
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        OnPushEvent.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        OnReleseEvent.Invoke();
    }
}
