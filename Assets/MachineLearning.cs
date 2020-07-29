using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineLearning : MonoBehaviour {
    public int population;
    public int save;
    public GameObject agentPrefab;
    private GameObject[] agents;
    private GameObject[] newAgents;
    private int generation;

    public GameObject destination;


    private int timeLeft = 200;

    void Start() {
        CreateGen0();
        generation = 0;
    }

    void CreateGen0() {
        agents = new GameObject[population];
        for (int i = 0 ; i < population ; i++) {
            agents[i] = Instantiate(agentPrefab);
            agents[i].GetComponent<Agent>().RandBrains();
        }
    }

    float DistanceToGoal(GameObject agent) {
        return Vector3.Distance(agent.transform.position, destination.transform.position);
    }

    void NewGeneration() {
        generation++;
        Debug.Log(generation);
        newAgents = new GameObject[population];
        for (int i = 0 ; i < population ; i++) {
            newAgents[i] = Instantiate(agentPrefab);
            newAgents[i].GetComponent<Agent>().RandBrains();
        }

        GameObject temp;
        for (int j = 0; j <= population - 2; j++) {
            for (int i = 0; i <= population - 2; i++) {
                if (DistanceToGoal(agents[i]) > DistanceToGoal(agents[i + 1])) {
                    temp = agents[i + 1];
                    agents[i + 1] = agents[i];
                    agents[i] = temp;
                }
            }
        }

        int agentNum = 0;
        for (int i = 0 ; i < save ; i++) {
            for (int j = i ; j < save ; j++) {
                newAgents[agentNum].GetComponent<Agent>().MergeBrains(agents[i].GetComponent<Agent>().brain, agents[j].GetComponent<Agent>().brain);
                agentNum++;
            } 
        } 

        if (generation % 10 == 0) {
            for (int i = 20 ; i < population ; i++) {
                newAgents[i].SetActive(false);
            }
        }

        for (int i = 0 ; i < population ; i++) {
            Destroy(agents[i]);
        }

        agents = newAgents;

        /*
        agents[agentNum].GetComponent<Agent>().MergeBrains(oldAgents[i].GetComponent<Agent>().brain, oldAgents[j].GetComponent<Agent>().brain);
        agents[i].GetComponent<Agent>().RandBrains();
        agents[i].GetComponent<Agent>().Reset();
        */
    }

    void FixedUpdate() {
        timeLeft--;

        if (timeLeft <= 0) {
            timeLeft = 200;
            NewGeneration();
            return;
        }

        for (int i = 0 ; i < agents.Length ; i++) {
            agents[i].GetComponent<Agent>().Forward(GetSensors(agents[i]));
        }
    }

    float[] GetSensors(GameObject agent) {
        float[] inputs = new float[2];
        inputs[0] = agent.transform.position.x - destination.transform.position.x;
        inputs[1] = agent.transform.position.z - destination.transform.position.z;
        return inputs;
    }
}
