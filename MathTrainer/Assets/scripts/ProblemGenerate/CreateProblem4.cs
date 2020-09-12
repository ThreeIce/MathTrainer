using System;
using System.Collections.Generic;
using System.Text;

namespace calculate
{
    class CreateProblem4
    {
        //返回 1个题目，1个答案
        public string[] GetEquation()
        {
            string[] question = new string[4];
            double x,a_b,a,b,k;
            do
            {    a_b = new Random().Next(10, 999);
                 a = new Random().Next(10, 999);
                 b = a - a_b;
                 k = new Random().Next(1, 99);
                
                x = (double)a_b / (double)k;
                //Console.WriteLine(x);

            } while (x*10.0!=(int)(x * 10));

            string s;
            if (b<0)
            {
                 s= k + "x" + b + "=" + a;
            }
            else
                 s = k + "x+" + b + "=" + a;

            string answer = x + "";
            question[0] = s;
            question[1] = answer;
            return question;
        }
    }
}
