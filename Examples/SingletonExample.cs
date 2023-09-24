using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonExample : SingletonMono<SingletonExample>
{ 
    //example of a singleton that only enables only the instance behavior's gameobject
    //while keeping none instances alive
    protected override void OnInstanceAwake()
    {
        Debug.Log("This is an instance awake");
    }

    protected override void OnAnyAwake()
    {
        Debug.Log("this is either an instance or none instance awake");
    } 

    protected override void OnNoneInstanceAwake()
    { 
        Debug.LogWarning("None instances are disabled on this example singletone!");
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(IsInstance)
            Debug.Log("This is an instance class on " + gameObject.name);
    }

    protected override void OnInstanceSet()
    {
        Debug.Log("Instance set on " + gameObject.name);
        gameObject.SetActive(true);
    }

    protected override void OnInstanceDestroy()
    {
        Debug.Log("Instance was destroyed!");
    } 

}
