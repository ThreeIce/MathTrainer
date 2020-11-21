using System;
using System.Collections.Generic;
using System.Text;

namespace calculate
{
    
    public class CreateProblem7
    {
         public static Random r;
         static CreateProblem7(){
             r = new Random();
         }

        //产生一元二次方程
        public string[] GetEquation(int flag)
        {
            //返回方程组result[0]与答案result[1,2]
            string[] result = new string[3];
            //10%无解[0]                  Type1
            //20%概率一个有理数解[1,2]    Type2
            //30%概率两个有理数解[4,5,3]  Type3
            //40%带根号解[6,7,8,9]        Type4

            int rand = 0;
            if (flag==1)
            {
                //生成随机数[0,9]
                Random rd = r;
                rand = rd.Next(0, 9);
            }
            else
            {
                rand = r.Next(0,5);
            }
            

            //区分类别
            int type = 0;
            if (rand - 6 >= 0)
            { type = 4; }
            else if ( rand-3>=0 )
            { type = 3; }
            else if (rand - 1 >= 0)
            { type = 2;}
            else 
            { type = 1;}
            
            switch(type)
            {
                case 3:result = Type3();break;
                case 2:result = Type2();break;
                case 1:result = Type1();break;
                case 4:result = Type4();break;
            }
            return result;
        }

        //创建两个有理数解类型
        private string[] Type3()
        {
            X:
            int x1 = r.Next(-10,10);
            int x2 = r.Next(-10,10);
            if(x1==x2) goto X;
            A:
            int a = r.Next(-10,10);
            if(a == 0) goto A;
            int c = a * x1 * x2;
            int b = -a * (x1 + x2);
            string[] question=new string[3];
            question[0] = "求" + a + "x^2" + 
                (b >= 0 ? "+" + b + "x" : b + "x")+
                (c >= 0 ? "+" + c : c.ToString()) +"=0的根，如果无解无需输入直接提交";
            question[1] = x1.ToString();
            question[2] = x2.ToString();
            return question;
        }
        //创建两个相同解类型
        private string[] Type2()
        {
            int x1 = r.Next(-10, 10);
            int x2 = x1;
            A:
            int a = r.Next(-10, 10);
            if(a == 0)  goto A;
            int c = a * x1 * x2;
            int b = -a * (x1 + x2);
            string[] question = new string[2];
            question[0] = "求" + a + "x^2" + 
                (b >= 0 ? "+" + b + "x" : b + "x")+
                (c >= 0 ? "+" + c : c.ToString()) +"=0的根，如果无解无需输入直接提交";
            question[1] = x1.ToString();
            return question;
        }
        //创建无解类型
        private string[] Type1()
        {
            string[] question = new string[1];
            int a, b, c;
            
            do{
                a = r.Next(-10, 10);
                b = r.Next(-10, 10);
                c = r.Next(-10, 10); 

            }while (4 * a * c < b * b) ;

            question[0] = "求" + a + "x^2" + 
                (b >= 0 ? "+" + b + "x" : b + "x")+
                (c >= 0 ? "+" + c : c.ToString()) +"=0的根，如果无解无需输入直接提交";

            return question;
        }
        //创建带根号类型
        private string[] Type4()
        {
            string[] question = new string[3];
            int a, b, c,temp1; double temp2;

            do
            {
                a = r.Next(-10, 10);
                b = r.Next(-10, 10);
                c = r.Next(-10, 10);

                temp1 = (int)Math.Sqrt(b * b - 4 * a * c);
                temp2= Math.Sqrt(b * b - 4 * a * c);

            } while ((4 * a * c > b * b)||temp1==temp2);

            string s;
            if (b < 0)
            {
                s = a + "x^2" + b + "x+"  + "="+ c;
            }
            else
                s = a + "x^2+" + b + "x+"  + "="+ c;

            question[0] =s ;

            int[] aaa = SolveSqrt(b * b - 4 * a * c);
            int bbb = aaa[0];
            int ccc = aaa[1];

            string answer1, answer2;
            if(b/2==(b/2.0))
            { 
                    answer1 = (-b / 2) + "/" + a + "+"+bbb+"\u221a"+ccc;
                    answer2 = (-b / 2) + "/" + a + "-" + bbb + "\u221a" + ccc;

            }
            else
            {
                answer1 = (-b) + "/" + (2*a) + "+" + bbb + "\u221a" + ccc;
                answer2 = (-b) + "/" + (2 * a) + "-" + bbb + "\u221a" + ccc;
            }


            question[0] = s;
            question[1] = answer1;
            question[2] = answer2;

            return question;
        }

        int[] SolveSqrt(int n)
        {
            int []a=new int[2];
            for(int i=(int)Math.Sqrt(n);i>0;i--)
            {
                if ((n % (i * i))==0)
                {
                    a[0] = i;
                    a[1]= ( n / (i * i));
                    break;
                }
            }
            return a;
        }

    }
}
