

using System.Reflection.Metadata.Ecma335;

public class Program
{
    public static void Main()
    {
        decimal anapara = 0, gunluk_faiz = 0;
        int gün_sayisi = 0;

        Console.WriteLine("Anapara giriniz: ");
        anapara = Convert.ToDecimal(Console.ReadLine());

        Console.WriteLine("Günlük faiz oranını giriniz: ");
        gunluk_faiz = Convert.ToDecimal(Console.ReadLine());

        Console.WriteLine("Gün sayısını giriniz: ");
        gün_sayisi = Convert.ToInt32(Console.ReadLine());




        for (int i=0; i<gün_sayisi; i++)
        {

            anapara += anapara * gunluk_faiz / 100;
            Console.WriteLine(i + 1 + ". Günde Yeni Toplam Paranız: " + anapara);
            
        }

        Main();
    }


    
}   