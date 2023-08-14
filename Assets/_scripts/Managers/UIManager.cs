using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private VirtualPanel[] _panels;

    private void Awake()
    {
        Instance = this;
    }

    public void EnablePanel(PanelType panelType)
    {
        foreach (var panel in _panels)
        {
            if (panel.panelType.Equals(panelType)) panel.gameObject.SetActive(true);
            else panel.gameObject.SetActive(false);
        }
    }
}
