using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetValue : MonoBehaviour
{

    //public GameObject treeGenerator;
    public GameObject pen;
    public GameObject oringinal;
    public GameObject UIwarning;
    
    

    private Generater01 generater;
    //public int iteration=0;
    public Tree[] trees;
    public InputField[] Values;
    public int TreeFlag=0;
    // Start is called before the first frame update
    void Start()
    {

        generater = pen.GetComponent<Generater01>();
        Getvalue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetTree()
    {
        GameObject[] Lines = GameObject.FindGameObjectsWithTag("Line");
        foreach (var gameObject in Lines)
        {
            Destroy(gameObject);
        }
        //iteration = 0;
        pen.transform.position = oringinal.transform.position;
        pen.transform.rotation = oringinal.transform.rotation;
       // pen.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        //generater.enabled = false;
        //treeGenerator.SetActive(false);

    }

    public void isPress()
    {
        generater.Generate(trees[TreeFlag].iteration);
    }

    public void AddTree()
    {
        TreeFlag++;
        TreeFlag = TreeFlag > 7 ? 0 : TreeFlag;
        Getvalue();
    }

    public void CutTree()
    {
        TreeFlag--;
        TreeFlag = TreeFlag<0?7: TreeFlag;
        Getvalue();
    }

    public void SubmitValue()
    {

        Setvalue();
    }

    private void Getvalue()
    {
        Values[1].text = trees[TreeFlag].Axiom;
        Values[2].text = trees[TreeFlag].Rules1_1.ToString();
        Values[3].text = trees[TreeFlag].Rules1;
        Values[4].text = trees[TreeFlag].Rules2_1.ToString();
        Values[5].text = trees[TreeFlag].Rules2;
        Values[6].text = trees[TreeFlag].Angle.ToString();
        Values[7].text = trees[TreeFlag].Length.ToString();
        Values[8].text = trees[TreeFlag].Width.ToString();
        Values[10].text= trees[TreeFlag].iteration.ToString();
        if (trees[TreeFlag].color == Color.green)
        {
            Values[9].text = "green";
        }else if(trees[TreeFlag].color == Color.red)
        {
            Values[9].text = "red";
        }else if (trees[TreeFlag].color == Color.blue)
        {
            Values[9].text = "blue";
        }
        Values[0].text = (TreeFlag+1).ToString();
    }

    private void Setvalue()
    {
        trees[TreeFlag].Axiom = Values[1].text;
        if (Values[2].text == String.Empty)
        {
            trees[TreeFlag].Rules2_1 = char.MinValue;
        }
       else
        {
            trees[TreeFlag].Rules1_1 = char.Parse(Values[2].text);
        }

        trees[TreeFlag].Rules1 = Values[3].text;

        if (Values[4].text == String.Empty)
        {
            trees[TreeFlag].Rules2_1 = char.MinValue;
        }
        else if (Values[4].text == Values[2].text)
        {
            trees[TreeFlag].Rules2_1 = char.MinValue;
            UIwarning.gameObject.SetActive(true);
            Values[4].text = String.Empty;
            StartCoroutine(UIdisappear());
        }
        else
        {
            trees[TreeFlag].Rules2_1 = char.Parse(Values[4].text);
        }
        trees[TreeFlag].Rules2 =  Values[5].text;
        trees[TreeFlag].Angle = float.Parse(Values[6].text);
        trees[TreeFlag].Length = float.Parse(Values[7].text);
        trees[TreeFlag].Width = float.Parse(Values[8].text);
        trees[TreeFlag].iteration = int.Parse(Values[10].text);
        if (Values[9].text == "green")
        {
            trees[TreeFlag].color = Color.green;
        }
        else if (Values[9].text == "red")
        {
            trees[TreeFlag].color = Color.red;
        }
        else if (Values[9].text == "blue")
        {
            trees[TreeFlag].color = Color.blue;
        }
    }

    IEnumerator UIdisappear()
    {
        yield return new WaitForSeconds(2f);
        UIwarning.SetActive(false);

    }
}
