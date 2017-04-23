using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvent {
    public delegate void CellPressedEvent(GridCell cell, bool triggered);
    public static event CellPressedEvent OnCellPressed;

    public static void Send(GridCell cell, bool triggered) {
        if (null != cell && OnCellPressed != null) {
            OnCellPressed(cell, triggered);
        }
    }
}
