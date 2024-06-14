using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SelectionSortScript : MonoBehaviour
{
    [SerializeField] private int arraySize = 100;
    void Start()
    {
        int[] myArray = GenerateArray(arraySize);
        Debug.Log("Несортированный массив:");
        DisplayArray(myArray);
        myArray = SelectionSort(myArray);
        Debug.Log("Сортированный массив:");
        DisplayArray(myArray);
    }

    private int[] GenerateArray(int length)
    {
        int[] array = new int[length];
        Random rnd = new Random();
        for (int i = 0; i < length; i++)
        {
            array[i] = rnd.Next(-100, 101);
        }
        return array;
    }
    private void DisplayArray(int[] array)
    {
        foreach (var x in array)
        {
            Debug.Log(x);
        }
    }
    private int[] SelectionSort(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int minElementIndex = i;
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j] < array[minElementIndex])
                {
                    minElementIndex = j;
                }
            }
            int temp = array[i];
            array[i] = array[minElementIndex];
            array[minElementIndex] = temp;
        }
        return array;
    }
}
