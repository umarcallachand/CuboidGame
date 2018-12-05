using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData {
    public float PlayerX { get; set; }
    public float PlayerY { get; set; }
    public float PlayerZ { get; set; }
    public int Score { get; set; }
    public int CurrentLevel { get; set; }
    public float PlayerXRotation { get; set; }
    public float PlayerYRotation { get; set; }
    public float PlayerZRotation { get; set; }
    public Direction TopDirection { get; set; }
}
