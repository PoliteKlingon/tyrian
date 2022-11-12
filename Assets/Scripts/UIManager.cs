using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //making it a singleton to be able to use it anywhere
    public static UIManager Instance { get; private set; }
    
    [SerializeField] private AView startingView;
    [SerializeField] private List<AView> views;

    private AView _currentView;
    private readonly Stack<AView> _history = new Stack<AView>();

    private void Awake()
    {
        // Check, if we do not have any instance yet.    
        if (Instance == null)
        {
            // 'this' is the first instance created => save it.
            Instance = this;
            
            // We want to keep the UI always present
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // Destroy 'this' object as there exist another instance
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        foreach (var view in views)
        {
            view.Initialize();
            view.DoHide();
        }

        if (startingView == null) return;
        Show(startingView, true);
    }
    
    public static T GetView<T>() where T : AView
    {
        foreach (var view in Instance.views)
        {
            if (view is not T tView) continue;
            return tView;
        }

        return null;
    }

    public static void Show<T>(object args = null, bool remember = true, bool hideCurrent = true) where T : AView
    {
        if (Instance._currentView != null)
        {
            if (remember) Instance._history.Push(Instance._currentView);
            if (hideCurrent) Instance._currentView.DoHide();
        }

        var view = GetView<T>();
        view.DoShow(args);
        Instance._currentView = view;
    }

    public static void Show(AView view, object args = null, bool remember = true)
    {
        if (Instance._currentView != null)
        {
            if (remember) Instance._history.Push(Instance._currentView);
            Instance._currentView.DoHide();
        }

        view.DoShow(args);
        Instance._currentView = view;
    }

    public static void HideCurrent()
    {
        Instance._currentView.DoHide();
    }

    public static void DeleteHistory()
    {
        while (Instance._history.Count > 0)
        {
            Instance._history.Pop();
        }
    }

    public static System.Type GetCurrentViewType()
    {
        return Instance._currentView.GetType();
    }

    public static void ShowLast()
    {
        if (Instance._history.Count <= 0) return;
        // NOTE: Will not pass args again.
        // May or may not be desirable to add this functionality depending on your use-case
        Show(Instance._history.Pop(), false);
    }
    
    // Auto initialization in any scene. Requires a UI Manager prefab placed inside the Resources folder
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void AutoCreateManager()
    {
        if (UIManager.Instance != null) return;
        var uiManager = Resources.Load("UIManager") as GameObject;
        if (uiManager == null) return;
        var instance = Instantiate(uiManager);
    }
}
