using UnityEngine; 
using System.Collections.Generic;
using Unity.Collections;

abstract public class SingletonMono<T> : SingletonBase where T : MonoBehaviour
{
    private static T _instance = null;
    bool isAwakeCalled;
    /// <summary>
    /// Is there any instance of <typeparamref name="T"></typeparamref>
    /// </summary>
    public static bool HasInstance => _instance; 
    public static T Instance
    { 
        private set
        {
            if (_instance && value == _instance)
                return;

            if(HasInstance)
                (_instance as SingletonMono<T>).OnInstanceRemoved(); 

            _instance = value;
             
            if (_instance != null)
            {
                var instanceSing = _instance as SingletonMono<T>; 

                instanceSing.OnInstanceSet();
                if (!instanceSing.isAwakeCalled)
                    instanceSing.Awake();

                if (instanceSing.enabled)
                    instanceSing.OnEnable();
                else
                    instanceSing.OnDisable();
            } 
        }
        get => _instance ??= FindObjectOfType<T>();   
    }   

    static public T FindAndForceInstance()
    {
        if ( ! _instance )
        {
            var inst = FindObjectOfType<T>();
            if (inst)
                (inst as SingletonMono<T>).ForceInstance();
            return inst;
        } 
        return _instance;
    }
    /// <summary>
    /// Is this the instance class for <typeparamref name="T"></typeparamref>?
    /// </summary>
    public override bool IsInstance { get => _instance && this == _instance; }
    /// <summary>
    /// Force this to be the instance class for <typeparamref name="T"></typeparamref>
    /// </summary>
    public override void ForceInstance() => Instance = this as T;
    protected virtual void Awake()
	{  
#if UNITY_EDITOR
        var prefabStatus = UnityEditor.PrefabUtility.GetPrefabInstanceStatus(gameObject);
        if(prefabStatus == UnityEditor.PrefabInstanceStatus.NotAPrefab)
		{
#endif
            T thisT = this as T;
            if (HasInstance)
            {
                if (IsInstance)
                    OnInstanceAwake();
                else 
                    OnNoneInstanceAwake();
            }
            else 
                Instance = thisT;  
#if UNITY_EDITOR
        }
#endif
        isAwakeCalled = true;

        OnAnyAwake(); 
    }


	void OnEnable()
    { 
        T  thisT = this as T;

        if (_instance != null && _instance != thisT) 
            OnNoneInstanceEnabled(); 
        else
        {
            Instance = thisT; 
            OnInstanceEnabled(); 
        }  
    }
    public void OnDestroy()
	{

		if (_instance == this)
        {
            OnInstanceRemoved();
            OnInstanceDestroy();
            Instance = null;
        }

		OnAnyDestroy();

	} 
    void OnDisable()
	{
        if (IsInstance)
            OnInstanceDisabled();
	} 
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

}



/// <summary>
/// This class is mostly for the custom editor implementation.
/// </summary>
public abstract class SingletonBase : MonoBehaviour
{
    public abstract bool IsInstance { get; }
    public abstract void ForceInstance();
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(SingletonMono<>), true)]
public class SingletonMonoEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var targetMono = target as SingletonBase;
        if( ! ReferenceEquals(targetMono ,null) )
        {
            var isInstance = targetMono.IsInstance;
            GUI.enabled = !isInstance;
            if( GUILayout.Button(isInstance ? "Instance Class" : "Force Instance Class") )
                targetMono.ForceInstance();
        }
    } 
}
#endif
