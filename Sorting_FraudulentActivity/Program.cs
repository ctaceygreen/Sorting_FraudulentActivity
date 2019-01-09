using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution
{

    // Complete the activityNotifications function below.
    static int activityNotifications(int[] expenditure, int d)
    {
        int notificationCount = 0;
        bool dIsEven = d % 2 == 0;
        int[] frequencyArray = new int[201];
        for(int i = 0; i < d; i++)
        {
            frequencyArray[expenditure[i]]++;
            
        }

        for(int startIndex = d; startIndex < expenditure.Length; startIndex++)
        {
            double median = GetMedian(frequencyArray, d, dIsEven);
            if(expenditure[startIndex] >= 2*median)
            {
                notificationCount++;
            }

            //Update frequency array by removing old and adding new
            frequencyArray[expenditure[startIndex - d]]--;
            frequencyArray[expenditure[startIndex]]++;
        }
        return notificationCount;
    }

    static double GetMedian(int[] freqArray, int d, bool dIsEven)
    {
        int[] cumulativeArray = new int[201];
        cumulativeArray[0] = freqArray[0];
        for(int i = 1; i < freqArray.Length; i++)
        {
            cumulativeArray[i] = freqArray[i] + cumulativeArray[i - 1];
        }
        double median = 0;
        if(dIsEven)
        {
            //Go through cum array until reach d/2 . Get this + next step / 2.0 as median
            int index = 0;
            int first = 0;
            int second = 0;
            while(index < cumulativeArray.Length)
            {
                if(cumulativeArray[index] >= d/2)
                {
                    first = index;
                    break;
                }
                index++;
            }
            while(index < cumulativeArray.Length)
            {
                if(cumulativeArray[index] >= d/2 + 1)
                {
                    second = index;
                    break;
                }
                index++;
            }
            median = (first + second) / 2.0;
        }
        else
        {
            int index = 0;
            while(index < cumulativeArray.Length)
            {
                if(cumulativeArray[index] >= d/2 + 1)
                {
                    median = index;
                    break;
                }
                index++;
            }
        }
        return median;
    }

    static void Main(string[] args)
    {
        //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nd = Console.ReadLine().Split(' ');

        int n = Convert.ToInt32(nd[0]);

        int d = Convert.ToInt32(nd[1]);

        int[] expenditure = Array.ConvertAll(Console.ReadLine().Split(' '), expenditureTemp => Convert.ToInt32(expenditureTemp))
        ;
        int result = activityNotifications(expenditure, d);

        //textWriter.WriteLine(result);

        //textWriter.Flush();
        //textWriter.Close();
    }
}
