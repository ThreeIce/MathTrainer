using System;
using System.Collections.Generic;
using System.Text;

namespace calculate
{
    class CreateProblem3
    {
        static Random r;
        static CreateProblem3(){
            r = new Random();
        }
        //返回 两个题目，两个答案
        public string[] GetEquation1()
        {
            string[] question = new string[4];

            
            double[] num= { 0.2, 0.4, 0.5, 0.6 ,0.5};
            int x11 = r.Next(10, 99);
            int selec= r.Next(0, 3);
            double x1 = x11 + num[selec];
            int b1 = 0;
            switch (selec)
            {
                case 0:b1 = 5;break;
                case 1:b1= 5;break;
                case 2:b1 = 6;break;
                case 3:b1 = 5; break;
                case 4:b1 = 2;break;
            }
            int a1 = (int)(b1 * x1);


            string q1 = a1 + "/" + b1 + "= __";
            question[0] = q1;
            question[1] = x1+"";

            return question;
        }



        public string[] GetEquation2()
        {
            string[] question = new string[4];
            double[] num = { 0.2, 0.4, 0.5, 0.6, 0.5 };
             int selec = r.Next(0, 3);
            
            int x22 = r.Next(10, 99);
            int b22 = r.Next(1, 9);
            int selec2 = r.Next(0, 3);
            double x2 = (double)(x22 + num[selec2]);

            int b2 = 0;
            switch (selec2)
            {
                case 0: b2 = b22 * 10 + 5; break;
                case 1: b2 = b22 * 10 + 5; break;
                case 2: b2 = b22 * 10 + 6; break;
                case 3: b2 = b22 * 10 + 5; break;
                case 4: b2 = b22 * 10 + 2; break;
            }


            int a2 = (int)(b2 * x2);

             
            string q2 = a2 + "/" + b2 + "= __";
            question[0] = q2;
            question[1] = x2 + "";

            return question;

        }

        }
}
