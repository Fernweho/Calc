using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Calculator : MonoBehaviour
{
    #region fields

    public TextMeshProUGUI inputText;
    private float _num0 = 0, _in = 0;
    private double _in1 =0, _in2 = 0, _res = 0;
    private string _oper , _text;
    private int _num1 = 0, _per = 0, _i = 0, _j=0, _k=0, _r=0, _c=0, _o=0;
   
    #endregion fields

    #region Methods
 
    //reset
    public void ClickReset()
    {
        Debug.Log(message: $" reset");
        inputText.text = $"";
        _in = 0;
        _in1 = 0;
        _in2 = 0;
        _res = 0;
        _num0 = 0;
        _per = 0;
        _i = 0;
        _k = 0;
        _r = 0;
        _c = 0;
        _o = 0;
    }

    public void ClickRemove()
    {
        
        if (_j == 0 && _k!=_per-1)
        {
            if (_per != 0)
            {
                _k++;
     
            }
            _num0 = (_num0 - (_num0 % 10)) / 10;
            Debug.Log("_num0= " + _num0);
            


        }
        _text = inputText.text;
        _num1 = _text.Length;

        if (_num1 - 1 <= 0)
        {
            inputText.text = $"";
            return;
        }
        _text = _text.Remove(_text.Length - 1);
        inputText.text = _text;
        if (_r == 1 || _c != 0)
        {
            _text = inputText.text;
            _num1 = _text.Length;

            if (_num1 - 1 <= 0)
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
                _num1 = _text.Length;

                if (_num1 - 1 <= 0)
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
    }
    public void ClickNumber(int num)
    {
            _j = 0;
        _r = 0;
        if (_c != 0)
        {
            ClickReset();
            
        }
        _c = 0;
        if (_res != 0 && _in1 == 0)
            {
                inputText.text = $"";
                _in = 0;
                _in1 = 0;
                _in2 = 0;
                _res = 0;
                _num0 = 0;
                _per = 0;
                _k = 0;
            }

            inputText.text = inputText.text + $"{num}";
            if (_per != 0)
            {
                _per++;
            }
            _in = _num0 * 10 + num;
            _num0 = _in;
            if (_k != 0 )
            {
                for (int m = 0; m < _k; m++)
                {
                    _in = _in * 10;
                }
            }
            Debug.Log("_number= " + _in);
           
    }
        public void ClickChange()
        {    
                if (_in >= 0)
            {
                inputText.text = inputText.text.Replace($"{_in}", $"(-{_in})");
                    _num0 = _num0 * (-1);
            }
            else
            {
                inputText.text = inputText.text.Replace($"(-{_in})", $"{_in}");
            _num0 = _num0 * (-1);
            }
        
         _c = 1;
        }
        public void ClickOperator(string oper)
        {
       
        _c = 0;
        if (_j != 0 && _o == 1)//якщо потрібно змінити операцію
        {
            
            ClickRemove();
            _r = 0;
        }
        
        if (oper== "^2"|| oper == "^3")
        {
            _r = 1;
        }
        if (oper == "^(1/2)")
        {
            _r = 2;
        }

        
        if (_i != 0 && _j ==0)//якщо операція НЕ перша
            {
            _r = 0;
                if (_per != 0 && _in!=0)
                {
                    _per = _per - 1-_k;
                }
                _in2 = _in / Math.Pow(10, _per);
                Debug.Log("_in2= " + _in2);
                _num0 = 0;
                _per = 0;


                if (!string.IsNullOrEmpty(_oper))
                {
                    switch (_oper)
                    {
                        case "+":
                            _res = _in1 + _in2;
                            break;
                        case "-":
                            _res = _in1 - _in2;
                            break;
                        case "*":
                            _res = _in1 * _in2;
                            break;
                        case "^(1/2)":

                            _res = Math.Sqrt(_in1);
                            break;

                        case "^2":

                            _res = Math.Pow(_in1, 2);
                            break;

                        case "^3":

                            _res = Math.Pow(_in1, 3);
                            break;

                        case "/":
                            if (_in2 == 0)
                            {
                                inputText.text = $"Error!";
                                break;
                            }
                            else
                            {
                                _res = _in1 / _in2;
                                break;
                            }
                    }

                    _in1 = _res;
                inputText.text = $"{_res}";
                    Debug.Log("_in1= " + _in1);
                }
            }
            else//якщо перша операція
            if(_j == 0)
        {
            _r = 0;
                if (_per != 0)
                {
                    _per = _per - 1 - _k;
         
                }
                if (_res == 0)
                {
                    _in1 = _num0 / Math.Pow(10, _per);
                _num0 = 0;
            }
                else
                {
                    _in1 = _res;
                inputText.text = $"{_res}";
                _num0 = 0;
            }
                _num0 = 0;
                _per = 0;
            }
        
        Debug.Log("_in1= " + _in1);
            inputText.text = inputText.text + $"{oper}";
            _oper = oper;
        
        _i++;
        _k = 0;
        _j = 1;
        _o = 1;

    }
        public void ClickEqual()
        {
        _o = 0;
            _i = 0;
            if (_per != 0)
            {
                _per = _per - 1 - _k;

        }
            _in2 = _num0 / Math.Pow(10, _per);
            Debug.Log("_in2= " + _in2);
            _num0 = 0;
            _per = 0;

            inputText.text = inputText.text + "=";

            if (!string.IsNullOrEmpty(_oper))
            {
                switch (_oper)
                {
                    case "+":
                        _res = _in1 + _in2;
                        break;
                    case "-":
                        _res = _in1 - _in2;
                        break;
                    case "*":
                        _res = _in1 * _in2;
                        break;
                    case "^(1/2)":

                        _res = Math.Sqrt(_in1);
                        break;

                    case "^2":

                        _res = Math.Pow(_in1, 2);
                        break;

                    case "^3":

                        _res = Math.Pow(_in1, 3);
                        break;

                    case "/":
                        if (_in2 == 0) {
                            inputText.text = $"Error!";
                            break;
                        }
                        else
                        {
                            _res = _in1 / _in2;
                            break;
                        }
                }
                _in1 = 0;

                inputText.text = inputText.text + $"{_res}";
                Debug.Log("_res= " + _res);
            }
            _k = 0;
        _r = 0;
        }
        public void ClickPeriod(string val)
        {
           if (_per == 0)
        {
            Debug.Log(message: $" ClickPeriod val:{val}");
            inputText.text = inputText.text + $"{val}";
            _per = 1;
        }
            
        }

        #endregion Methods

    
}
