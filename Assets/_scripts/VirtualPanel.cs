using System.Collections;
using UnityEngine;

public enum PanelType
{
    Menu,
    Game,
    Complete
}

public class VirtualPanel : MonoBehaviour
{
    public PanelType panelType;
}