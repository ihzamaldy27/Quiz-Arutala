namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Masukkan Waktu (hh:mm:ssAM / hh:mm:ssPM) :");
            var s = Console.ReadLine();
            Console.WriteLine(ConvertTime(s));
            Console.ReadKey();
        }

        static string ConvertTime(string s){
        return DateTime.Parse(s).ToString("HH:mm:ss");
        }
    }
}
