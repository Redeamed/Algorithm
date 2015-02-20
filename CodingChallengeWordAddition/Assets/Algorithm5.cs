using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Algorithm5 : MonoBehaviour {
    public StateManager stateManager;

    public int S;
    public int E;
    public int V;
    public int N;
    public int I;
    public int X;
    public int T;
    public int W;
    public int Y;

    int[] CO = new int[5]; // carry over array;
		
	// Use this for initialization
	void Start () 
    {
 
        StartCoroutine(Assign());
	}
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator Assign()
    {
		bool[] available = new bool[10];
		for (int i = 0; i < 10; i++)
		{
			available[i] = true;
		}

        /*List<int> intAvailable = new List<int>();
		for (int i = 0; i < 10; i++)
		{
			intAvailable.Add(i);
		}*/

        //maximum carry over potential for T is 2 so only check values < 3

        for (N = 1; N < 10; ++N)//base on column 1: N != 0 as X would need to = Y. start at 1 to avoid this. 
        {
            if (N == 9)
            {
                N = N - 0;
            }
            if (!available[N])
            {
                if (N != 9 && !available[N + 1]) { N++; }//check if next state is available to reduce needed loops.
                continue;
            }
            available[N] = false;

            for (X = 0; X < 10; ++X)
            {

                if (N == 1 && X == 0) { X = 2; }//insures a low value available for T
                if (!available[X])
                {
                    if (X != 9 && !available[X + 1]) { X++; }//check if next state is available to reduce needed loops.
                    continue;
                }
                available[X] = false;

                CO[0] = ((N * 2) + X); //hold total value of first column in carry over
                Y =  CO[0] % 10; //this is neccessarily true of all true states.  
                CO[0] = (CO[0] - Y) / 10; //calculate just the carry over

                if (!available[Y])
                {

                    available[X] = true;//be sure to release X if Y fail since it will need to check its loop again
                    continue;
                }
                available[Y] = false;

                for (E = 0; E < 10; ++E)
                {
                    if (!available[E])
                    {
                        //examine column 4.  E + E + the carry over from column 3 must be 0, 8 or 9 to equal E after modulus math.
                        //So if E is available check if E is 0.
                        if (E == 0) { E = 7; }//if E == 0 jump passed impossible middle values; 7 not 8 so after continue it will increment to 8
                        continue;
                    }
                    available[E] = false;
                    for (I = 0; I < 10; ++I)
                    {
                        if (!available[I])
                        {
                            if (I != 9 && !available[I + 1]) { I++; }
                            continue;
                        }
                        available[I] = false;

                        CO[1] = ((E * 2) + I + CO[0]); //hold total value of second column in carry over
                        T = CO[1] % 10; //this is neccessarily true of all true states.  
                        CO[1] = (CO[1] - T) / 10; //calculate just the carry over

                        if (!available[T] || T > 2)
                        {
                            //any checks here?

                            available[I] = true;
                            continue;
                        }
                        available[T] = false;

                            for (S = 0; S < 10; ++S)
                            {
                                if (!available[S])
                                {
                                    if (S != 9 && !available[S + 1]) { S++; }
                                    continue;
                                }

                                available[S] = false;

                                for (V = 0; V < 10; ++V)
                                {
                                    CO[2] = (V * 2) + S + CO[1];//hold temp  column value
                                    if (!available[V] || N != CO[2] % 10)//check that modulus column value equals already calculated N;
                                    {
                                        if (V != 9 && !available[V + 1]) { V++; }
                                        continue;
                                    }
                                    CO[2] = (CO[2] - N) / 10;// calculate just the carry over for this column

                                    available[V] = false;
                                    CO[3] = (S * 2) + CO[3];
                                    W = CO[3] % 10;
                                    CO[3] = (CO[3] - W) / 10;
                                    if (!available[W] || CO[3] != T)
                                        {

                                            available[V] = true;
                                            if (W != 9 && !available[W + 1]) { W++; }
                                            continue;
                                        }
                                        available[W] = false;

                                            CheckState();

                                        available[W] = true;
                                        available[V] = true;
                                        yield return null;
                                }
                                available[S] = true;
                            }
                        available[T] = true;
                        available[I] = true;
                    }
                    available[E] = true;
                }
                available[Y] = true;
                available[X] = true;
            }
        available[N] = true;
        }

        Debug.Log("Finished");
		yield return null;
        
    }
    void CheckState()
    {
        if (Calculate())
        {
            stateManager.totalTrue++;
            StateManager.State tempState = new StateManager.State();
            tempState.S = S;
            tempState.E = E;
            tempState.V = V;
            tempState.N = N;
            tempState.I = I;
            tempState.X = X;
            tempState.T = T;
            tempState.W = W;
            tempState.Y = Y;
            stateManager.addTrueState(tempState);
        }
        stateManager.totalStatesChecked++;
        Debug.Log("T: " + T);
        Debug.Log("N: " + N);
        Debug.Log("X: " + X);
        Debug.Log("Y: " + Y);
        Debug.Log("S: " + S);
        Debug.Log("E: " + E);
        Debug.Log("V: " + V);
        Debug.Log("I: " + I);
        Debug.Log("W: " + W);
    }
    bool Calculate()
    {

        //check each columb right to lest.
        float check1 = N + N + X;
        if (check1 % 10 != Y){ return false;}

        float check2 = E + E + I + CO[0];
       if (check2 % 10 != T) { return false; }

       float check3 = V + V + S + CO[1];
        if (check3 % 10 != N) { return false; }

        float check4 = E + E + CO[2];
		if (check4 % 10 != E) { return false; }

        float check5 = S+S + CO[3];
		if (check5 % 10 != W) { return false; }

        return true;
    }

    /*float createSeven(int S,int E, int V, int N)
    {
        
        ///possible letters S,E,V,N,I,X,T,W,Y
        float seven = S * Mathf.Pow(10, 4);
        seven += E * (Mathf.Pow(10, 3));
        seven += V * (Mathf.Pow(10, 2));
        seven += E * 10;
        seven += N;
        return seven; 
    }
    float createSix(int S, int I, int X)
    {
        ///possible letters S,E,V,N,I,X,T,W,Y
        float six = S * (Mathf.Pow(10, 2));
        six += I * 10;
        six += X;
        return six;
    }
    float createTwenty(int T, int W, int E, int N, int Y)
    {
        ///possible letters S,E,V,N,I,X,T,W,Y
        float twenty = T * (Mathf.Pow(10, 5));
        twenty += W * (Mathf.Pow(10, 4));
        twenty += E * (Mathf.Pow(10, 3));
        twenty += N * (Mathf.Pow(10, 2));
        twenty += T * 10;
        twenty += Y;
        return twenty;
    }*/

}

