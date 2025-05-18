using System.Transactions;

namespace Stage0
{
     partial class Program
     {
        static void Main(string[] args)
        {
            Wellcome4543();
            Wellcome9318();
            Console.ReadKey(); 

        }

        static partial void Wellcome9318();

        private static void Wellcome4543()
        {
            Console.Write("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine(name + ",welcom to my first console application");
            Console.WriteLine("Trying more stuped things...");
            int trying_im_malkie;
        }

     }
}
