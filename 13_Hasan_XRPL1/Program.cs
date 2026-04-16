using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp
{
    class Program_Sistem_Kasir_Sederhana
    {
         static void Main(string[] args)
        {
            Console.WriteLine("=== Sistem Kasir Sansaba.dev ===");
            Console.Write("Masukkan nama pelanggan : ");
            string namaPelanggan = Console.ReadLine();
            Console.Write("Masukkan jumlah jenis barang : ");
            int jumlahJenis = int.Parse(Console.ReadLine());
            double totalBelanja = 0;
            for (int i = 0; i < jumlahJenis; i++)
            {
                Console.Write("Masukkan harga barang ke-" + (i + 1) + " : ");
                double hargaBarang = double.Parse(Console.ReadLine());
                totalBelanja += hargaBarang;
            }

            double diskonPersen = 0;
            if (totalBelanja >= 500000)
            {
                diskonPersen = 0.15;
            }
            else if (totalBelanja >= 200000)
            {
                diskonPersen = 0.10;
            }
            double diskon = totalBelanja * diskonPersen;
            double totalBayar = totalBelanja - diskon;
     
            Console.WriteLine("\n--- Ringkasan Pembayaran ---");
            Console.WriteLine("Nama pelanggan: " + namaPelanggan);
            Console.WriteLine("Total belanja (sebelum diskon): "+ totalBelanja.ToString("C"));
            Console.WriteLine("Nominal diskon: " + diskon.ToString("C"));
            Console.WriteLine("Total bayar (setelah diskon): "+ totalBayar.ToString("C"));
            Console.WriteLine("---------------------------------");

            Console.Write("\nMasukkan jumlah uang dibayar: Rp.");
            double uangDibayar = double.Parse(Console.ReadLine());

            if (uangDibayar > totalBayar)
            {
                double kembalian = uangDibayar - totalBayar;
                Console.WriteLine("Kembalian: "+ kembalian.ToString("C"));
            }
            else if (uangDibayar == totalBayar)
            {
                Console.WriteLine("Uang pas");
            }
            else
            {
                double kurang = totalBayar - uangDibayar;
                Console.WriteLine("Uang anda kurang Rp."+ kurang.ToString("C"));
                Console.Write("Ingin melakukan pembayaran ulang? (y/n): ");
                string pilihan = Console.ReadLine();
                if (pilihan.ToLower() == "y")
                {
                    Main(args);
                }
                else
                {
                    Console.WriteLine("=== Transaksi dibatalkan ===");
                }
            }

            Console.WriteLine("Terima kasih " + namaPelanggan + ", telah berbelanja di Sansaba.dev!");
            Console.WriteLine();
            Console.WriteLine("=== Transaksi selesai ===");
            Console.ReadKey();
        }


        //CREATED BY HASAN NUR ADGHI WIBOWO/13/XRPL1
        //SAYA BERJANJI TIDAK MENYONTEK DAN OPEN AI ATAU SEJENISNYA
        //SAYA BERBUAT DEMI MASA DEPAN 
        //ngga baca kalau misal bisa pilih pake int atau double wkwkkwwk (malas ngedit lagi)
        //DIKUMPULKAN JAM 09.10
    } 
}        