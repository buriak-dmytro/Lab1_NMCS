namespace Lab_1
{
    class RootOnSegment
    {
        public readonly double root;
        public readonly double leftBound;
        public readonly double rightBound;

        public RootOnSegment(double root, double leftBound, double rightBound)
        {
            this.root = root;
            this.leftBound = leftBound;
            this.rightBound = rightBound;
        }

        public override string ToString()
        {
            return $"x in ({leftBound:0.#####} - {rightBound:0.#####}): x = {root:0.#####}";
        }
    }

    internal class Program
    {
        static double func(double x)
        {
            return Math.Cos(Math.Sin(Math.Pow(x, 3))) - 0.7;
        }

        static void Main(string[] args)
        {
            List<RootOnSegment> roots = new List<RootOnSegment>();

            double leftBound = - Math.PI / 2; // початкове значення лівого краю першого сегменту (1/10 проміжку)
            double step = Math.PI / 10; // значення кроку (1/10 проміжку)
            double rightBound = leftBound + step;  // початкове значення правого краю першого сегменту (1/10 проміжку)

            for (int i = 0; i < 10; i++) // для кожного з 10-ти сегментів
            {
                double rootOfSubsegment = 0; // змінна для зберігання кореня в сегменті

                double xLeft = leftBound;
                double xRight = rightBound;

                double yLeft = func(xLeft);
                double yRight = func(xRight);

                if (yLeft == 0) // у випадку, коли значення функції на границі сегменту рівне нулю, ця границя і буде коренем
                {
                    roots.Add(new RootOnSegment(xLeft, leftBound, rightBound));
                }
                else if (yRight == 0) // у випадку, коли значення функції на границі сегменту рівне нулю, ця границя і буде коренем
                {
                    roots.Add(new RootOnSegment(xRight, leftBound, rightBound));
                }
                else if (Math.Sign(yLeft) != Math.Sign(func(xRight))) // якщо знаки функції на границях сегменту різні, використовуємо метод хорд для уточнення кореня
                {
                    bool forceStop = false; // булева змінна для випадку, коли обчислене значення аргументу всередині сегменту і є коренем

                    while (Math.Abs(xRight - xLeft) > 0.01 | forceStop) // допоки не досягнута точність 0.01 або обчислене значення аргументу і є коренем
                    {
                        double xMid = xLeft - func(xLeft) * ((xRight - xLeft) / (func(xRight) - func(xLeft))); // обчислення аргументу за методом хорд
                        double yMid = func(xMid); // обчислення значення фунціїї для щойно знайденого аргументу

                        if (yMid == 0) // коли аргумент проміжної точки є коренем
                        {
                            rootOfSubsegment = xMid;
                            forceStop = true;
                        }
                        else
                        {
                            if (Math.Sign(yLeft) != Math.Sign(yMid)) // коли знаки функції на границях лівого підпроміжку сегменту різні
                            {
                                xRight = xMid; // нова права границя
                            }
                            else // Math.Sign(yMid) != Math.Sign(yRight) // коли знаки функції на границях правого підпроміжку сегменту різні
                            {
                                xLeft = xMid; // нова ліва границя
                            }

                            rootOfSubsegment = xLeft;
                        }
                    }

                    roots.Add(new RootOnSegment(rootOfSubsegment, leftBound, rightBound)); // після досягнення заданої точності додаємо розв'язок у список
                }

                leftBound = rightBound;
                rightBound += step;
            }

            foreach (var item in roots)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}
