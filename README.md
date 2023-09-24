# Unity-Singleton-MonoBehaviors
Easy to use, Singleton MonoBehaviors for unity.
Only the SingletonMono.cs in Singeltons folder is needed.<br /> 
SingletonExtensions.cs will automatically set all instances before the first scene loads.<br />
Works with inheritance.<br />
Usage:<br />
```c#
public class SomeClass : SingletonMono<SomeClass>
{
    protected override void OnInstanceAwake()
    {
        Debug.Log("This is an instance awake for SomeClass Instances");
    }
}
//shared instance base on Inheritance
//everytime a new SomeOtherClass is called awake it will steal the SomeClass Instance
public class SomeOtherClass : SomeClass
{
    protected override void OnAnyAwake()
    {
        ForceInstance();
    } 
}
``` 

The methods that you can override:
```c#
    /// <summary>
    /// Called on Instance or None instance Awake
    /// </summary>
    protected virtual void OnAnyAwake() { }  
    /// <summary>
    /// Called when Instance or None instance destroy
    /// </summary>
    protected virtual void OnAnyDestroy() { }
    /// <summary>
    /// alled when this object is set to Instance of <typeparamref name="T"></typeparamref>
    /// </summary>
	protected virtual void OnInstanceSet() {}
    /// <summary>
    /// Called when this Instance object is disabled
    /// </summary>
    protected virtual void OnInstanceDisabled() { }
    /// <summary>
    /// Called when this object is no longer the Instance object.
    /// </summary>
    protected virtual void OnInstanceRemoved() { }
    /// <summary>
    /// Called when this instance object is destroyed
    /// </summary>
    protected virtual void OnInstanceDestroy() { }
    /// <summary>
    /// Called when this instance object is enabled
    /// </summary>
    protected virtual void OnInstanceEnabled() { }
    /// <summary>
    /// Called when none instance object is enabled
    /// </summary>
    protected virtual void OnNoneInstanceEnabled() { }
    /// <summary>
    /// Called on instance object awake, or when instance was set and instance awake wasn't called.
    /// May be called in the editor when <see cref="ForceInstance"/> is called. 
    /// </summary>
    protected virtual void OnInstanceAwake() { }
    /// <summary>
    /// Called on none instance awake
    /// </summary>
    protected virtual void OnNoneInstanceAwake() { }
```
