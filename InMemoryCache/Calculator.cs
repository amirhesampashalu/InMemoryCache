namespace InMemoryCache
{
    public class Calculator
    {
        public int  sum(int a, int b)
        {
            checked {return a + b;}
          
        }
        public int Deliverd(int a, int b)
        {
            return a / b;
        }
        public int Multiplex(int a, int b)
        {
            return a * b;
        }

    }
}
