using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class SelectedCell
{

    public static Vector2Int SelectedСell
    {
        get { return _selectedCell; }
    }

    private static Vector2Int _selectedCell= Vector2Int.zero;
        
    public static void NameDecryption(string Name)
    {
        _selectedCell = new Vector2Int(int.Parse(new string(Name[1],1)), int.Parse(new string(Name[4], 1)));
        Debug.Log(SelectedСell);
    }
}
