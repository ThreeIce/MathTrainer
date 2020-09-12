using System;
using System.Collections.Generic;
using System.Text;

namespace calculate
{
    class CreateProblem5
    {
        //返回题目与答案
        public string[] GetEquation()
        {
            string[] question = new string[3];
            int a, b, c, d;
            do
            {
                a = new Random().Next(-10, 10);
                b = new Random().Next(-10, 15);
                c = new Random().Next(-10, 10);
                d = new Random().Next(-10, 15);

            } while (a==0||b==0||c==0||d==0);

            

            string adbc = "";
            string bd = "";
            if(a * d + b * c>0)
            {
                adbc = "+" + (a * d + b * c);
            }
            else
                adbc = "" + (a * d + b * c);

            if (b * d>0)
            {
                bd = "+" + (b*d);
            }
            else
                bd=""+ (b * d);

            string s1 = a * c + "x^2" +adbc+ "x" + bd;


            string bs, ds;
            if (b > 0)
            {
                bs = "+" + b;
            }
            else
                bs = "" + b;
             
            if (d > 0)
            {
                ds = "+" + d;
            }
            else
                ds = "" + d;

            string s2 = "(" + a + "x" + bs + ")(" + c + "x" + ds + ")";

            question[0] = s1;
            question[1] = s2;

            return question;

        }
    }
}
