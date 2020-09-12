using System;
using System.Collections.Generic;
using System.Text;

namespace calculate
{
    class CreateProblem2
    {
        //返回 两个题目，两个答案
        public string[] GetEquation1()
        {
            string[] question = new string[4];
            int x1 = new Random().Next(1, 9);
            int y1 = new Random().Next(10, 99);


            string q1 = x1 + "*" + y1 + "= __";
            question[0] = q1;
            question[1] =( x1 * y1)+"";

            return question; 
        }
        public string[] GetEquation2()
        {
            string[] question = new string[4];
            int x2 = new Random().Next(10, 99);
            int y2 = new Random().Next(10, 99);
            string q2 = x2 + "*" + y2 + "= __";
            question[0] = q2;
            question[1] = (x2 * y2) + "";
            return question;
        }
    }
}
