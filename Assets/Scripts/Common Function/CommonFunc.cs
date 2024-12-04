using System;
using System.Text;
using System.IO;
using UnityEngine;

public class CommonFunc
{
    public int SizeOfRabbit { get; }
    public int[] NeedCarrot { get; set; }
    public int[] RabbitX { get; set; }
    public int[] RabbitY { get; set; }
    public int[] RabbitW { get; set; }
    public int[] RabbitH { get; set; }

    public string[] RabbitName { get; set; }
    public string[] Passive { get; set; }

    public StringBuilder Sb { get; set; } = new StringBuilder(100);

    public StreamReader DataReader { get; set; } = new StreamReader("Assets/DataBases/Rabbit.csv");

    public CommonFunc()
    {
        SizeOfRabbit = 5;
        NeedCarrot = new int[SizeOfRabbit];
        RabbitX = new int[SizeOfRabbit];
        RabbitY = new int[SizeOfRabbit];
        RabbitW = new int[SizeOfRabbit];
        RabbitH = new int[SizeOfRabbit];
        RabbitName = new string[SizeOfRabbit];
        Passive = new string[SizeOfRabbit];
        string line;
        string[] data;
        while (!DataReader.EndOfStream)
        {
            line = DataReader.ReadLine();
            data = line.Split(',');
            switch (data[0])
            {
                case "이름":
                    for (int i = 0; i < SizeOfRabbit; i++)
                        RabbitName[i] = data[i + 1];
                    break;
                case "당근량":
                    for (int i = 0; i < SizeOfRabbit; i++)
                        NeedCarrot[i] = int.Parse(data[i + 1]);
                    break;
                case "패시브":
                    for (int i = 0; i < SizeOfRabbit; i++)
                        Passive[i] = data[i + 1];
                    break;
                case "X 좌표":
                    for (int i = 0; i < SizeOfRabbit; i++)
                        RabbitX[i] = int.Parse(data[i + 1]);
                    break;
                case "Y 좌표":
                    for (int i = 0; i < SizeOfRabbit; i++)
                        RabbitY[i] = int.Parse(data[i + 1]);
                    break;
                case "너비":
                    for (int i = 0; i < SizeOfRabbit; i++)
                        RabbitW[i] = int.Parse(data[i + 1]);
                    break;
                case "높이":
                    for (int i = 0; i < SizeOfRabbit; i++)
                        RabbitH[i] = int.Parse(data[i + 1]);
                    break;
                default:
                    break;
            }
        }
        DataReader.Close();
    }

    public string SbConcat(string[] str)
    {
        Sb.Clear();
        for (int i = 0; i < str.Length; i++)
            Sb.Append(str[i]);
        return Sb.ToString();
    }

    public float Ceil(float n)
    {
        return n - ((n % 1) - 1);
    }
}