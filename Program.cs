using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using Topshelf.Runtime.Windows;

namespace Consol
{
    class Program
    {
        public static void CallToChildThread()
        {
            Console.WriteLine("Child thread starts");
            Console.Beep(2000, 10000);
        }

        //variables
        static Random rnd = new Random();
        public enum RunApp : int
        {
            Null = 0,
            Setup,
            App,
            Calculator,
            Pi,
            Similarity,
            TicTacToe,
            SquareoSquare,
            QuadraticsMenu,
            QuadraticFormula,
            CompletingSquare
        }
        public static RunApp AppId;
        public enum CalcType
        {
            Null = 0,
            Add,
            Subtract,
            Multiply,
            Divide,
            Remainder,
            PowerIndex,
            Root
        }
        public static CalcType CalcId = CalcType.Null;
        public enum Box
        {
            Null = 0,
            Box1,
            Box2,
            Box3,
            Box4,
            Box5,
            Box6,
            Box7,
            Box8,
            Box9
        }

		public static NativeMethods.ConsoleHandle consoleHandle;
		public static Int32 originalConsoleMode = 0; // original mode

		//functions
		static void pause(int a)
        {
            Thread.Sleep(a);
        }
        public static object Clear()
        {
            Console.Clear();
            return (true);
        }
        public static void newl() { Console.WriteLine(); }
        private static ConsoleColor GetRndColor()
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)consoleColors.GetValue(rnd.Next(consoleColors.Length));
        }
        public static void type(string txt, int delay, bool writel)
        {
            for (int i = 0; i < txt.Length; i++)
            {
                Console.Write(txt[i]);
                pause(delay);
            }
            if (writel == true)
                Console.WriteLine();
        }
        public static void ClrCL()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        public static void progress(string name ,int processes, int speed, bool randomSpeed, int rndlow, int rndhigh, bool useBar, bool usePercent, bool useAnimation)
        {
            int g = 0;
            string[] animation = new string[4] { "|", "/", "--", "\\" };
            string done = "";
            string undone = "";
            if (useBar == true)
            {
                for (int f = 0; f <= processes; f++)
                {
                    undone = undone + "-";
                }
            }
            int progress = 0;
            for (int f = 0; f <= processes; f++)
            {
                
                ClrCL();
                if (useBar == true)
                {
                    Console.Write("{0} [{1}{2}]", name, done, undone);
                    done += "#";
                    undone = undone.Remove(0, 1);
                }
                if (usePercent == true)
                {
                    Console.Write("%{0}", progress);
                    progress += 100 / processes;
                }
                if (useAnimation == true)
                {
                    Console.Write(" {0}", animation[g]);
                    if (g == 3)
                        g = 0;
                    g++;
                }
                newl();
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                if (randomSpeed == false)
                    pause(speed);
                else if (randomSpeed == true)
                    pause(rnd.Next(rndlow, rndhigh));
                else
                    pause(100);
            }
            ClrCL();
            Console.Write("{0} [{1}{2}]", name, done, undone);
            Console.WriteLine("Complete");
        }
        public static void load(string text, int amount, int dotAmount)
        {
            for (int f = 0; f <= amount; f++)
            {
                Console.Write(text);
                pause(500);
                for (int g = 0; g <= dotAmount; g++)
                {
                    Console.Write(".");
                    pause(500);
                }
                Console.SetCursorPosition(0, Console.CursorTop);
                ClrCL();
            }
        }
        public static double Mathf(CalcType mode, double a, double b)
        {
            double result = 0;
            switch (mode)
            {
                case CalcType.Add:
                    result = a + b;
                    break;
                case CalcType.Subtract:
                    result = a - b;
                    break;
                case CalcType.Multiply:
                    result = a * b;
                    break;
                case CalcType.Divide:
                    result = a / b;
                    break;
                case CalcType.Remainder:
                    result = a % b;
                    break;
                case CalcType.PowerIndex:
                    result = Math.Pow(a, b);
                    break;
                case CalcType.Root:
                    result = Math.Pow(a, 1.0 / b);
                    break;
            }
            return result;
        }

        /*
        protected static void Play(Note[] tune)
        {
            foreach (Note n in tune)
            {
                if (n.NoteTone == Tone.rest)
                    Thread.Sleep((int)n.NoteDuration);
                else
                    Console.Beep((int)n.NoteTone, (int)n.NoteDuration);
            }
        }
        protected struct Note
        {
            Tone toneVal;
            Duration durVal;

            public Note(Tone frequency, Duration time)
            {
                toneVal = frequency;
                durVal = time;
            }
            public Tone NoteTone { get { return toneVal; } }
            public Duration NoteDuration { get { return durVal; } }
        }
        protected enum Tone
        {
            rest = 0,
            C4 = 262,
            D4 = 294,
            E4 = 330,
            F4 = 349,
            G4 = 392,
            G4sharp = 415,
            A4 = 440,
            B4 = 494,
            C5 = 523,
            D5 = 587,
            E5 = 659,
            F5 = 698,
            G5 = 784,
            G5sharp = 831,
            A5 = 880,
            B5 = 988,
            C6 = 1047,
            D6 = 1175,
            E6 = 1319,
            F6 = 1397,
            G6 = 1568,
            A6 = 1760,
            B6 = 1976,
            C7 = 2093,
        }
        protected enum Duration
        {
            whole = 1000,
            half = whole / 2,
            third = whole / 3,
            quarter = half / 2,
            eighth = quarter / 2,
            sixteenth = eighth / 2,
        }
        */


        //methods
        public static void Setup()
        {
			

			Console.CursorVisible = false;
            Clear();
            type("Skip intro? Y/N", 70, true);
            var pressed = Console.ReadKey().Key;
            if (pressed == ConsoleKey.Y)
                goto End;
            else if (pressed == ConsoleKey.N)
                Clear();
            else
                goto End;


            /*
            Note[] Maga =
            {
                new Note(Tone.B4, Duration.quarter),
                new Note(Tone.A4, Duration.quarter),
                new Note(Tone.G4, Duration.quarter),
                new Note(Tone.A4, Duration.quarter),
                new Note(Tone.B4, Duration.quarter),
                new Note(Tone.B4, Duration.quarter),
                new Note(Tone.B4, Duration.half),
                new Note(Tone.A4, Duration.quarter),
                new Note(Tone.A4, Duration.quarter),
                new Note(Tone.A4, Duration.half),
                new Note(Tone.B4, Duration.quarter),
                new Note(Tone.D5, Duration.quarter),
                new Note(Tone.D5, Duration.half)
            };

            Play(Maga);
            */


            ThreadStart childref = new ThreadStart(CallToChildThread);

            Console.WriteLine("Hi");
            pause(2000);
            Console.WriteLine("I'm glad you're here...");
            pause(1000);
            Clear();
            pause(500);

            type("Welcome to the Robotic Vitality Transfigurator", 70, true);
            pause(3000);
            newl();
            Console.WriteLine("Press M to attempt connection...");
            while (Console.ReadKey(true).Key != ConsoleKey.M) { }

            Console.WriteLine("Preparing connection...");
            pause(rnd.Next(500, 2000));

            type("Establishing connection with server...", 70, true);
            pause(1000);
            int perc = 0;
            string wrtperc = "0";
            string wrtpercold;
            Console.WriteLine("%{0}", wrtperc);
            while (perc <= 100)
            {
                wrtpercold = wrtperc;
                wrtperc = perc.ToString();
                wrtperc.Replace(wrtpercold, wrtperc);
                Console.WriteLine("%{0}", wrtperc);
                pause(rnd.Next(100, 500));
                perc += rnd.Next(1, 8);
            }
            if (perc != 100)
                Console.WriteLine("%100");
            Console.WriteLine("Connection Established.");
            pause(3000);
            Clear();
            Console.WriteLine("Attempting to log in...");
            pause(rnd.Next(1000, 2000));
            Clear();
            Console.WriteLine("Log in successful...");
            pause(1000);
            Clear();
            Console.WriteLine("Synchronising data...");
            pause(rnd.Next(2000, 4000));
            Clear();
            Console.WriteLine("Launching application...");
            pause(700);
            for (int f = 0; f <= rnd.Next(5, 30); f++)
            {
                Console.Beep(rnd.Next(200, 1000), rnd.Next(200, 500));
                Console.BackgroundColor = GetRndColor();
                Console.Clear();
            }
            pause(2000);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Clear();
            for (int f = 0; f <= 2; f++)
            {
                Console.Write("Creating directories");
                pause(500);
                for (int g = 0; g <= 4; g++)
                {
                    Console.Write(".");
                    pause(500);
                }
                Clear();
            }

            for (int f = 0; f <= 200; f++)
            {
                Console.WriteLine("Creating new directory at C:\\Windows\\System32\\{0}", rnd.Next(1000, 9999));
                pause(20);
            }
            for (int f = 0; f <= 200; f++)
            {
                Console.WriteLine("Creating new directory at C:\\Windows\\Program Files\\{0}", rnd.Next(100000, 999999));
                pause(20);
            }
            for (int f = 0; f <= 200; f++)
            {
                Console.WriteLine("Creating new directory at C:\\Windows\\Program Files (x86)\\{0}", rnd.Next(10000000, 99999999));
                pause(20);
            }
            pause(500);
            Clear();
            type("Successfully created x600 directories...", 70, true);
            pause(500);
            Console.WriteLine();
            type("Please enter the PIN to continue...", 70, true);
            Console.WriteLine();
            string PIN;
        Retry:
            PIN = Console.ReadLine();
            if (PIN != "3.1415")
            {
                Console.WriteLine("Authentication failed, please try again!");
                goto Retry;
            }
            type("Authentication successful...", 70, true);
            pause(500);
            type("Encrypting connection...", 70, true);
            pause(500);
            for (int f = 0; f <= 50; f++)
            {
                Console.Write("-");
                pause(rnd.Next(10, 50));
            }
            pause(500);
            newl();
            type("Complete...", 70, true);
            pause(2000);
            Clear();
            load("Loading", 1, 2);
            type("Human authentication required...", 70, true);
            pause(500);
            type("Please answer the following questions to prove that you are not a robot...", 70, true);
            pause(500);
            Clear();
            type("How old are you?", 70, true);
        Age:
            var age = int.Parse(Console.ReadLine());
            if (age < 0 || age > 120)
            {
                Console.WriteLine("error");
                Console.WriteLine("Try again!");
                goto Age;
            }
            type("How many continents are there?", 70, true);
        continents:
            if (int.Parse(Console.ReadLine()) != 7)
            {
                Console.WriteLine("error");
                Console.WriteLine("Try again!");
                goto continents;
            }
        answered:
            type("How many questions have you answered?", 70, true);
            if (int.Parse(Console.ReadLine()) != 2)
            {
                Console.WriteLine("error");
                Console.WriteLine("Try again!");
                goto answered;
            }
            type("Human authentication successful", 70, true);
            pause(2000);
            Clear();
            for (int f = 0; f <= 1; f++)
            {
                Console.Write("Entering incognito mode");
                pause(500);
                for (int g = 0; g <= 4; g++)
                {
                    Console.Write(".");
                    pause(500);
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Clear();
            }
            type("Encoding connection...", 70, true);
            pause(3000);
            for (int f = 0; f <= 300; f++)
            {
                Console.Write("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11}",
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999),
                    rnd.Next(1000, 9999)
                    );
                pause(10);
            }
            Clear();
            for (int f = 0; f <= 1; f++)
            {
                Console.Write("Decoding incoming connection");
                pause(500);
                for (int g = 0; g <= 2; g++)
                {
                    Console.Write(".");
                    pause(500);
                }
                Clear();
            }

            Console.Write("Progress:");
            progress("Progress:", 23, 150, false, 0, 0, true, true, true);
            pause(1000);
            Clear();
            type("Files successfully recieved!", 70, true);
            pause(500);
            progress("Extracting...", 39, 70, false, 0, 0, true, true, true);
            for (int f = 0; f <= 70; f++)
            {
                Console.WriteLine("C:\\Program Files\\Custom Utilities\\temp\\{0}.temp", rnd.Next(100, 99999));
                pause(20);
            }
            Clear();
            Console.WriteLine("Finalising setup...");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClrCL();
            pause(1000);
            progress("Finalising setup...", 19, 0, true, 10, 1000, true, true, false);
            Console.WriteLine("Press A to launch the main application...");
            while (Console.ReadKey(true).Key != ConsoleKey.A) { }
            Clear();
            load("Loading", 2, 2);
            Clear();
            load("Loading resources", 3, 2);
        End:
            Clear();
            AppId = RunApp.App;
        }
        public static void App()
        {
            Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            type(@"
╔╦═╦╗╔═╗╔╗ ╔═╗╔═╗╔═╦═╗╔═╗     ╔══╗╔═╗     ╔══╗╔╗╔╗╔═╗
║║║║║║╦╝║║ ║╔╝║║║║║║║║║╦╝     ╚╗╔╝║║║     ╚╗╔╝║╚╝║║╦╝
║║║║║║╩╗║╚╗║╚╗║║║║║║║║║╩╗      ║║ ║║║      ║║ ║╔╗║║╩╗
╚═╩═╝╚═╝╚═╝╚═╝╚═╝╚╩═╩╝╚═╝      ╚╝ ╚═╝      ╚╝ ╚╝╚╝╚═╝", 0, true);//10
            Console.ForegroundColor = ConsoleColor.White;
            type(@"
╔═══╗╔═══╗╔══╗ ╔═══╗╔════╗╔══╗╔═══╗     ╔╗  ╔╗╔══╗╔════╗╔═══╗╔╗   ╔══╗╔════╗╔╗  ╔╗     ╔════╗╔═══╗╔═══╗╔═╗ ╔╗╔═══╗╔═══╗╔══╗╔═══╗╔╗ ╔╗╔═══╗╔═══╗╔════╗╔═══╗╔═══╗
║╔═╗║║╔═╗║║╔╗║ ║╔═╗║║╔╗╔╗║╚╣ ╝║╔═╗║     ║╚╗╔╝║╚╣ ╝║╔╗╔╗║║╔═╗║║║   ╚╣ ╝║╔╗╔╗║║╚╗╔╝║     ║╔╗╔╗║║╔═╗║║╔═╗║║║╚╗║║║╔═╗║║╔══╝╚╣ ╝║╔═╗║║║ ║║║╔═╗║║╔═╗║║╔╗╔╗║║╔═╗║║╔═╗║
║╚═╝║║║ ║║║╚╝╚╗║║ ║║╚╝║║╚╝ ║║ ║║ ╚╝     ╚╗║║╔╝ ║║ ╚╝║║╚╝║║ ║║║║    ║║ ╚╝║║╚╝╚╗╚╝╔╝     ╚╝║║╚╝║╚═╝║║║ ║║║╔╗╚╝║║╚══╗║╚══╗ ║║ ║║ ╚╝║║ ║║║╚═╝║║║ ║║╚╝║║╚╝║║ ║║║╚═╝║
║╔╗╔╝║║ ║║║╔═╗║║║ ║║  ║║   ║║ ║║ ╔╗      ║╚╝║  ║║   ║║  ║╚═╝║║║ ╔╗ ║║   ║║   ╚╗╔╝        ║║  ║╔╗╔╝║╚═╝║║║╚╗║║╚══╗║║╔══╝ ║║ ║║╔═╗║║ ║║║╔╗╔╝║╚═╝║  ║║  ║║ ║║║╔╗╔╝
║║║╚╗║╚═╝║║╚═╝║║╚═╝║  ║║  ╔╣ ╗║╚═╝║      ╚╗╔╝ ╔╣ ╗  ║║  ║╔═╗║║╚═╝║╔╣ ╗  ║║    ║║         ║║  ║║║╚╗║╔═╗║║║ ║║║║╚═╝║║║   ╔╣ ╗║╚╩═║║╚═╝║║║║╚╗║╔═╗║  ║║  ║╚═╝║║║║╚╗
╚╝╚═╝╚═══╝╚═══╝╚═══╝  ╚╝  ╚══╝╚═══╝       ╚╝  ╚══╝  ╚╝  ╚╝ ╚╝╚═══╝╚══╝  ╚╝    ╚╝         ╚╝  ╚╝╚═╝╚╝ ╚╝╚╝ ╚═╝╚═══╝╚╝   ╚══╝╚═══╝╚═══╝╚╝╚═╝╚╝ ╚╝  ╚╝  ╚═══╝╚╝╚═╝
            ", 0, true);//4
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(@"
╔════╗ ╔═╗╔══╗╔╗ ╔═╗╔╦╗╔╗ ╔══╗╔══╗╔═╗╔═╗
║    ║ ║╔╝║╔╗║║║ ║╔╝║║║║║ ║╔╗║╚╗╔╝║║║║╬║
║    ║ ║╚╗║╠╣║║╚╗║╚╗║║║║╚╗║╠╣║ ║║ ║║║║╗╣
╚════╝ ╚═╝╚╝╚╝╚═╝╚═╝╚═╝╚═╝╚╝╚╝ ╚╝ ╚═╝╚╩╝
            ");//4
            Console.Write(@"
╔════╗ ╔═╗╔╦╗╔══╗╔══╗╔═╗╔══╗╔══╗╔══╗╔═╗╔══╗
║    ║ ║╬║║║║║╔╗║╚╗╗║║╬║║╔╗║╚╗╔╝╚║║╝║╔╝║══╣
║    ║ ╚╗║║║║║╠╣║╔╩╝║║╗╣║╠╣║ ║║ ╔║║╗║╚╗╠══║
╚════╝  ╚╝╚═╝╚╝╚╝╚══╝╚╩╝╚╝╚╝ ╚╝ ╚══╝╚═╝╚══╝
            ");
            Console.Write(@"
╔════╗ ╔══╗╔═╗╔╦╗╔══╗╔═╗╔═╗╔═╗╔══╗╔═╗╔╦╗╔══╗╔═╗╔═╗
║    ║ ║══╣║╬║║║║║╔╗║║╬║║╦╝║║║║══╣║╬║║║║║╔╗║║╬║║╦╝
║    ║ ╠══║╚╗║║║║║╠╣║║╗╣║╩╗║║║╠══║╚╗║║║║║╠╣║║╗╣║╩╗
╚════╝ ╚══╝ ╚╝╚═╝╚╝╚╝╚╩╝╚═╝╚═╝╚══╝ ╚╝╚═╝╚╝╚╝╚╩╝╚═╝
");


            NativeMethods.SetConsoleMode(consoleHandle, NativeMethods.ENABLE_MOUSE_INPUT | NativeMethods.ENABLE_EXTENDED_FLAGS);

            var record = new NativeMethods.INPUT_RECORD();
            uint recordLen = 0;
            while (true)
            {
                NativeMethods.ReadConsoleInput(NativeMethods.GetStdHandle(NativeMethods.STD_INPUT_HANDLE), ref record, 1, ref recordLen);
                Console.SetCursorPosition(0, 0);

                int X = record.MouseEvent.dwMousePosition.X;
                int Y = record.MouseEvent.dwMousePosition.Y;
                int button = record.MouseEvent.dwButtonState;
                /*Enumerable.Range(1, 13).Contains(X)*/
                if (X >= 1 && X <= 4 && Y >= 15 && Y <= 16 && button == 1)
                {
					NativeMethods.SetConsoleMode(consoleHandle, originalConsoleMode);
                    Clear();
                    AppId = RunApp.Calculator;
                    goto OpenApp;
                }
                if (X >= 1 && X <= 4 && Y >= 20 && Y <= 21 && button == 1)
                {
                    NativeMethods.SetConsoleMode(consoleHandle, originalConsoleMode);
                    Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    AppId = RunApp.QuadraticsMenu;
                    goto OpenApp;
                }
                if (X >= 1 && X <= 4 && Y >= 25 && Y <= 26 && button == 1)
                {
                    NativeMethods.SetConsoleMode(consoleHandle, originalConsoleMode);
                    Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    AppId = RunApp.SquareoSquare;
                    goto OpenApp;
                }
            }
        OpenApp:;
        }
        public static void Calculator()
        {
            string In1, In2;
            int history = 1;

            type(@"
╔═══╗╔═══╗╔╗   ╔═══╗╔╗ ╔╗╔╗   ╔═══╗╔════╗╔═══╗╔═══╗ Instructions:                                   History:
║╔═╗║║╔═╗║║║   ║╔═╗║║║ ║║║║   ║╔═╗║║╔╗╔╗║║╔═╗║║╔═╗║ | a = Add      | s = Subtract
║║ ╚╝║║ ║║║║   ║║ ╚╝║║ ║║║║   ║║ ║║╚╝║║╚╝║║ ║║║╚═╝║ | m = Multiply | d = Divide  
║║ ╔╗║╚═╝║║║ ╔╗║║ ╔╗║║ ║║║║ ╔╗║╚═╝║  ║║  ║║ ║║║╔╗╔╝ | r = Remainder
║╚═╝║║╔═╗║║╚═╝║║╚═╝║║╚═╝║║╚═╝║║╔═╗║  ║║  ║╚═╝║║║║╚╗ Special Modes:
╚═══╝╚╝ ╚╝╚═══╝╚═══╝╚═══╝╚═══╝╚╝ ╚╝  ╚╝  ╚═══╝╚╝╚═╝ | p = Index    | o = Root
            ", 0, true);//5
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(52, 1);
            type("Instructions:", 20, true);
            Console.SetCursorPosition(52, 5);
            type("Special Modes:", 20, true);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(58, 2);
            Console.Write("Add");
            Console.SetCursorPosition(58, 3);
            Console.Write("Multiply");
            Console.SetCursorPosition(58, 4);
            Console.Write("Remainder");

        Main:
            history += 1;
            Console.SetCursorPosition(0, 9);
            CalcId = CalcType.Null;
            type("Select Calc Mode:", 70, true);
        SetCalcMode:
            ClrCL();
            var pressed = Console.ReadKey().Key;
            if (pressed == ConsoleKey.A)
                CalcId = CalcType.Add;
            else if (pressed == ConsoleKey.S)
                CalcId = CalcType.Subtract;
            else if (pressed == ConsoleKey.M)
                CalcId = CalcType.Multiply;
            else if (pressed == ConsoleKey.D)
                CalcId = CalcType.Divide;
            else if (pressed == ConsoleKey.R)
                CalcId = CalcType.Remainder;
            else if (pressed == ConsoleKey.P)
                CalcId = CalcType.PowerIndex;
            else if (pressed == ConsoleKey.O)
                CalcId = CalcType.Root;
            else goto SetCalcMode;
            newl();

            type("Enter the first number:", 10, true);
            In1 = Console.ReadLine();
            double.TryParse(In1, out double No1);
            type("Enter the second number:", 10, true);
            In2 = Console.ReadLine();
            double.TryParse(In2, out double No2);
            double No3 = Mathf(CalcId, No1, No2);
            Console.WriteLine("Result:");
            Console.WriteLine(No3);
            pause(500);

            //add history
            Console.SetCursorPosition(100, history);
            Console.Write(No3);

            Console.SetCursorPosition(0, 17);
            for(int f = 0; f <= 7; f++)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1 );
                ClrCL();
            }
            goto Main;
        }
        public static void Similarity()
        {
            type(@"
╔══╗╔══╗╔═╦═╗╔══╗╔╗─╔══╗╔═╗╔══╗╔══╗╔═╦╗
║══╣╚║║╝║║║║║╚║║╝║║─║╔╗║║╬║╚║║╝╚╗╔╝╚╗║║
╠══║╔║║╗║║║║║╔║║╗║╚╗║╠╣║║╗╣╔║║╗─║║─╔╩╗║
╚══╝╚══╝╚╩═╩╝╚══╝╚═╝╚╝╚╝╚╩╝╚══╝─╚╝─╚══╝
"
            , 5, true);
            Console.ReadLine();
        }
        public static void Pi()
        {
            double pi = 0;
            int i = -1;
            while (true)
            {
                pi += 4.0d / (i += 2);
                pi -= 4.0d / (i += 2);
                Console.WriteLine(pi);
            }
        }
        public static void SquareoSquare()
        {
            //length 6
            /**/
            string SquareShapeRow1 = "███", SquareShapeRow2 = "█  █", SquareShapeRow3 = "███";
            while (true)
            {
                type("How many Square?", 10, true);
                int.TryParse(Console.ReadLine(), out int inputSquare);
                //Check Invalid Input
                if (inputSquare == 0)
                {
                    type("Invalid Input", 10, true);
                    pause(500);
                    Clear();
                    break;
                }

                //Draw Square
                int SquareRow = 1;
                int SquareEnter = 2;
                try
                {
                    for (int noSquare = 0; noSquare < inputSquare; noSquare++)
                    {
                        for (int RowSquare = 0; RowSquare < inputSquare; RowSquare++)
                        {
                            Console.SetCursorPosition(SquareRow, SquareEnter);
                            Console.Write(SquareShapeRow1);
                            Console.SetCursorPosition(SquareRow, SquareEnter + 1);
                            Console.Write(SquareShapeRow2);
                            Console.SetCursorPosition(SquareRow, SquareEnter + 2);
                            Console.Write(SquareShapeRow3);
                            SquareRow += 4;
                        }
                        SquareRow = 1;
                        SquareEnter += 2;
                    }
                    Console.WriteLine();
                } catch(ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(":)");
                }

                double result = 0, multiplier = 1, totalSquare = 0;

                for (int noSquare = 0; noSquare < inputSquare; noSquare++)
                {
                    result += multiplier;
                    //Console.WriteLine("Result = {0}",result);
                    multiplier += 2;
                    //Console.WriteLine("Multiplier = {0}",multiplier);
                    totalSquare += result;
                }
                type(totalSquare.ToString(), 10, true);
                pause(1000);
                Clear();
                //1 + 4 + 9 + 16
            }
        }




        //Quadratics

        public static void QuadraticsMenu()
        {
            Console.WriteLine(@"
----------------
QUADRATICS STUFF
----------------
1. Quadratic Formula Solver
2. Completing a Square
");
            Console.WriteLine("Select:");
        SetMode:
            ClrCL();
            var pressed = Console.ReadKey().Key;
            if (pressed == ConsoleKey.D1)
            {
                Clear();
                QuadraticFormula();
            }
            else if (pressed == ConsoleKey.D2)
            {
                Clear();
                CompletingSquare();
            }
            else goto SetMode;
            newl();

        }
        //Completing a Quadratics
        public static int history = 0;
        public static string[] historycont = new string[50];
        public static void CompletingSquare()
        {
            history += 1;
            historycont[0] = "History:";
            //write the history
            for (int i = 0; i < historycont.Length; i++)
            {
                Console.SetCursorPosition(50, i);
                Console.Write(historycont[i]);
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Select Mode:");
            Console.WriteLine(@"
1. ax^2 ± bx ± ___ = (x ± ___)^2
2. (x + a)^2 = b
");
            
            
        SetMode:
            ClrCL();
            var pressed = Console.ReadKey().Key;
            if (pressed == ConsoleKey.D1)
            {
                Clear();
                CS1();
            }
            else if (pressed == ConsoleKey.D2)
            {
                Clear();
                CS2();
            }
            else goto SetMode;
            newl();
        }
        public static void CS1()
        {
            string Sign;
            Console.WriteLine("ax^2 ± bx ± ___ = (x ± ___)^2");
            Console.Write("a = ");
            double.TryParse(Console.ReadLine(), out double a);
        SetSign:
            ClrCL();
            Console.Write("±");
            var pressed = Console.ReadKey().Key;
            if (pressed == ConsoleKey.OemMinus)
                Sign = "-";
            else if (pressed == ConsoleKey.OemPlus)
                Sign = "+";
            else goto SetSign;
            newl();
            //x/a/2*

            Console.Write("b = ");
            double.TryParse(Console.ReadLine(), out double b);
            Console.WriteLine("x^2 " + Sign + " " + (b / a) + "x + " + (b / a / 2 * b / a / 2) + " = (x " + Sign + " " + (b / a / 2) + ")^2");
            Console.WriteLine("x^2 " + Sign + " " + SimplifyFraction(b, a) + "x + " + SimplifyFraction(b * b, 2 * a * (2 * a)) + " = (x " + Sign + " " + SimplifyFraction(b, a * 2) + ")^2");
            Console.ReadLine();
            Clear();
            if ((b / a) == (int)(b / a) && (b / a / 2 * b / a / 2) == (int)(b / a / 2 * b / a / 2) && (b / a / 2) == (int)(b / a / 2))
                historycont[history] = "x^2 " + Sign + " " + (b / a) + "x + " + (b / a / 2 * b / a / 2) + " = (x " + Sign + " " + (b / a / 2) + ")^2";
            else
                historycont[history] = "x^2 " + Sign + " " + SimplifyFraction(b, a) + "x + " + SimplifyFraction(b * b, 2 * a * (2 * a)) + " = (x " + Sign + " " + SimplifyFraction(b, a * 2) + ")^2";
        }
        public static void CS2()
        {
            bool isSurd = true;
            double a;
            Console.WriteLine("(x + a)^2 = b");
            Console.Write("a = ");
            string InputA = Console.ReadLine();
            if (InputA.Contains("/"))
            {
                InputToFraction(InputA);
                a = convertedN / convertedD;
                Console.WriteLine(convertedN);
                Console.WriteLine(convertedD);
            }
            else if (double.TryParse(InputA, out a))
                double.TryParse(InputA, out a);




            Console.Write("b = ");
            double.TryParse(Console.ReadLine(), out double b);
            if (Math.Sqrt(b) == (int)Math.Sqrt(b))
                isSurd = false;
            //double x1, x2; Never Used...
            Console.WriteLine("x = " + -a + "√±" + b);
            Console.ReadLine();
            if (!isSurd)
                historycont[history] = (-a + Math.Sqrt(b).ToString());
            else
                historycont[history] = "x = " + -a + "√±" + b;
            convertedD = 0;
            convertedN = 0;
        }

        //Quadratic Formula
        struct QuadraticVariables
        {
            double a;

        }
        public static void QuadraticFormula()
        {
            double a, b, c;
            Console.WriteLine("ax^2 ±√b^2 - 4ac / 2a");

            Console.Write("a = ");
            while(!double.TryParse(Console.ReadLine(), out a))
                Console.Write("Invalid Input...\na = ");
            Console.Write("b = ");
            while (!double.TryParse(Console.ReadLine(), out b))
                Console.Write("Invalid Input...\nb = ");
            Console.Write("c = ");
            while (!double.TryParse(Console.ReadLine(), out c))
                Console.Write("Invalid Input...\nc = ");


            Console.WriteLine("-{1}±√{1}*{1} - 4*{0}*{2} / 2*{0}", a, b, c);

            bool isSurd;
            double surdSimplify, surd;
            
            QrFormula(a, b, c, out surdSimplify, out surd, out isSurd);
            Console.WriteLine("Is Surd: {0}", isSurd);
            
            if (surd == 0)
                Console.WriteLine("{0}±{1} / {2}", -b, surdSimplify, 2 * a);
            else if (surdSimplify == 1)
                Console.WriteLine("{0}±√{1} / {2}", -b, surd, 2 * a);
            else
                Console.WriteLine("{0}±{1}√{2} / {3}", -b, surdSimplify, surd, 2 * a);

            double finalDenominator, finalNumerator, surdMultiple, multiple;


            SimplifyQrFraction(-b, surdSimplify, 2 * a, out finalDenominator, out finalNumerator, out surdMultiple, out multiple);
            string Denominator, Numerator, SurdM, Surd;
            Denominator = " / " + finalDenominator.ToString();
            Numerator = finalNumerator.ToString();
            SurdM = surdMultiple.ToString();
            Surd = "√" + surd;
            if (finalDenominator == 1)
                Denominator = "";
            if (surdMultiple == 1 && surd != 0)
                SurdM = "";
            if (surd == 0)
                Surd = "";
            Console.WriteLine(Numerator + "±" + SurdM + Surd + Denominator + "  " + "(/{0})", multiple);
            Console.WriteLine();
            double x1 = (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
            double x2 = (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a);

            Console.WriteLine("({0} : {1})", x1, x2);
            Console.WriteLine("({0} : {1})", Math.Round(x1, 2), Math.Round(x2, 2));
            if (!isSurd)
            Console.WriteLine("{0} : {1}", SimplifyFraction(-b/multiple + surdMultiple, finalDenominator), SimplifyFraction(-b/multiple - surdMultiple, finalDenominator));
            Console.WriteLine();
            Console.Write(Numerator + "±" + SurdM + Surd);
            int equationLength = Console.CursorLeft;
            Console.WriteLine();
            if(finalDenominator != 1)
            {
                for (int i = equationLength; i > 0; i--)
                {
                    Console.Write("-");
                }
                Console.WriteLine();
                Console.CursorLeft = equationLength / 2;
                Console.WriteLine(finalDenominator);
            }
            

            Console.ReadLine();
            Clear();
        }
        public static double QrFormula(double a, double b, double c, out double simplify, out double surd, out bool isSurd)
        {
            double m = b * b - (4 * a * c);
            //No solution
            if (m < 0)
            {
                isSurd = true;
                simplify = 1;
                Console.WriteLine("No solution");
                surd = m;
                return 0;
            }
            double n = Math.Sqrt(m);
            //Perfect Square
            if(n == (int)n)
            {
                isSurd = false;
                simplify = n;
                surd = 0;
                return 0;
            }
            //Simplify
            for (int i = (int)Math.Floor(m); i >= 2; i--)
            {
                if(m % (i * i) == 0)
                {
                    Console.WriteLine("±√{0} = ±{1}√{2}", m, i, m / (i * i));
                    isSurd = true;
                    simplify = i;
                    surd = m / (i * i);
                    return 0;
                }
            }
            //Surd
            isSurd = true;
            simplify = 1;
            surd = m;
            return 0;
        }
        public static void SimplifyQrFraction(double a, double b, double c, out double finalDenominator, out double finalNumerator, out double simplifySurd, out double multiple)
        {
            finalNumerator = a;
            simplifySurd = b;
            finalDenominator = c;
            multiple = 1;
            for (double i = c; i > 0; i--)
            {
                if(a % i == 0 && b % i == 0 && c % i == 0)
                {
                    finalDenominator = c / i;
                    finalNumerator = a / i;
                    simplifySurd = b / i;
                    multiple = i;
                    break;
                }
            }
        }
        public static string SimplifyFraction(double a, double b)
        {
            for (double i = b; i > 0; i--)
            {
                if (a % i == 0 && b % i == 0)
                {
                    return (a / i).ToString() + "/" + (b / i).ToString();
                }
            }
            return (a + b).ToString();
        }
        public static int convertedN, convertedD;
        public static void InputToFraction(string input)
        {
            string[] fraction = input.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            int numerator, denominator;
            if (int.TryParse(fraction[0], out numerator) && int.TryParse(fraction[1], out denominator))
            {
                convertedN = numerator;
                convertedD = denominator;
            }
        }


        //new
        //Tic Tac Toe
        public static char n = ' ';
        public static char x = 'X';
        public static char o = 'O';
        public static char opponent = x;
        public struct Move
        {
            public int Row, Col;
        }
        //Check if the board is full
        public static bool MovesLeft(char[,] board)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i,j] == ' ')
                        return true;
            return false;
        }
        public static void GenerateBoard()
        {
            Console.SetCursorPosition(10, 5);
            Console.Write("   |   |   ");
            Console.SetCursorPosition(10, 6);
            Console.Write("--- --- ---");
            Console.SetCursorPosition(10, 7);
            Console.Write("   |   |   ");
            Console.SetCursorPosition(10, 8);
            Console.Write("--- --- ---");
            Console.SetCursorPosition(10, 9);
            Console.Write("   |   |   ");
        }
        public enum gridCoord
        {
            Null = 0,
            X1 = 11,
            X2 = 15,
            X3 = 19,
            Y1 = 5,
            Y2 = 7,
            Y3 = 9
        }
        public static int evaluate(char[,] board)
        {
            // Checking for Rows for X or O victory.
            for (int row = 0; row < 3; row++)
            {
                if (board[row,0] == board[row,1] &&
                    board[row,1] == board[row,2])
                {
                    if (board[row,0] == x)
                        return +10;
                    else if (board[row,0] == o)
                        return -10;
                }
            }

            // Checking for Columns for X or O victory.
            for (int col = 0; col < 3; col++)
            {
                if (board[0,col] == board[1,col] &&
                    board[1,col] == board[2,col])
                {
                    if (board[0,col] == x)
                        return +10;

                    else if (board[0,col] == o)
                        return -10;
                }
            }

            // Checking for Diagonals for X or O victory.
            if (board[0,0] == board[1,1] && board[1,1] == board[2,2])
            {
                if (board[0,0] == x)
                    return +10;
                else if (board[0,0] == o)
                    return -10;
            }

            if (board[0,2] == board[1,1] && board[1,1] == board[2,0])
            {
                if (board[0,2] == x)
                    return +10;
                else if (board[0,2] == o)
                    return -10;
            }

            // Else if none of them have won then return 0
            return 0;
        }

        static int max(int a, int b)
        {
            return (a > b) ? a : b;
        }
        static int min(int a, int b)
        {
            return ((a < b) ? a : b);
        }
        public static int minimax(char[,] board, int depth, bool isMax)
        {
            //Check if game is decide
            int score = evaluate(board);
            //returns -10, 10, 0
            //loss, win, ongoing or tie
            //if does not contain any, evaluate

            // If Maximizer has won the game return his/her
            // evaluated score
            if (score == 10)
                return score;

            // If Minimizer has won the game return his/her
            // evaluated score
            if (score == -10)
                return score;

            // If there are no more moves and no winner then
            // it is a tie
            if (MovesLeft(board) == false)
                return 0;

            // If this maximizer's move
            if (isMax)
            {
                int best = -1000;

                // Traverse all cells
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Check if cell is empty
                        if (board[i,j] == n)
                        {
                            // Make the move
                            board[i,j] = x;

                            // Call minimax recursively and choose
                            // the maximum value
                            best = max(best,
                                minimax(board, depth + 1, !isMax));

                            // Undo the move
                            board[i,j] = n;
                        }
                    }
                }
                return best;
            }

            // If this minimizer's move
            else
            {
                int best = 1000;

                // Traverse all cells
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Check if cell is empty
                        if (board[i,j] == n)
                        {
                            // Make the move
                            board[i,j] = o;

                            // Call minimax recursively and choose
                            // the minimum value
                            best = min(best,
                                minimax(board, depth + 1, !isMax));

                            // Undo the move
                            board[i,j] = n;
                        }
                    }
                }
                return best;
            }
        }
        public static Move findBestMove(char[,] board)
        {
            int bestVal = -1000;
            Move bestMove;
            bestMove.Row = -1;
            bestMove.Col = -1;
            

            // Traverse all cells, evaluate minimax function for
            // all empty cells. And return the cell with optimal
            // value.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty
                    if (board[i,j] == n)
                    {
                        // Make the move
                        board[i,j] = x;

                        // compute evaluation function for this
                        // move.
                        int moveVal = minimax(board, 0, false);

                        // Undo the move
                        board[i,j] = n;

                        // If the value of the current move is
                        // more than the best value, then update
                        // best/
                        if (moveVal > bestVal)
                        {
                            bestMove.Row = i;
                            bestMove.Col = j;
                            bestVal = moveVal;
                        }
                    }
                }
            }

            Console.WriteLine("Move Value {0}", bestVal);

            return bestMove;
        }
        public static void playOpponent(ref char[,] Board)
        {
            Move bestMove = findBestMove(Board);
            Console.SetCursorPosition(bestMove.Col, bestMove.Row);
            //Board[bestMove.Col, bestMove.Row] = opponent;
            
            if (opponent == x)
            {
                Console.Write("X");
            }
            if (opponent == o)
            {
                Console.Write("O");
            }
            opponent = (opponent == x) ? o : x;
            
        }
        public static void TicTacToe()
        {
            //━ ┏ ┛ ┗ ┓ ┃ ┫ ╋ ┣ ︱ ┻ ┳
            char[,] BoardStatus = new char[3, 3] {
                {o, x, o},
                {x, x, o},
                {o, x, x},
                };
            Move bestMove = findBestMove(BoardStatus);
            Console.WriteLine(bestMove.Col);
            Console.WriteLine(bestMove.Row);
            Console.ReadLine();
            /*
            newl();
            type("Ya Start!", 70, true);

        Start:
            ClrCL();


            Console.SetCursorPosition(0, 11);

            goto Start;
            */
        }
        

        static void Main(string[] args)
        {
            consoleHandle = NativeMethods.GetStdHandle(NativeMethods.STD_INPUT_HANDLE);
            NativeMethods.GetConsoleMode(consoleHandle, out originalConsoleMode);

            AppId = RunApp.Setup;
            while(true)
            {
                switch (AppId)
                {
                    case RunApp.Setup:
                        Setup();
                        break;
                    case RunApp.App:
                        //load("Loading applications", 0, 2);
                        Console.WindowWidth = 200;
                        Console.WindowHeight = 60;
                        Console.CursorVisible = false;
                        App();
                        break;
                    case RunApp.Calculator:
                        Console.CursorVisible = false;
                        Clear();
                        Calculator();
                        break;
                    case RunApp.Pi:
                        Pi();
                        break;
                    case RunApp.Similarity:
                        Similarity();
                        break;
                    case RunApp.TicTacToe:
                        TicTacToe();
                        break;
                    case RunApp.SquareoSquare:
                        SquareoSquare();
                        break;
                    case RunApp.QuadraticsMenu:
                        QuadraticsMenu();
                        break;
                    case RunApp.QuadraticFormula:
                        QuadraticFormula();
                        break;
                    case RunApp.CompletingSquare:
                        CompletingSquare();
                        break;
                }
            }
        }


        public class NativeMethods
        {

            public const Int32 STD_INPUT_HANDLE = -10;

            public const Int32 ENABLE_MOUSE_INPUT = 0x0010;
            public const Int32 ENABLE_QUICK_EDIT_MODE = 0x0040;
            public const Int32 ENABLE_EXTENDED_FLAGS = 0x0080;

            public struct INPUT_RECORD
            {
                public Int16 EventType;
                public MOUSE_EVENT_RECORD MouseEvent;
            }

            public struct MOUSE_EVENT_RECORD
            {
                public COORD dwMousePosition;
                public Int32 dwButtonState;
                public Int32 dwControlKeyState;
                public Int32 dwEventFlags;
            }
            public struct COORD
            {
                public ushort X;
                public ushort Y;
            }

            public class ConsoleHandle : SafeHandleMinusOneIsInvalid
            {
                public ConsoleHandle() : base(false) { }

                protected override bool ReleaseHandle()
                {
                    return true; //releasing console handle is not our business (LOL) 😡
                }
            }


            [DllImportAttribute("kernel32.dll", SetLastError = false)]
            public static extern ConsoleHandle GetStdHandle(Int32 nStdHandle);

            [DllImportAttribute("kernel32.dll", SetLastError = false)]
            public static extern bool ReadConsoleInput(ConsoleHandle hConsoleInput, ref INPUT_RECORD lpBuffer, UInt32 nLength, ref UInt32 lpNumberOfEventsRead);

            [DllImportAttribute("kernel32.dll", SetLastError = false)]
            public static extern bool SetConsoleMode(ConsoleHandle hConsoleHandle, Int32 dwMode);

			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool GetConsoleMode(ConsoleHandle hConsoleHandle, out Int32 lpMode);









			//Thread childThread = new Thread(childref);
			//Console.Beep(1000, 10000);
			//childThread.Start();
			//Console.Beep(1000, 10000);
			//Console.ReadLine();


		}
    }
}





/*Console.WriteLine(X);
Console.WriteLine(Y);
                Console.WriteLine(button);
                Console.WriteLine(string.Format("{0,4}", record.MouseEvent.dwMousePosition.X));
                Console.WriteLine(string.Format("{0,4}", record.MouseEvent.dwMousePosition.Y));
                Console.WriteLine(string.Format("{0,4}", record.MouseEvent.dwButtonState));
                Console.WriteLine(string.Format("    dwControlKeyState: 0x{0:X4}  ", record.MouseEvent.dwControlKeyState));
                Console.WriteLine(string.Format("{0:X4}", record.MouseEvent.dwEventFlags));
                */