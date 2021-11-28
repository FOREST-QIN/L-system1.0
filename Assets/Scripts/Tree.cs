using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Tree",menuName ="Custom/Tree",order = 1)]
public class Tree : ScriptableObject
{
    public string Axiom;
    public char Rules1_1;
    public string Rules1;
    public char Rules2_1;
    public string Rules2;
    public float Angle;
    public float Length;
    public float Width;
    public Color color;
    public int iteration;

}
