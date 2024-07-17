

public static class Tool
{
    public static bool isLoadMainScene = false;
    public static void AddListenerToBtn(UnityEngine.UI.Button _btn, UnityEngine.Events.UnityAction action)
    {
        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(action);
    }
}
