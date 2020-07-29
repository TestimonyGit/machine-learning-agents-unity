using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {
  public Brain brain;

  public void RandBrains() {
    brain = new Brain();
    brain.Init();
  }

  public void MergeBrains(Brain brain1, Brain brain2) {
    brain.Merge(brain1, brain2);
  }

  public void Reset() {
    this.transform.position = new Vector3(0, 0.5f, -10f);
  }

  public void Forward(float[] inputs) {
    Act(brain.GetOutput(inputs));
  }

  private void Act(float[] outputs) {
    this.transform.position += new Vector3(outputs[0] * 0.05f, 0, outputs[1] * 0.05f);
  }
}



public class Brain {
  public float[][] weightsLayer1;
  public float[] biasesLayer1;
  public float[][] weightsLayer2;
  public float[] biasesLayer2;
  public float[][] outputWeights;

  public Brain() {

  }

  public void Init() {
    weightsLayer1 = Numpy.rand(2, 20);
    biasesLayer1 = Numpy.rand(20);
    weightsLayer2 = Numpy.rand(20, 20);
    biasesLayer2 = Numpy.rand(20);
    outputWeights = Numpy.rand(20, 2);
  }

  public void Merge(Brain brain1, Brain brain2) {
    weightsLayer1 = Numpy.merge(brain1.weightsLayer1, brain2.weightsLayer1);
    biasesLayer1 = Numpy.merge(brain1.biasesLayer1, brain2.biasesLayer1);
    weightsLayer2 = Numpy.merge(brain1.weightsLayer2, brain2.weightsLayer2);
    biasesLayer2 = Numpy.merge(brain1.biasesLayer2, brain2.biasesLayer2);
    outputWeights = Numpy.merge(brain1.outputWeights, brain2.outputWeights);
  }

  public float[] GetOutput(float[] inputs) {
    float[] outputLayer1 = Numpy.forward(inputs, weightsLayer1, biasesLayer1);
    float[] outputLayer2 = Numpy.forward(outputLayer1, weightsLayer2, biasesLayer2);
    float[] output = Numpy.dot(outputLayer2, outputWeights);
    return output;
  }
}