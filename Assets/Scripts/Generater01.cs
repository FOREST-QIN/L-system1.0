using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TransformInfoo
{
    public Vector3 position;
    public Quaternion rotation;

}
public class Generater01 : MonoBehaviour
{
    //Store the vertices of the tree
    public LineRenderer up;
    //Store the Lowest point of tree
    public LineRenderer down;
    //store Store leftmost point
    public LineRenderer left;
    //store Store Rightmost point
    public LineRenderer right;
    //create a OrthographicCamera
    public Camera uicamera;
    
    [SerializeField] private GameObject Branch;//后面删除
    public GameObject Leaf;



    //[SerializeField] public int iteration;
    public Material material;
    [SerializeField] private float Length = 10f;
    [SerializeField] private float angle = 30f;
    private float width;
    private string axiom;
    private Dictionary<char, string> rules;
    private int iteration;

    public GameObject original;

    //Create a stack to store the transforminfo class and create an instance of transformstack to receive information  
    private Stack<TransformInfoo> transformStack = new Stack<TransformInfoo>();
    private string currentString = string.Empty;//遍历用


    public GameObject canv;
    private GetValue getValue;
    void Start()
    {

        


        getValue = canv.GetComponent<GetValue>();
        angle = getValue.trees[getValue.TreeFlag].Angle;
        Length = getValue.trees[getValue.TreeFlag].Length;
        width= getValue.trees[getValue.TreeFlag].Width;
        axiom= getValue.trees[getValue.TreeFlag].Axiom;
        material.color= getValue.trees[getValue.TreeFlag].color;


        if (getValue.trees[getValue.TreeFlag].Rules2!=null)
        {
            rules = new Dictionary<char, string>
            {
                { getValue.trees[getValue.TreeFlag].Rules1_1, getValue.trees[getValue.TreeFlag].Rules1 },
                { getValue.trees[getValue.TreeFlag].Rules2_1, getValue.trees[getValue.TreeFlag].Rules2 },
            };
        }
        else
        {
            rules = new Dictionary<char, string>
            {
                { getValue.trees[getValue.TreeFlag].Rules1_1, getValue.trees[getValue.TreeFlag].Rules1 }
            };
        }
    }
    private void FixedUpdate()
    {
        angle = getValue.trees[getValue.TreeFlag].Angle;
        Length = getValue.trees[getValue.TreeFlag].Length;
        width = getValue.trees[getValue.TreeFlag].Width;
        axiom = getValue.trees[getValue.TreeFlag].Axiom;
        material.color = getValue.trees[getValue.TreeFlag].color;


        if (getValue.trees[getValue.TreeFlag].Rules2 != null)
        {
            rules = new Dictionary<char, string>
            {
                { getValue.trees[getValue.TreeFlag].Rules1_1, getValue.trees[getValue.TreeFlag].Rules1 },
                { getValue.trees[getValue.TreeFlag].Rules2_1, getValue.trees[getValue.TreeFlag].Rules2 },
            };
        }
        else
        {
            rules = new Dictionary<char, string>
            {
                { getValue.trees[getValue.TreeFlag].Rules1_1, getValue.trees[getValue.TreeFlag].Rules1 }
            };
        }
    }
    LineRenderer line;
    public void Generate(int iteration)
    {
        currentString = axiom;

        //Set the number of iterations
        for (int a = 0; a < iteration - 1; a++)
        {

            string newString = "";
            //Convert the string to be converted into an array of char type
            char[] stringCharacters = currentString.ToCharArray();
            for (int i = 0; i < stringCharacters.Length; i++)
            {
                char currentCharacter = stringCharacters[i];
                //replace according to the rule
                if (rules.ContainsKey(currentCharacter))
                {
                    newString += rules[currentCharacter];
                }
                else       
                {
                    newString += currentCharacter.ToString();
                }
            }
            //return new string replaced
            currentString = newString;
            

        }


        Debug.Log(currentString);

        char[] newstringCharacters = currentString.ToCharArray();
       
        
        for (int i = 0; i < newstringCharacters.Length; i++)
        {
            char currentCharacter = newstringCharacters[i];
          
            if (currentCharacter == 'F')
            {
                //Record the current position of the gameobject
                Vector3 initialPosition = transform.position;
                //Move this gameobject 
                transform.Translate(Vector3.up  * Length);

               // GameObject treeSegment = newstringCharacters[(i + 1) % newstringCharacters.Length] == 'X' || newstringCharacters[(i + 3) % newstringCharacters.Length] == 'F' ? Instantiate(Leaf,gameObject.transform,true) : Instantiate(Branch, gameObject.transform, true);
                //newstringCharacters[(i + 1) % newstringCharacters.Length]
                //Instantiate a gameobject with line renderer to draw line
                 GameObject treeSegment = Instantiate(Branch, gameObject.transform, true);

                 //&& newstringCharacters[(i + 4) % newstringCharacters.Length] == 'X'

                //Sets the width of the line
                treeSegment.GetComponent<LineRenderer>().startWidth = width;
                treeSegment.GetComponent<LineRenderer>().endWidth = width;

                //Draw a line between the initial position and the moved position
                treeSegment.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
               
                line = treeSegment.GetComponent<LineRenderer>();
                //TODO 新增
                #region TODO 新增

                //Compare the Y values of the start and end points and store the larger ones in the "up"
                if (!up||up.GetPosition(1).y<line.GetPosition(1).y)
                {
                    up = line;
                }
                if (!down||down.GetPosition(1).y>line.GetPosition(0).y)
                {
                    down = line;
                }
                if (!left||left.GetPosition(1).x>line.GetPosition(1).x)
                {
                    left =line;
                }
                if (!right||right.GetPosition(1).x<line.GetPosition(1).x)
                {
                    right = line;
                }
                

                #endregion
            }
            else if (currentCharacter == 'X')//为啥X不做操作呢  因为不可能把X都迭代成F
            {

            }
            else if (currentCharacter == '+')
            {
                transform.Rotate(-Vector3.back * angle);
                //transform.Rotate(Vector3.up * angle);
            }
            else if (currentCharacter == '-')
            {
                transform.Rotate(-Vector3.forward * angle);
                //transform.Rotate(Vector3.up * -angle);
            }
            else if (currentCharacter == '[')
            {
                //create TransformInfo to record the current state of gameobject
                TransformInfoo ti = new TransformInfoo();
                ti.position = transform.position;
                ti.rotation = transform.rotation;
                //Push this information to the stack
                transformStack.Push(ti);
            }
            else if (currentCharacter == ']')
            {
                TransformInfoo ti = transformStack.Pop();
                transform.position = ti.position;
                transform.rotation = ti.rotation;
            }
            else
            {
                throw new System.InvalidOperationException("Invalid L-tree operation");

            }

            //gameObject.transform.position = original.transform.position;
            //gameObject.transform.rotation = original.transform.rotation;
          

        }

        //Gets the center position of the tree
        float temp = (up.GetPosition(1).y - down.GetPosition(0).y) / 2;
        temp = down.GetPosition(0).y + temp;
        //place the camera at the center position of tree
        uicamera.transform.position = new Vector3(down.GetPosition(0).x, temp, -100);

        //The size of the orthographic view is set to the size of the tree
        uicamera.orthographicSize = up.GetPosition(1).y-down.GetPosition(0).y;





    }

    //private void OnGUI()
    //{
    //    GUIStyle fontstyle = new GUIStyle();
    //    fontstyle.normal.textColor=Color.red;
    //    fontstyle.fontSize = 120;
    //    if (up)
    //    {
           
    //        GUILayout.Label("UP: "+up.GetPosition(1).y.ToString(),fontstyle);
    //    }
        
    //    if (down)
    //    {
            
    //        GUILayout.Label("down: "+down.GetPosition(0).y.ToString(),fontstyle);
    //    }
        
    //    if (left)
    //    {
          
    //        GUILayout.Label( "left: "+left.GetPosition(1).x.ToString(),fontstyle);
    //    }
        
    //    if (right)
    //    {
           
    //        GUILayout.Label("right: "+right.GetPosition(1).x.ToString(),fontstyle);
    //    }
    //}



}
