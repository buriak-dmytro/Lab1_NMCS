using System;

namespace Lab_1
{
    internal class Program
    {
        static HashSet<double> roots = new HashSet<double>();

        static void Subdivide(int iterations, double leftBoundX, double rightBoundX)
        {
            double step = (rightBoundX - leftBoundX) / iterations;

            for (int i = 0; i < iterations; i++)
            {
                double xLeftSubsegment = leftBoundX + i * step;
                double xRightSubsegment = leftBoundX + (i + 1) * step;

                double yLeftSubsegment = func(xLeftSubsegment);
                double yRightSubsegment = func(xRightSubsegment);

                if (IsDerivativeFuncChangeSign(10, xLeftSubsegment, xRightSubsegment)) // unstable sign of derivative function
                {

                    if (Math.Abs(xRightSubsegment - xLeftSubsegment) > 0.001) // limit value for subdivision (less is more accurate but can add similar roots)
                    {
                        Subdivide(10, xLeftSubsegment, xRightSubsegment);
                    }
                    else
                    {
                        if (Math.Abs(yLeftSubsegment) < 0.001) // works in pair with first above
                        {
                            roots.Add(xLeftSubsegment);
                        }
                        if (Math.Abs(yRightSubsegment) < 0.001) // works in pair with second above
                        {
                            roots.Add(xRightSubsegment);
                        }
                    }
                }
                else // stable sign of derivative function
                {
                    if (yLeftSubsegment == 0)
                    {
                        roots.Add(xLeftSubsegment);
                    }
                    if (yRightSubsegment == 0)
                    {
                        roots.Add(xRightSubsegment);
                    }

                    if ((Math.Sign(yLeftSubsegment) == -1 & Math.Sign(yRightSubsegment) == 1) |
                        (Math.Sign(yLeftSubsegment) == 1 & Math.Sign(yRightSubsegment) == -1)) // one sign change
                    {
                        SpecifyRoot(xLeftSubsegment, xRightSubsegment, 0.01);
                    }
                    else // no sign changes on subsegment
                    {
                        continue;
                    }
                }
            }
        }

        static bool IsDerivativeFuncChangeSign(int iterations, double leftBoundX, double rightBoundX)
        {
            bool IsChangeSign = false;
            double sign = Math.Sign(funcDerivative(leftBoundX, 0.05));

            double step = (rightBoundX - leftBoundX) / iterations;

            for (int i = 0; i < iterations & !IsChangeSign; i++)
            {
                double rightFuncDeriv = funcDerivative(leftBoundX + (i + 1) * step, 0.05);

                if (sign == 0)
                {
                    sign = Math.Sign(rightFuncDeriv);
                }
                else
                {
                    if (Math.Sign(rightFuncDeriv) != sign)
                    {
                        IsChangeSign = true;
                    }
                }
            }

            return IsChangeSign;
        }

        static void SpecifyRoot(double leftBoundX, double rightBoundX, double epsilon)
        {
            double xLeft = leftBoundX;
            double xRight = rightBoundX;

            double yLeft = func(leftBoundX);
            double yRight = func(rightBoundX);

            double xMid = XMid(xLeft, xRight);

            while (Math.Abs(func(xMid)) > epsilon & func(xMid) != 0)
            {
                double yMid = func(xMid);

                if (Math.Sign(yLeft) != Math.Sign(yMid))
                {
                    xRight = xMid;
                }
                else
                {
                    xLeft = xMid;
                }

                xMid = XMid(xLeft, xRight);
            }

            roots.Add(xMid);
        }
        
        static double func(double x)
        {
            return Math.Cos(Math.Sin(Math.Pow(x, 3))) - 0.7;
        }

        static double funcDerivative(double x, double delta)
        {
            return (func(x + delta) - func(x)) * 1.0 / (delta);
        }

        static double XMid(double leftBoundX, double rightBoundX)
        {
            return leftBoundX - func(leftBoundX) * ((rightBoundX - leftBoundX) / (func(rightBoundX) - func(leftBoundX)));
        }

        static void Output()
        {
            foreach (var item in roots)
            {
                Console.WriteLine(item);
            }
        }

        static void Main(string[] args)
        {
            Subdivide(10, -Math.PI / 2, Math.PI / 2);

            Output();

            Console.WriteLine(roots.Count);

            Console.ReadLine();
        }
    }
}

