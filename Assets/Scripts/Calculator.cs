using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Calculator : MonoBehaviour
{
    #region fields

    public TextMeshProUGUI inputText;
    private float _number0 = 0, _input = 0;
    private double _input1 =0, _input2 = 0, _result = 0;
    private string _operator , _text;
    private int _number1 = 0, _period = 0, _i = 0, _j=0, _k=0, _r=0, _c=0, _o=0, _z = 0;
    /*
    _number0 - ��������������� � ���� �����
    _input - ��������������� � ���� �����
    _input1 - ����� �����
    _input2 - ����� �����
    _result - ��������� ��������
    _operator - �������� (���� ������)
    _text - �� ����, �� inputText.text
    _number1 - ������� ������� �� �����
    _period - ������, ���� ������, �� ����� � ���������� ������
     
    ����, �� ����������� ���� ������ - �������:
    _i - ������, �� ��� ������������ ����� ��������, �� �
    _j - ������, �� ���� �������: ��� �����, �� ��� �� ��������
    _k - ������, ������� ����� ��� ClickRemove, ���� �� ��������� ������ ������ (����������� �����), ���� ����� �� ���������
    _r - ������, �� ��������, �� �������� ���� ��������� � ������, �� ������ ������, � ������ ��� ��� ClickRemove ������ ���������� ����� ����� �� ���� ���
    _c - ������, �� ���� ��������� 䳺� ���� ����� (ClickChange)
    _o - ������, ������� ����� �� ������ ������ ��������, ���������� �� ������� �� ���
    _z - ������� �� ��, ��� ������� �� �������� ����� ���� ������;

    */
    #endregion fields

    #region Methods

    public void ClickReset()//������� ����� ��� (������ �)
    {
        Debug.Log(message: $" reset");
        inputText.text = $"";
        _input = 0;
        _input1 = 0;
        _input2 = 0;
        _result = 0;
        _number0 = 0;
        _period = 0;
        _i = 0;
        _k = 0;
        _r = 0;
        _c = 0;
        _o = 0;
        _z = 0;
    }
    public void ClickRemove()//������ ���� ����/�����/��������� ��
    { 
        if (_j == 0 && _k!=_period-1)
        {
            if (_period != 0)
            {
                _k++;
            }
            _number0 = (_number0 - (_number0 % 10)) / 10;
            
        }
        _text = inputText.text;
        _number1 = _text.Length;

        if (_number1 - 1 <= 0)
        {
            inputText.text = $"";
            return;
        }
        _text = _text.Remove(_text.Length - 1);
        inputText.text = _text;
        if (_r == 1 || _c != 0)
        {
            _text = inputText.text;
            _number1 = _text.Length;

            if (_number1 - 1 <= 0)
            {
                inputText.text = $"";
                return;
            }
            _text = _text.Remove(_text.Length - 1);
            inputText.text = _text;
            _r = 0;
            
        }
        if (_r == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                _text = inputText.text;
                _number1 = _text.Length;

                if (_number1 - 1 <= 0)
                {
                    inputText.text = $"";
                    return;
                }
                _text = _text.Remove(_text.Length - 1);
                inputText.text = _text;
            }
            _r = 0;
            
        }
        if (_j != 0)
        {
            _o = 0;
        }
        _r = 0;
        _z = 0;
    }

    public void ClickNumber(int num)//��� �����
    {
        CatchError();
        _j = 0;
        _r = 0;
        _z = 0;
        if (_c != 0)
        {
            ClickReset(); 
        }
        _c = 0;
        if (_result != 0 && _input1 == 0)
        {
                inputText.text = $"";
                _input = 0;
                _input1 = 0;
                _input2 = 0;
                _result = 0;
                _number0 = 0;
                _period = 0;
                _k = 0;
        }

        inputText.text = inputText.text + $"{num}";
        if (_period != 0)
        {
                _period++;
        }
        _input = _number0 * 10 + num;
        _number0 = _input;
        if (_k != 0 )
        {
             for (int m = 0; m < _k; m++)
             {
                _input = _input * 10;
             }
        }
        
           
    }
    public void ClickPeriod(string val)//���� �������� ���������� ���
    {
        if (_period == 0)
        {
            Debug.Log(message: $" ClickPeriod val:{val}");
            inputText.text = inputText.text + $"{val}";
            _period = 1;
        }

    }
    public void ClickChange()//���� �����
    {    
            if (_input >= 0)
            {
                inputText.text = inputText.text.Replace($"{_input}", $"(-{_input})");
                _number0 = _number0 * (-1);
            }
            else
            {
                inputText.text = inputText.text.Replace($"(-{_input})", $"{_input}");
                _number0 = _number0 * (-1);
            }
        
        _c = 1;
    }

    public void ClickOperator(string oper)//��������
    {
        CatchError();
        _z = 0;
        _c = 0;
        if (_operator == "^2" || _operator == "^3")
        {
            _r = 1;
        }
        if (_operator == "^(1/2)")
        {
            _r = 2;
        }
        if (_j != 0 && _o == 1)//���� ������� ������ ��������
        {  
            ClickRemove();
            _r = 0;     
        }
        if (oper == "^2" || oper == "^3")
        {
            _r = 1;
        }
        if (oper == "^(1/2)")
        {
            _r = 2;
        }

        if (_i != 0 && _j ==0)//���� �������� �� �����
        {
           
            InputSecond();
            if (!string.IsNullOrEmpty(_operator))
            {
                SimpleEqual();
                CatchError();
            }

         _input1 = _result;
         inputText.text = $"{_result}";
                
                
        }else//���� ����� ��������
            if(_j == 0)
         {
            _r = 0;
                if (_period != 0)
                {
                    _period = _period - 1 - _k;
                }
                if (_result == 0)
                {
                    _input1 = _number0 / Math.Pow(10, _period);
                    _number0 = 0;
                } else {
                    _input1 = _result;
                inputText.text = $"{_result}";
                _number0 = 0;
                }
            _number0 = 0;
            _period = 0;
         }
        
        
        inputText.text = inputText.text + $"{oper}";
        _operator = oper;
        
        _i++;
        _k = 0;
        _j = 1;
        _o = 1;

    }
    public void ClickEqual()//�������
    {
        
        if (_z == 0)
        {
            _o = 0;
            _i = 0;
            InputSecond();
            inputText.text = inputText.text + "=";

            if (!string.IsNullOrEmpty(_operator))
            {
                SimpleEqual();
                CatchError();
            }
            _input1 = 0;
            inputText.text = inputText.text + $"{_result}";
            
            _k = 0;
            _r = 0;
        }
        _z = 1;
    } 
    
    public void InputSecond()//�������� �������� ������� ����� - in2
    {
        if (_period != 0)
        {
            _period = _period - 1 - _k;

        }
        _input2 = _number0 / Math.Pow(10, _period);
        Debug.Log("_input2= " + _input2);
        _number0 = 0;
        _period = 0;

    }
    public void SimpleEqual()//�������� �������� � ������� �������� ���������� - res
    {
        switch (_operator)
        {
            case "+":
                _result = _input1 + _input2;
                break;
            case "-":
                _result = _input1 - _input2;
                break;
            case "*":
                _result = _input1 * _input2;
                break;
            case "^(1/2)":
                if (_input1 < 0)
                {
                    inputText.text = "Error!";
                    break;
                }
                else
                {
                    _result = Math.Sqrt(_input1);
                    break;
                }
            case "^2":

                _result = Math.Pow(_input1, 2);
                break;

            case "^3":

                _result = Math.Pow(_input1, 3);
                break;

            case "/":
                if (_input2 == 0)
                {
                    inputText.text = "Error!";
                    break;
                }
                else
                {
                    _result = _input1 / _input2;
                    break;
                }
        }
    }
    public void CatchError()//������� ����� ���� �����
    {
        if(inputText.text== $"Error!0")
        {
            ClickReset();
        }

    }
    #endregion Methods


}
