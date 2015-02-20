using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager : MonoBehaviour {
    public struct State
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
    public int possibleStates;
    public int frames = 0;
    public float fps = 0;
     public int totalStatesChecked = 0, totalTrue = 0;
    public float runTime = 0.0f;
    public float perMinute;
    public List<State> trueStates;

	// Use this for initialization
	void Start () 
    {
        trueStates = new List<State>();

        //calculate possible states.
         possibleStates = 3;//T states
         Debug.Log(possibleStates + ": T");
        possibleStates *= 9;
        Debug.Log(possibleStates + ": TN");
        possibleStates *= 8;
        Debug.Log(possibleStates + ": TNXY"); // Y no longer has its own effect on the total because it is determined
        possibleStates *= 7; //at any give time S only has 5 possible states > 4 or < 5;
        Debug.Log(possibleStates + ": TNXYS");
        possibleStates *= 6; //at any give time E only has 3 possible states 0,8,9;
        Debug.Log(possibleStates + ": TNXYSE");
        possibleStates *= 5; //the expected 10 factorial value for this depth
        Debug.Log(possibleStates + ": TNXYSEV");
        possibleStates *= 4; //the expected 10 factorial value for this depth
        Debug.Log(possibleStates + ": TNXYSEVI");
        possibleStates *= 3; //the expected 10 factorial value for this depth
        Debug.Log(possibleStates + ": TNXYSEVIW");

	}
	
	// Update is called once per frame
	void Update () 
    {
        frames++;
        fps = frames/runTime;
        runTime += Time.deltaTime;
        perMinute = totalStatesChecked / (runTime / 60.0f);
        Debug.Log("RunTime: " + runTime + "\n Checks per Minute:" + perMinute);
	}

    public void addTrueState(State state) { trueStates.Add(state); }
}
