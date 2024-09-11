# Unity-Singleton-MonoBehaviours
Easy to use, Singleton MonoBehaviours pattern for unity.<br /> 
Only the SingletonMono.cs in Singeltons folder is needed.<br /> 
SingletonExtensions.cs will automatically set all instances before the first scene loads.<br />
Works with inheritance.<br />
# Usage:
```c#
public class ExampleSingleton : SingletonMono<ExampleSingleton>
{
    public int Value { get; private set; }
    protected override void OnInstanceSet()
    {
        Debug.Log("Instance set for SomeClass Singleton");
    }
}
//shared instance base on Inheritance
//everytime a new SomeOtherClass's awake is called it will steal the SomeClass Instance
public class ExampleSingletonInherited : SomeClass
{
    void Start()
    {
        ForceInstance();
    } 
}
// Use SingletonMono<T>.Instance to get the current instance
// Will get ExampleSingletonInherited if it's the instance
public class ClassUsesExampleSingleton : SomeClass
{
    void Start()
    {
        Debug.Log(ExampleSingleton.Instance.Value);
    } 
}

``` 
Switch instance using:
```c#
SomeClass some = ...;
SomeClass.Instance = some; //set it to null to remove the instance
some.ForceInstance(): //forces this to be the instance class, same as Instance = some
```

The methods that you can override:
```c# 
/// <summary>
/// alled when this object is set to Instance of <typeparamref name="T"></typeparamref>
/// </summary>
protected virtual void OnInstanceSet() {} 
/// <summary>
/// Called when this object is no longer the Instance object.
/// </summary>
protected virtual void OnInstanceRemoved() { } 
```
