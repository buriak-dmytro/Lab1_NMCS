using System;

namespace Lab1
{
    class RootOnSegment
    {
        public readonly double root;
        public readonly double xLeft;
        public readonly double xRight;

        public RootOnSegment(double root, double xLeft, double xRight)
        {
            this.root = root;
            this.xLeft = xLeft;
            this.xRight = xRight;
        }

        public override string ToString()
        {
            return $"x in ({xLeft:0.#####} - {xRight:0.#####}): x = {root:0.#####}";
        }
    }
    
    class Program
    {
        static double func(double x)
        {
            return Math.Cos(Math.Sin(Math.Pow(x, 3))) - 0.7;
        }
        
        static double XMId(double xLeft, double xRight)
        {
            return xLeft - func(xLeft) * ((xRight - xLeft) / (func(xRight) - func(xLeft)));
        }
        
        static void Main()
        {
            double a = - Math.Pi / 2;
            double b = Math.Pi / 2;
            
            double step = (b - a) / 10;
                
            for(int i = 0; i < 10; i++)
            {
                double xLeft = a + i * step;
                double xRight = a + (i + 1) * step;
                
                double yLeft = func(xLeft);
                double yRight = func(xRight);
                
                if (yLeft == 0)
                {
                    roots.Add(new RootOnSegment(xLeft, leftBound, rightBound));
                }
                if (Math.Sign(yLeft) != Math.Sign(func(xRight)))
                {
                    bool forceStop = false;
                    double xMid = XMid(xLeft, xRight);
                    rootOfSubsegment = xMid;

                    while (Math.Abs(func(xMid)) > 0.01 | forceStop)
                    {
                        
                    }
                }
                if (yRight == 0)
                {
                    roots.Add(new RootOnSegment(xRight, leftBound, rightBound));
                }
            }
            
            
            
            
            Console.ReadLine();
        }
    }
}