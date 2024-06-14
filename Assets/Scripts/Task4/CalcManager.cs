using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalcManager : MonoBehaviour
{

    [SerializeField] private Text screenText;
	private string firstNum = "0";
	private string secondNum = "";
	private string operatorSign = "";
	private char[] operatorsArray = new char[] {'+', '-', 'x', '/'};

	void Start()
	{
		if (screenText != null) 
		{
			screenText.text = "0";
		}
	}
	public void OnButtonClick()
	{
		GameObject currentButton = EventSystem.current.currentSelectedGameObject;
		if (currentButton != null)
		{
			Text buttonText = currentButton.GetComponentInChildren<Text>();
			if (buttonText != null)
			{
				ManageInput(buttonText.text);
			}
		}
	}
	private void ManageInput(string inputVal)
	{
		char inputChar = inputVal[0];
		if (inputChar >= '0' && inputChar <= '9')
		{
			if (string.IsNullOrEmpty(operatorSign))
			{
				if (firstNum == "0")
				{
					firstNum = inputVal;
				}
				else if (firstNum == "-0")
				{
					firstNum = "-" + inputVal;
				}
				else
				{
					firstNum += inputVal;
				}
			}
			else
			{
				secondNum += inputVal;
			}
		}
		else if (inputChar == '(')
		{
			if (string.IsNullOrEmpty(operatorSign))
			{
				if (firstNum.Contains('-'))
				{
					firstNum = firstNum.Substring(1);
				}
				else
				{
					firstNum = "-" + firstNum;
				}
			}
			else 
			{
				if (secondNum.Contains('-'))
				{
					secondNum = secondNum.Substring(1);
				}
				else 
				{ 
					secondNum = "-" + secondNum;
				}
			}
		}
		else if (inputChar == '<')
		{
			if (string.IsNullOrEmpty(operatorSign))
			{
				if (!string.IsNullOrEmpty(firstNum))
				{
					firstNum = firstNum.Substring(0, firstNum.Length - 1);
					if (firstNum == "-" || string.IsNullOrEmpty(firstNum))
					{
						firstNum = "0";
					}
				}
			}
			else 
			{
				if (string.IsNullOrEmpty(secondNum))
				{
					operatorSign = "";
				}
				else
				{
					secondNum = secondNum.Substring(0, secondNum.Length - 1);
				}
			}
		}
		else if (inputChar == ',')
		{
			if (string.IsNullOrEmpty(operatorSign))
			{
				if (!firstNum.Contains('.'))
				{
					firstNum += ".";
				}
			}
			else if (!secondNum.Contains('.'))
			{
				if (string.IsNullOrEmpty(secondNum))
				{
					secondNum = "0.";
				}
				else if (secondNum == "-")
				{
					secondNum = "-0.";
				}
				else
				{
					secondNum += ".";
				}
			}
		}
		else if (inputChar == 'C') 
		{
			firstNum = "0";
			secondNum = "";
			operatorSign = "";
		}
		else if (operatorsArray.Contains(inputChar))
		{
			Evaluate();
			operatorSign = inputVal;
		}
		else 
		{
			Evaluate();
		}

		if (screenText != null)
		{
			screenText.text = firstNum + operatorSign + secondNum;
		}
	}
	private void Evaluate()
	{
		if (string.IsNullOrWhiteSpace(secondNum) || secondNum == "-")
		{
			return;
		}
		if (operatorSign == "/" && secondNum == "0")
		{
			operatorSign = "";
			secondNum = "";
			return;
		}
		switch (operatorSign)
		{
			case "-":
				firstNum = (float.Parse(firstNum, CultureInfo.InvariantCulture) - float.Parse(secondNum, CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
				break;
			case "+":
				firstNum = (float.Parse(firstNum, CultureInfo.InvariantCulture) + float.Parse(secondNum, CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
				break;
			case "x":
				firstNum = (float.Parse(firstNum, CultureInfo.InvariantCulture) * float.Parse(secondNum, CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
				break;
			case "/":
				firstNum = (float.Parse(firstNum, CultureInfo.InvariantCulture) / float.Parse(secondNum, CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
				break;
		}
		operatorSign = "";
		secondNum = "";
	}
}