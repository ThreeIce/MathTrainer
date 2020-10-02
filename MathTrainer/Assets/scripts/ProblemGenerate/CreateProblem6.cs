using System;
using System.Collections.Generic;
using System.Text;

namespace calculate
{
    class CreateProblem6
    {
        public static Random r;
        static CreateProblem6(){
            r = new Random();
        }
        //返回题目，x，y 
        public string[] GetEquation()
        {
            string[] question = new string[3];
            int x = r.Next(-10,10);
            int y = r.Next(-10,10);
            int a1= r.Next(-20,20);
            int b1 = r.Next(-20, 20);
            int c1 = x * a1+b1*y;

            int a2 = r.Next(-20, 20);
            int b2 = r.Next(-20, 20);
            int c2 = x * a2 + b2 * y;

            string s1, s2;
            if(b1<0)
            {
                s1 = a1 + "x-" + (-b1) + "y=" + c1;
            }
            else
            {
                s1 = a1 + "x+" + b1 + "y=" + c1;
            }

            if (b2 < 0)
            {
                s2 = a2 + "x-" + (-b2) + "y=" + c2;
            }
            else
            {
                s2 = a2 + "x+" + b2 + "y=" + c2;
            }

            question[0] = s1 + "\n" + s2+"\n"+"x= __  y= __";
            question[1] = x+"";
            question[2] = y+"";


            return question;

        }

        
        

    }
}
