using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CasinoApp
{
    class Program
    {
        static Player player = new Player("Guest", 1000000);
        static Random rng = new Random();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            Console.Write("\n  Masukkan nama kamu: ");
            Console.CursorVisible = true;
            string name = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(name)) player.Name = name;
            Console.CursorVisible = false;

            while (true)
            {
                ShowMainMenu();
                string choice = Console.ReadLine()?.Trim();

                if (player.Balance <= 0 && choice != "5")
                {
                    PrintColored("\n  Saldo kamu habis! Game over.\n", ConsoleColor.Red);
                    Console.Write("  Mau main lagi dari awal? (y/n): ");
                    if (Console.ReadLine()?.ToLower() == "y")
                        player.Balance = 1000000;
                    else break;
                    continue;
                }

                switch (choice)
                {
                    case "1": SlotMachine.Play(player, rng); break;
                    case "2": Blackjack.Play(player, rng); break;
                    case "3": Roulette.Play(player, rng); break;
                    case "4": Craps.Play(player, rng); break;
                    case "5":
                        Console.Clear();
                        PrintColored($"\n  Terima kasih sudah bermain, {player.Name}!\n", ConsoleColor.Yellow);
                        PrintColored($"  Saldo akhir: Rp {player.Balance:N0}\n\n", ConsoleColor.Cyan);
                        return;
                    default:
                        PrintColored("  Pilihan tidak valid.\n", ConsoleColor.Red);
                        break;
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.Clear();
            PrintColored("╔══════════════════════════════════════╗\n", ConsoleColor.Yellow);
            PrintColored("║           ROYAL CASINO               ║\n", ConsoleColor.Yellow);
            PrintColored("╚══════════════════════════════════════╝\n", ConsoleColor.Yellow);
            PrintColored($"  Pemain : {player.Name}\n", ConsoleColor.White);
            PrintColored($"  Saldo  : Rp {player.Balance:N0}\n\n", ConsoleColor.Green);
            PrintColored("  Pilih Game:\n", ConsoleColor.Cyan);
            Console.WriteLine("  [1] Slot Machine");
            Console.WriteLine("  [2] Blackjack");
            Console.WriteLine("  [3] Roulette");
            Console.WriteLine("  [4] Dadu (Craps)");
            Console.WriteLine("  [5] Keluar");
            Console.Write("\n  Pilihan: ");
        }

        public static void PrintColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static int AskBet(Player player)
        {
            while (true)
            {
                PrintColored($"\n  Saldo kamu: Rp {player.Balance:N0}\n", ConsoleColor.Green);
                Console.Write("  Masukkan taruhan (atau 0 untuk kembali): Rp ");
                if (int.TryParse(Console.ReadLine(), out int bet))
                {
                    if (bet == 0) return 0;
                    if (bet < 0) { PrintColored("  Taruhan tidak boleh negatif.\n", ConsoleColor.Red); continue; }
                    if (bet > player.Balance) { PrintColored("  Saldo tidak cukup!\n", ConsoleColor.Red); continue; }
                    return bet;
                }
                PrintColored("  Masukkan angka yang valid.\n", ConsoleColor.Red);
            }
        }

        public static void Pause()
        {
            Console.Write("\n  Tekan Enter untuk melanjutkan...");
            Console.ReadLine();
        }
    }

    // ─────────────────────────────────────────────
    class Player
    {
        public string Name;
        public int Balance;
        public Player(string name, int balance) { Name = name; Balance = balance; }
    }

    // ─────────────────────────────────────────────
    //  SLOT MACHINE
    // ─────────────────────────────────────────────
    static class SlotMachine
    {
        static string[] symbols = { "Ceri ", "Lemon", "Jeruk", "Anggr", "Intan", "Tujuh", "Bel  " };
        static int[]    weights  = {  30,     25,     20,     12,     7,      4,      2 };

        public static void Play(Player player, Random rng)
        {
            Console.Clear();
            Program.PrintColored("╔══════════════════════════════╗\n", ConsoleColor.Magenta);
            Program.PrintColored("║         SLOT MACHINE         ║\n", ConsoleColor.Magenta);
            Program.PrintColored("╚══════════════════════════════╝\n\n", ConsoleColor.Magenta);

            int bet = Program.AskBet(player);
            if (bet == 0) return;

            player.Balance -= bet;

            Console.Write("\n  Memutar");
            for (int i = 0; i < 3; i++) { Thread.Sleep(400); Console.Write("."); }

            string[] result = new string[3];
            for (int i = 0; i < 3; i++) result[i] = PickSymbol(rng);

            Console.WriteLine($"\n\n  +-------+-------+-------+");
            Console.WriteLine($"  | {result[0]} | {result[1]} | {result[2]} |");
            Console.WriteLine($"  +-------+-------+-------+\n");

            int win = 0;
            if (result[0] == result[1] && result[1] == result[2])
            {
                if (result[0] == "Tujuh") win = bet * 10;
                else if (result[0] == "Intan") win = bet * 7;
                else win = bet * 3;
                Program.PrintColored($"  *** JACKPOT! Kamu menang Rp {win:N0}! ***\n", ConsoleColor.Yellow);
            }
            else if (result[0] == result[1] || result[1] == result[2] || result[0] == result[2])
            {
                win = (int)(bet * 1.5);
                Program.PrintColored($"  Dua sama! Kamu menang Rp {win:N0}!\n", ConsoleColor.Green);
            }
            else
            {
                Program.PrintColored($"  Tidak ada kombinasi. Kamu kalah Rp {bet:N0}.\n", ConsoleColor.Red);
            }

            player.Balance += win;
            Program.PrintColored($"  Saldo sekarang: Rp {player.Balance:N0}\n", ConsoleColor.Cyan);
            Program.Pause();
        }

        static string PickSymbol(Random rng)
        {
            int total = weights.Sum();
            int roll = rng.Next(total);
            int cumul = 0;
            for (int i = 0; i < symbols.Length; i++)
            {
                cumul += weights[i];
                if (roll < cumul) return symbols[i];
            }
            return symbols[0];
        }
    }

    // ─────────────────────────────────────────────
    //  BLACKJACK
    // ─────────────────────────────────────────────
    static class Blackjack
    {
        static List<string> deck = new List<string>();
        static int deckPos = 0;

        public static void Play(Player player, Random rng)
        {
            Console.Clear();
            Program.PrintColored("╔══════════════════════════════╗\n", ConsoleColor.Blue);
            Program.PrintColored("║           BLACKJACK          ║\n", ConsoleColor.Blue);
            Program.PrintColored("╚══════════════════════════════╝\n", ConsoleColor.Blue);
            Console.WriteLine("  Target: capai 21, jangan lewati!\n");

            int bet = Program.AskBet(player);
            if (bet == 0) return;
            player.Balance -= bet;

            BuildDeck(rng);

            var playerHand = new List<string> { Deal(), Deal() };
            var dealerHand = new List<string> { Deal(), Deal() };

            while (true)
            {
                Console.Clear();
                Program.PrintColored("╔══════════════════════════════╗\n", ConsoleColor.Blue);
                Program.PrintColored("║           BLACKJACK          ║\n", ConsoleColor.Blue);
                Program.PrintColored("╚══════════════════════════════╝\n", ConsoleColor.Blue);
                Program.PrintColored($"\n  Dealer: [{dealerHand[0]}] [???]\n", ConsoleColor.Yellow);
                Program.PrintColored($"  Kamu  : [{string.Join("] [", playerHand)}]  -> {HandValue(playerHand)}\n\n", ConsoleColor.White);

                int pv = HandValue(playerHand);
                if (pv > 21) { Bust(player, bet); return; }
                if (pv == 21) { Console.WriteLine("  Blackjack! Auto-stand."); break; }

                Console.Write("  [H] Hit  [S] Stand: ");
                string input = Console.ReadLine()?.ToUpper();
                if (input == "H") playerHand.Add(Deal());
                else if (input == "S") break;
                else Program.PrintColored("  Pilihan tidak valid.\n", ConsoleColor.Red);
            }

            while (HandValue(dealerHand) < 17) dealerHand.Add(Deal());

            int pVal = HandValue(playerHand);
            int dVal = HandValue(dealerHand);

            Console.Clear();
            Program.PrintColored("--- HASIL ---\n", ConsoleColor.Cyan);
            Program.PrintColored($"  Dealer: [{string.Join("] [", dealerHand)}]  -> {dVal}\n", ConsoleColor.Yellow);
            Program.PrintColored($"  Kamu  : [{string.Join("] [", playerHand)}]  -> {pVal}\n\n", ConsoleColor.White);

            if (pVal > 21) Bust(player, bet);
            else if (dVal > 21 || pVal > dVal)
            {
                int win = bet * 2;
                Program.PrintColored($"  Kamu menang! +Rp {bet:N0}\n", ConsoleColor.Green);
                player.Balance += win;
            }
            else if (pVal == dVal)
            {
                Program.PrintColored($"  Seri! Taruhan dikembalikan.\n", ConsoleColor.Yellow);
                player.Balance += bet;
            }
            else Program.PrintColored($"  Dealer menang. -Rp {bet:N0}\n", ConsoleColor.Red);

            Program.PrintColored($"  Saldo: Rp {player.Balance:N0}\n", ConsoleColor.Cyan);
            Program.Pause();
        }

        static void Bust(Player player, int bet)
        {
            Program.PrintColored("  BUST! Kamu melebihi 21!\n", ConsoleColor.Red);
            Program.PrintColored($"  Saldo: Rp {player.Balance:N0}\n", ConsoleColor.Cyan);
            Program.Pause();
        }

        static void BuildDeck(Random rng)
        {
            deck.Clear();
            string[] suits = { "S", "H", "D", "C" };
            string[] ranks = { "2","3","4","5","6","7","8","9","10","J","Q","K","A" };
            foreach (var s in suits)
                foreach (var r in ranks)
                    deck.Add(r + s);
            deck = deck.OrderBy(_ => rng.Next()).ToList();
            deckPos = 0;
        }

        static string Deal() => deck[deckPos++];

        static int HandValue(List<string> hand)
        {
            int val = 0, aces = 0;
            foreach (var card in hand)
            {
                string r = card.Length > 2 ? card.Substring(0, 2) : card.Substring(0, 1);
                if (r == "A") { aces++; val += 11; }
                else if (r == "J" || r == "Q" || r == "K" || r == "10") val += 10;
                else val += int.Parse(r);
            }
            while (val > 21 && aces > 0) { val -= 10; aces--; }
            return val;
        }
    }

    // ─────────────────────────────────────────────
    //  ROULETTE
    // ─────────────────────────────────────────────
    static class Roulette
    {
        public static void Play(Player player, Random rng)
        {
            Console.Clear();
            Program.PrintColored("╔══════════════════════════════╗\n", ConsoleColor.Red);
            Program.PrintColored("║           ROULETTE           ║\n", ConsoleColor.Red);
            Program.PrintColored("╚══════════════════════════════╝\n", ConsoleColor.Red);
            Console.WriteLine("  Pilih jenis taruhan:\n");
            Console.WriteLine("  [1] Angka tepat (0-36)  -> bayar 35x");
            Console.WriteLine("  [2] Merah / Hitam       -> bayar 2x");
            Console.WriteLine("  [3] Ganjil / Genap      -> bayar 2x");
            Console.WriteLine("  [4] Rendah (1-18)       -> bayar 2x");
            Console.WriteLine("  [5] Tinggi (19-36)      -> bayar 2x");
            Console.WriteLine("  [0] Kembali\n");
            Console.Write("  Pilihan: ");

            string betType = Console.ReadLine()?.Trim();
            if (betType == "0") return;

            int bet = Program.AskBet(player);
            if (bet == 0) return;

            int betNumber = -1;
            string betColor = "", betParity = "", betRange = "";

            switch (betType)
            {
                case "1":
                    Console.Write("  Tebak angka (0-36): ");
                    if (!int.TryParse(Console.ReadLine(), out betNumber) || betNumber < 0 || betNumber > 36)
                    { Program.PrintColored("  Angka tidak valid.\n", ConsoleColor.Red); return; }
                    break;
                case "2":
                    Console.Write("  Pilih [M]erah atau [H]itam: ");
                    betColor = Console.ReadLine()?.ToUpper() == "M" ? "Merah" : "Hitam"; break;
                case "3":
                    Console.Write("  Pilih [G]anjil atau [E]nap (G/E): ");
                    betParity = Console.ReadLine()?.ToUpper() == "G" ? "Ganjil" : "Genap"; break;
                case "4": betRange = "Rendah"; break;
                case "5": betRange = "Tinggi"; break;
                default: Program.PrintColored("  Pilihan tidak valid.\n", ConsoleColor.Red); return;
            }

            player.Balance -= bet;

            Console.Write("\n  Memutar roda");
            for (int i = 0; i < 4; i++) { Thread.Sleep(350); Console.Write("."); }

            int result = rng.Next(0, 37);
            bool isRed = IsRed(result);
            string colorStr = result == 0 ? "Hijau" : (isRed ? "Merah" : "Hitam");

            Console.WriteLine($"\n\n  +====================+");
            Console.ForegroundColor = result == 0 ? ConsoleColor.Green : (isRed ? ConsoleColor.Red : ConsoleColor.DarkGray);
            Console.WriteLine($"  |  Hasil: {result,2}  {colorStr,-6}  |");
            Console.ResetColor();
            Console.WriteLine($"  +====================+\n");

            bool won = betType switch
            {
                "1" => result == betNumber,
                "2" => result != 0 && ((betColor == "Merah") == isRed),
                "3" => result != 0 && ((betParity == "Ganjil") == (result % 2 != 0)),
                "4" => result >= 1 && result <= 18,
                "5" => result >= 19 && result <= 36,
                _ => false
            };

            int multiplier = betType == "1" ? 35 : 1;

            if (won)
            {
                int win = bet + bet * multiplier;
                Program.PrintColored($"  Menang! +Rp {bet * multiplier:N0}\n", ConsoleColor.Green);
                player.Balance += win;
            }
            else Program.PrintColored($"  Kalah. -Rp {bet:N0}\n", ConsoleColor.Red);

            Program.PrintColored($"  Saldo: Rp {player.Balance:N0}\n", ConsoleColor.Cyan);
            Program.Pause();
        }

        static bool IsRed(int n)
        {
            int[] reds = { 1,3,5,7,9,12,14,16,18,19,21,23,25,27,30,32,34,36 };
            return reds.Contains(n);
        }
    }

    // ─────────────────────────────────────────────
    //  CRAPS (DADU)
    // ─────────────────────────────────────────────
    static class Craps
    {
        public static void Play(Player player, Random rng)
        {
            Console.Clear();
            Program.PrintColored("╔══════════════════════════════╗\n", ConsoleColor.Green);
            Program.PrintColored("║          DADU (CRAPS)        ║\n", ConsoleColor.Green);
            Program.PrintColored("╚══════════════════════════════╝\n", ConsoleColor.Green);
            Console.WriteLine("  Aturan:");
            Console.WriteLine("  * Lemparan pertama 7/11   -> Menang");
            Console.WriteLine("  * Lemparan pertama 2/3/12 -> Kalah (Craps)");
            Console.WriteLine("  * Lainnya -> jadi 'Point', lempar lagi");
            Console.WriteLine("    - Sama dengan Point -> Menang");
            Console.WriteLine("    - Keluar 7          -> Kalah\n");

            int bet = Program.AskBet(player);
            if (bet == 0) return;
            player.Balance -= bet;

            Console.Write("\n  Tekan Enter untuk lempar dadu...");
            Console.ReadLine();

            int d1 = rng.Next(1, 7), d2 = rng.Next(1, 7);
            int total = d1 + d2;
            PrintDice(d1, d2, total);

            if (total == 7 || total == 11)
            {
                Program.PrintColored("\n  Natural! Kamu menang!\n", ConsoleColor.Green);
                player.Balance += bet * 2;
            }
            else if (total == 2 || total == 3 || total == 12)
            {
                Program.PrintColored("\n  Craps! Kamu kalah.\n", ConsoleColor.Red);
            }
            else
            {
                int point = total;
                Program.PrintColored($"\n  Point ditetapkan: {point}. Lempar lagi!\n", ConsoleColor.Yellow);
                Thread.Sleep(600);

                while (true)
                {
                    Console.Write("  Tekan Enter untuk lempar dadu...");
                    Console.ReadLine();
                    d1 = rng.Next(1, 7); d2 = rng.Next(1, 7);
                    total = d1 + d2;
                    PrintDice(d1, d2, total);

                    if (total == point)
                    {
                        Program.PrintColored($"\n  Point! Kamu menang!\n", ConsoleColor.Green);
                        player.Balance += bet * 2;
                        break;
                    }
                    else if (total == 7)
                    {
                        Program.PrintColored("\n  Keluar 7! Kamu kalah.\n", ConsoleColor.Red);
                        break;
                    }
                    else Program.PrintColored($"  Bukan {point} atau 7, lempar lagi...\n", ConsoleColor.Yellow);
                }
            }

            Program.PrintColored($"\n  Saldo: Rp {player.Balance:N0}\n", ConsoleColor.Cyan);
            Program.Pause();
        }

        static void PrintDice(int d1, int d2, int total)
        {
            Console.WriteLine($"\n  +---+  +---+");
            Console.WriteLine($"  | {d1} |  | {d2} |");
            Console.WriteLine($"  +---+  +---+");
            Program.PrintColored($"  Total: {total}\n", ConsoleColor.White);
        }
    }
}