using UnityEngine;

public static class Numpy {
  
  public static void print(float[] array) {
    Debug.Log("Array print:");
    string Line = "";
    for (int i = 0 ; i < array.Length; i++) {
      Line += array[i] + " ";
    }
    Debug.Log(Line);
    Debug.Log("Array printed.");
  }

  public static void print(float[][] array) {
    Debug.Log("Matrix print:");
    for (int i = 0 ; i < array.Length; i++) {
      string Line = "";
      for (int j = 0 ; j < array[i].Length; j++) {
        Line += array[i][j] + " ";
      }
      Debug.Log(Line);
    }
    Debug.Log("Matrix printed.");
  }

  public static float[] rand(int size) {
    float[] array = new float[size];

    for (int i = 0 ; i < size; i++) {
      array[i] = Random.Range(-1.0f, 1.0f);
    }

    return array;
  }
  
  public static float[][] rand(int previousLayerNeurons, int neurons) {
    float[][] matrix = new float[previousLayerNeurons][];

    for (int i = 0 ; i < previousLayerNeurons; i++) {
      matrix[i] = new float[neurons];
      for (int j = 0 ; j < neurons; j++) {
        matrix[i][j] = Random.Range(-1.0f, 1.0f);
      }
    }

    return matrix;
  }
  
  public static float[] dot(float[] inputs, float[][] weights) {
    float[] output = new float[weights[0].Length];

    for (int i = 0 ; i < output.Length; i++) {
      output[i] = 0f;
      for (int j = 0 ; j < inputs.Length; j++) {
        output[i] += inputs[j] * weights[j][i];
      }
    }

    return output;
  }

  public static float[] addBias(float[] output, float[] bias) {
    float[] newOutput = new float[output.Length];

    for (int i = 0 ; i < output.Length; i++) {
      newOutput[i] = output[i] + bias[i];
    }

    return newOutput;
  }

  public static float[] forward(float[] inputs, float[][] weights, float[] bias) {
    float[] output = addBias(dot(inputs,weights), bias);
    for (int i = 0 ; i < output.Length ; i++) {
      if (output[i] < 0f) output[i] = 0f;
    }
    return output;
  }

  public static float[] merge(float[] arr1, float[] arr2) {
    float[] mergedArr = new float[arr1.Length];
    for (int i = 0 ; i < arr1.Length ; i++) {
      mergedArr[i] = (arr1[i] + arr2[i]) / 2;
    }
    return mergedArr;
  }

  public static float[][] merge(float[][] matrix1, float[][] matrix2) {
    float[][] mergedMatrix = new float[matrix1.Length][];
    for (int i = 0 ; i < matrix1.Length ; i++) {
      mergedMatrix[i] = merge(matrix1[i], matrix2[i]);
      /*
      mergedMatrix[i] = new float[matrix1[i].Length];
      for (int j = 0 ; j < matrix1[i].Length ; j++) {
        mergedMatrix[i][j] = (matrix1[i][j] + matrix2[i][j]) / 2;
      }
      */
    }
    return mergedMatrix;
  }
}