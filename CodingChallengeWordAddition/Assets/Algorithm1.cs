using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Algorithm1 : MonoBehaviour {
    struct Variable 
    {
        public char letter;
        public int value;
    }

    Variable[] variable;
    int totalStatesChecked = 0, totalTrue = 0;
    List<int> valueAvailable;
    List<int> valueUsed;
    float runTime = 0.0f;
	// Use this for initialization
	void Start () 
    {
        valueAvailable = new List<int>();
        valueUsed = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            valueAvailable.Add(i);
        }

            variable = new Variable[9];
        ///possible letters S,E,V,N,I,X,T,W,Y
        variable[0].letter = 'S';
        variable[1].letter = 'E';
        variable[2].letter = 'V';
        variable[3].letter = 'N';
        variable[4].letter = 'I';
        variable[5].letter = 'X';
        variable[6].letter = 'T';
        variable[7].letter = 'W';
        variable[8].letter = 'Y';

        //calculate possible states.
        int possibleStates = 5;
        Debug.Log(possibleStates + " " + 0);
        for (int i = 1; i < 9; ++i)
        {
            possibleStates *= (10 - i);
            Debug.Log(possibleStates + " " + i);
        }


        StartCoroutine("Assign");
	}
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator Assign() 
    {

        for (int a = 5; a < 10; ++a)
        {
            variable[0].value = valueAvailable[a];
            Use(a);
            for (int b = 0; b < 9; ++b)
            {
                variable[1].value = valueAvailable[0];
                Use(0);

                for (int c = 0; c < 8; ++c)
                {
                    variable[2].value = valueAvailable[0];
                    Use(0);
                    for (int d = 0; d < 7; ++d)
                    {
                        variable[3].value = valueAvailable[0];
                        Use(0);
                        for (int e = 0; e < 6; ++e)
                        {
                            variable[4].value = valueAvailable[0];
                            Use(0);
                            for (int f = 0; f < 5; ++f)
                            {
                                variable[5].value = valueAvailable[0];
                                Use(0);

                                for (int g = 0; g < 4; ++g)
                                {

                                    variable[6].value = valueAvailable[0];
                                    Use(0);

                                    for (int h = 0; h < 3; ++h)
                                    {

                                        variable[7].value = valueAvailable[0];
                                        Use(0);
                                        for (int i = 0; i < 2; ++i)
                                        {

                                            variable[8].value = valueAvailable[0];
                                            Use(0);
                                            for (int z = 0; z < variable.Length; ++z)
                                            {
                                                Debug.Log(variable[z].letter + ": " + variable[z].value + "\n");
                                            }
                                                Calculate();
                                            yield return null;

                                            notUse(i);
                                           
                                        }

                                        notUse(h);
                                    }
                                    notUse(g);
                                }
                                notUse(f);
                            }
                            notUse(e);
                        }
                        notUse(d);
                    }
                    notUse(c);
                }
                notUse(b);
            }
            notUse(a);

        }
        yield return null;
    }
    void Use(int i)
    {
        if (i > valueAvailable.Count) { Debug.Log("Use Failed: " + i + " > " + valueAvailable.Count); }
        valueUsed.Add(valueAvailable[i]);
        valueAvailable.Remove(valueAvailable[i]);
        
    }
    void notUse(int i)
    {
        int temp = valueUsed.Count - 1;
        valueAvailable.Add(valueUsed[temp]);
        valueUsed.Remove(valueUsed[temp]);
    }

    void Calculate()
    {
        float Seven = createSeven();
        float Six = createSix();
        float Twenty = createTwenty();
        float result = (Seven * 2) + Six;
        Debug.Log(Seven + " + " + Seven + " + " + Six + " = " + result);
        if (result == Twenty)
        {
            Debug.Log(result + " == " + Twenty);
            totalTrue++;
        }
        else
        {
            Debug.Log(result + " != " + Twenty);
        }
        totalStatesChecked++;
        Debug.Log("True: " + totalTrue + "\n Total: " + totalStatesChecked);
        runTime += Time.deltaTime;
        float perMinute = totalStatesChecked / (runTime / 60.0f);
        Debug.Log("RunTime: " + runTime + "\n Checks per Minute:" + perMinute);
    }

    float createSeven()
    {
        
        ///possible letters S,E,V,N,I,X,T,W,Y
        float seven = variable[0].value * Mathf.Pow(10, 4);
        seven += variable[1].value * (Mathf.Pow(10, 3));
        seven += variable[2].value * (Mathf.Pow(10, 2));
        seven += variable[1].value * 10;
        seven += variable[3].value;
        return seven; 
    }
    float createSix()
    {
        ///possible letters S,E,V,N,I,X,T,W,Y
        float six = variable[0].value * (Mathf.Pow(10, 2));
        six += variable[4].value * 10;
        six += variable[5].value;
        return six;
    }
    float createTwenty()
    {
        ///possible letters S,E,V,N,I,X,T,W,Y
        float twenty = variable[6].value * (Mathf.Pow(10, 5));
        twenty += variable[7].value * (Mathf.Pow(10, 4));
        twenty += variable[1].value * (Mathf.Pow(10, 3));
        twenty += variable[3].value * (Mathf.Pow(10, 2));
        twenty += variable[7].value * 10;
        twenty += variable[8].value;
        return twenty;
    }

}

