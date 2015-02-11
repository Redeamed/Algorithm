using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Algorithm3 : MonoBehaviour {
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


        for (T = 1; T < 3; ++T)
        {
            
            available[T] = false;
            for (E = 0; E < 10; ++E)
            {
                if (!available[E])
                {
                    if (E == 0) { E = 7; }//if E == 0 jump passed impossible middle values; 7 not 8 so after continue it will increment to 8
                    continue;
                }
                available[E] = false;

            for (N = 1; N < 10; ++N)//N != 0 of X would need to = Y. start at 1 to avoid this.
            {
                if (!available[N])
                {
                    if (N != 9 && !available[N + 1]) { N++; }
                    continue;
                }
                available[N] = false;

                for (X = 0; X < 10; ++X)
                {
                    if (!available[X])
                    {
                        if (X != 9 && !available[X + 1]) { X++; }
                        continue;
                    }
                    available[X] = false;

                    Y = ((N*2) + X) % 10; //this is neccessarily true of all troofs
                        if (!available[Y])
                        {

                            available[X] = true;
                            continue;
                        }
                        available[Y] = false;
            for (S = 0; S < 10; ++S)
            {
                if (!available[S] || (T == 0 && S > 4) || ((T != 0 && S <= 4)))//if S > 4 T must be at least 1
                {
                    if (T == 0 && S > 4) { S = 10; }
                    else if (T != 0 && S <= 4) { S = 4; }
                    else if (S != 9 && !available[S + 1]) { S++; }
                    continue; 
                }

                available[S] = false;

                
                    for (V = 0; V < 10; ++V)
                    {
                        if (!available[V])
                        {
                            if (V != 9 && !available[V + 1]) { V++; }
                            continue;
                        }
                        available[V] = false;

                            for (I = 0; I < 10; ++I)
                            {
                                if (!available[I])
                                {
                                    if (I != 9 && !available[I + 1]) { I++; }
                                    continue;
                                }
                                available[I] = false;
                                
                                    for (W = 0; W < 10; ++W)
                                    {
                                        if (!available[W])
                                        {
                                            if (W !=9 && !available[W + 1]) { W++; }
                                            continue;
                                        }
                                        available[W] = false;

                                            if (Calculate(S, E, V, N, I, X, T, W, Y))
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

                                        available[W] = true;

                                        yield return null;
                                    }
                                    available[I] = true;
                                }
                                available[V] = true;
                            }
                        available[S] = true;
                    }
                    available[Y] = true;
                    available[X] = true;
                }
                available[N] = true;
            }
            available[E] = true;
            }
            available[T] = true;
        }
				
		
		yield return null;
        
    }
    bool Calculate(int S, int E, int V, int N, int I, int X, int T, int W, int Y)
    {
        //check each columb right to lest.
        float check1 = N + N + X;
        if (check1 % 10 != Y){ return false;}

        float check2 = E + E + I;
        if (check1 > 10) { check2+= (check1 - Y)/10; }
        if (check2 % 10 != T) { return false; }

        float check3 = V + V + S;
		if (check2 > 10) { check3+= (check2 - T)/10; }
		if (check3 % 10 != N) { return false; }

        float check4 = E + E;
		if (check3 > 10) { check4+= (check3 - N)/10; }
		if (check4 % 10 != E) { return false; }

        float check5 = S+S;
		if (check4 > 10) { check5+= (check4 - E)/10; }
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

