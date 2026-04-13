using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp
{
    class Program
    {
       static void Main(string[] args)
        {         
            Console.WriteLine("====== pilih study case yang ingin anda coba ======");
            Console.WriteLine();
            Console.WriteLine("1. Study Case 45");
            Console.WriteLine("2. Study Case 46");
            Console.WriteLine("3. Study Case 47");
            Console.WriteLine("4. Study Case 48");
            Console.WriteLine("5. Study Case 49");
            Console.WriteLine("6. Study Case 50");
            Console.Write("Masukkan pilihan Anda (1-6): ");
            Console.WriteLine();
            int pilihan = int.Parse(Console.ReadLine());
            Console.WriteLine("");
                switch (pilihan)
                {
                    case 1:
                        Console.WriteLine("membuat program perulangan untuk menampilkan angka dari 1 sampai 100 namun ketika sampai 25 dia akan berhenti");
                        for (int i = 1; i <= 100; i++)
                        {
                            if (i == 25)
                            {
                                break;
                            }
                            Console.WriteLine(i);
                        }
                        break;
                    case 2:
                        Console.WriteLine("membbuat program untuk mencari angka pertama yang habis dibagi 7 dalam rentang angka 1-100");
                        for (int i = 1; i <= 100; i++)
                        {
                            if (i % 7 == 0)
                            {
                                Console.WriteLine("Angka pertama yang habis dibagi 7 adalah: " + i);
                                break;
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine("membuat program untuk mencari angka 5 pertama kelipatan 9 dalam rentang angka 1-100");
                        int count = 0;
                        for (int i = 1; i <= 100; i++)
                        {
                            if (i % 9 == 0)
                            {
                                Console.WriteLine("Angka kelipatan 9: " + i);
                                count++;
                                if (count == 5)
                                {
                                    break;
                                }
                            }
                        }
                        break;
                    case 4:
                        Console.WriteLine("membuat program untuk menampilkan angka 1-20, tetapi setiap kelipatan 5 akan terlewati");
                        for (int i = 1; i <= 20; i++)
                        {
                            if (i % 5 == 0)
                            {
                                continue;
                            }
                            Console.WriteLine(i);
                        }
                        break;
                    case 5:
                        Console.WriteLine("membuat program untuk mencetak semua angka dari 1-20, tetapi melewati angka ganjil dengan menerapkan continue");
                        for (int i = 1; i <= 20; i++)
                        {
                            if (i % 2 != 0)
                            {
                                continue;
                            }
                            Console.WriteLine(i);
                        }
                        break;
                    case 6:
                        Console.WriteLine("membuat program untuk mencetak angka dari 1 hingga 20, tetapi melewati angka kelipatan 3 atau kelipatan 5 sekaligus");
                        for (int i = 1; i <= 20; i++)
                        {
                            if (i % 3 == 0 || i % 5 == 0)
                            {
                                continue;
                            }
                            Console.WriteLine(i);
                        }
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }
                Console.WriteLine("Ada ingin mencoba lagi? (y/n): ");
                string cobaLagi = Console.ReadLine();
                if (cobaLagi.ToLower() == "y")
                {
                    Console.Clear();
                    Main(args); 
                }
                else
                {
                    Console.WriteLine("Terima kasih telah mencoba!");
                }
         }
    } 
}        