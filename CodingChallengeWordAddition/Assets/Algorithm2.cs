using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Algorithm2 : MonoBehaviour {
    
    struct state
    {
        public int S;
        public int E;
        public int V;
        public int N;
        public int I;
        public int X;
        public int T;
        public int W;
        public int Y;
    }
	int totalStatesChecked = 0, totalTrue = 0;
	float runTime = 0.0f;
	List<state> trueStates;
	// Use this for initialization
	void Start () 
    {

        trueStates = new List<state>();

        //calculate possible states.
        int possibleStates = 5;
        Debug.Log(possibleStates + " " + 0);
        for (int i = 1; i < 9; ++i)
        {
            possibleStates *= (10 - i);
            Debug.Log(possibleStates + " " + i);
        }

		/*Known true answer
		 * S  E  V  N  I  X  T  W  Y
        if (Calculate(6, 8, 7, 2, 5, 0, 1, 3, 4))
        {
            Debug.Log("troof be that");
        }
        else
        {
            Debug.Log("opps");
        }
		 */

        StartCoroutine(Assign (5));
		StartCoroutine(Assign (6));
		StartCoroutine(Assign (7));
		StartCoroutine(Assign (8));
		StartCoroutine(Assign (9));
	}
	
	// Update is called once per frame
	void Update () {
        runTime += Time.deltaTime;
        float perMinute = totalStatesChecked / (runTime / 60.0f);
        Debug.Log("RunTime: " + runTime + "\n Checks per Minute:" + perMinute);
	}

    IEnumerator Assign(int S)
    {
		bool[] available = new bool[10];
		for (int i = 0; i < 10; i++)
		{
			available[i] = true;
		}
		List<int> intAvailable = new List<int>();
		for (int i = 0; i < 10; i++)
		{
			intAvailable.Add(i);
		}


						available [S] = false;
						intAvailable.Remove (S);

						for (int E = 0; E < 10; ++E) {
								if (!available [E] || (E > 0 && E < 8)) {
										continue;
								}
								available [E] = false;
			
								for (int V = 0; V < 10; ++V) {
										if (!available [V]) {
												continue;
										}
										available [V] = false;
			
										for (int N = 0; N < 10; ++N) {
												if (!available [N]) {
														continue;
												}
												available [N] = false;
												for (int I = 0; I < 10; ++I) {
														if (!available [I]) {
																continue;
														}
														available [I] = false;
														for (int X = 0; X < 10; ++X) {
																if (!available [X]) {
																		continue;
																}
																available [X] = false;
																for (int T = 0; T < 10; ++T) {
																		if (!available [T]) {
																				continue;
																		}
																		available [T] = false;
																		for (int W = 0; W < 10; ++W) {
																				if (!available [W]) {
																						continue;
																				}
																				available [W] = false;
																				for (int Y = 0; Y < 10; ++Y) {
																						if (!available [Y]) {
																								continue;
																						}
																						available [Y] = false;
			
																						if (Calculate (S, E, V, N, I, X, T, W, Y)) {
																								totalTrue++;
																								state tempState = new state ();
																								tempState.S = S;
																								tempState.E = E;
																								tempState.V = V;
																								tempState.N = N;
																								tempState.I = I;
																								tempState.X = X;
																								tempState.T = T;
																								tempState.W = W;
																								tempState.Y = Y;
																								trueStates.Add (tempState);
																						}
																						totalStatesChecked++;
																						Debug.Log ("S: " + S);
																						Debug.Log ("E: " + E);
																						Debug.Log ("V: " + V);
																						Debug.Log ("N: " + N);
																						Debug.Log ("I: " + I); 
																						Debug.Log ("X: " + X);
																						Debug.Log ("T: " + T);
																						Debug.Log ("W: " + W);
																						Debug.Log ("Y: " + Y); 
																						Debug.Log ("Troofs: " + totalTrue);
																						Debug.Log ("TotalChecked: " + totalStatesChecked);
																						
																						available [Y] = true;
																						intAvailable.Add (Y);
																						yield return null;
																						//Debug.Log("Y");
																				}
																				available [W] = true;
																				intAvailable.Add (W);
																				//Debug.Log("W");
																		}
																		available [T] = true;
																		intAvailable.Add (T);
																		//Debug.Log("T");
																}
																available [X] = true;
																intAvailable.Add (X);
																//Debug.Log("X");
														}
														available [I] = true;
														intAvailable.Add (I);
														//Debug.Log("I");
												}
												available [N] = true;
												intAvailable.Add (N);
												//Debug.Log("N");
										}
										available [V] = true;
										intAvailable.Add (V);
										//Debug.Log("V");
								}
								available [E] = true;
								intAvailable.Add (E);
								//Debug.Log("E");
						}
						available [S] = true;
						intAvailable.Add (S);
						//Debug.Log("S");
				
		
		yield return null;
        
    }
    bool Calculate(int S, int E, int V, int N, int I, int X, int T, int W, int Y)
    {
        //first start with sure to fail states.
        if (S < 5 || T > 2 || (E <= 0 && E >= 8)) { return false; }  //5 possible states for for S, 6 for E  

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

