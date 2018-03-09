using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
/* Hello Everybody.Welcome to our first action game.This game made for learn about coding skill by C# in Advanced Computer Programming */
/*Made by Thanaphoom Babparn (Mart)*/
/*Made by Siranee Lunput (Guitar)*/
/*Made by Piamsub Preneetham (Warm)*/
namespace Game
{
    class Program
    {
        private static Random _random = new Random();
        public static int Final = 0;

        //Random Color
        private static ConsoleColor GetRandomConsoleColor()
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)consoleColors.GetValue(_random.Next(consoleColors.Length));
        }

        static void Main(string[] args)
        {
            string[] PlayerName = new string[5];
            int[] Class = new int[5];
            int EnemyHP = 25000;
            int i;
            char[] separators = new char[] { ' ', ',', '.', '/' };
            int[] Mana = new int[5];
            int[] ATK = new int[5];
            //Setup Thread
            Thread[] threadsArray = new Thread[5];
            Console.WriteLine("\n\n                            Welcome to Attack on Target                            ");

            for (i = 0; i < threadsArray.Length; i++)
            {
                Console.WriteLine("\n================================================================================");
                Console.WriteLine("Enter 100 to skip create character\n");
                Console.Write("What's your name Player {0} : ", i + 1);
                string PiName = Console.ReadLine();
                if(PiName=="100")
                {
                    break;
                }
                PlayerName[i] = PiName;
                SelectClass:
                {
                    try
                    {
                        ShowClass();
                        Console.Write("\nSelect your Class Player {0} : ", i + 1);
                        Class[i] = int.Parse(Console.ReadLine());
                        if (Class[i] > 4 || Class[i] < 0)
                        {
                            Console.WriteLine("Input number 1-4");
                            goto SelectClass;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Something wrong. Please insert correct type of parameter");
                        Console.WriteLine("Press any key to re input");
                        Console.ReadLine();
                        goto SelectClass;
                    }

                }
                Console.WriteLine("\nInput your status for you (MP , ATK) e.g. 5/2 \n(This status can't upper and lower than 7 point)\n");         
                int[] MP = new int[5];
                int[] Atk = new int[5];
                InputStatus:
                {
                    try
                    {
                        Console.Write("Input your status : ");
                        string[] Character = Console.ReadLine().Split(separators);
                        int MP1 = Convert.ToInt32(Character[0]);
                        int ATK1 = Convert.ToInt32(Character[1]);
                        int CheckStatus = MP1 + ATK1;
                        if (CheckStatus > 7 || CheckStatus < 7)
                        {
                            Console.WriteLine("Something wrong. Please insert status 7 point");
                            Console.WriteLine("Press any key to re input");
                            Console.ReadLine();
                            goto InputStatus;
                        }
                        if (MP1 < 0 || ATK1 < 1)
                        {
                            Console.WriteLine("Min of ATK is 1 and Min of MP is 0\n");
                            goto InputStatus;
                        }
                        MP[i] = MP1;
                        Atk[i] = ATK1;
                        switch (MP1)
                        {
                            case 0: {   Mana[i] = 0;    break; }
                            case 1: {   Mana[i] = 340;  break; }
                            case 2: {   Mana[i] = 410;  break; }
                            case 3: {   Mana[i] = 500;  break; }
                            case 4: {   Mana[i] = 580;  break; }
                            case 5: {   Mana[i] = 650;  break; }
                            case 6: {   Mana[i] = 720;  break; }
                            case 7: {   Mana[i] = 800;  break; }
                        }
                        switch (ATK1)
                        {
                            case 1: { ATK[i] = 67; break; }
                            case 2: { ATK[i] = 74; break; }
                            case 3: { ATK[i] = 85; break; }
                            case 4: { ATK[i] = 94; break; }
                            case 5: { ATK[i] = 115; break; }
                            case 6: { ATK[i] = 140; break; }
                            case 7: { ATK[i] = 300; break; }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Something wrong. Please insert correct type of parameter");
                        Console.WriteLine("Press any key to re input");
                        Console.ReadLine();
                        goto InputStatus;
                    }
                }
                int thdNumber = i;
                ConsoleColor ConColor = new ConsoleColor();
                ConColor = GetRandomConsoleColor();
                ConColor = GetRandomConsoleColor();
                threadsArray[thdNumber] = new Thread(() => Attack(thdNumber,PlayerName[thdNumber],Class[thdNumber],EnemyHP,Mana[thdNumber],ATK[thdNumber],ConColor));
            }

            for (int kk = 0; kk < i; kk++)
            ShowTable(kk,PlayerName[kk],Class[kk],Mana[kk],ATK[kk]);

            //Check Y/N to Continue
            while (true)
            {
                Console.Write("Start Fight <Y/N> : ");
                string Accept = Console.ReadLine();
                if (Accept == "Y" || Accept == "y")
                {
                    break;
                }
                else if (Accept == "N" || Accept == "n")
                {
                    Console.WriteLine("\n================================================================================");
                    Console.WriteLine("Game setting terminate.");
                    Console.WriteLine("Close Program");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Please insert Y/N");
                }
            }

            for (int j = 0; j < i; j++)
            {
                int k = j;
                threadsArray[k].Start();
                Console.WriteLine("Player {0} Ready...START!!", k + 1);
            }

            getFinal();
            Console.WriteLine("Finished program");
            Console.ReadLine();
        }

        //Loop of Attack
        private static void Attack(int ID,string PlayerName,int Class1,int EnemyHP,int myMana,int myATK,ConsoleColor ConColor)
        {   int realATK = 1;
            do
            {
                if (Console.ForegroundColor != ConColor)
                {
                    Console.ResetColor();
                    Console.ForegroundColor = ConColor;
                }

                //Class Fighter Attack

                if (Class1 == 1)
                {   Random rndSkill = new Random();
                    int randomSkill = rndSkill.Next()%5;
                    switch (randomSkill)
                    {
                        case 0:
                                {
                                if (myMana >= 10)
                                {
                                    Console.WriteLine("Skill 1 : Bash [Active]");
                                    realATK = myATK * 5;
                                    myMana = myMana - 10;
                                    EnemyHP -= realATK;
                                    break;
                                }
                                else
                                {
                                    realATK = myATK;
                                    EnemyHP -= realATK;
                                    break;
                                }
                                
                                }
                        case 1:
                                {
                                if (myMana >= 20)
                                {
                                    Console.WriteLine("Skill 2 : Ignition Break [Active]");
                                    realATK = myATK * 13;
                                    myMana = myMana - 20;
                                    EnemyHP -= realATK;
                                    break;
                                }
                                else
                                {
                                    realATK = myATK;
                                    EnemyHP -= realATK;
                                    break;
                                }

                                }
                        case 2:
                                {
                                if (myMana >= 50)
                                {
                                    Console.WriteLine("Skill 3 : Guillotine [Active]");
                                    realATK = myATK * 23;
                                    myMana = myMana - 50;
                                    EnemyHP -= realATK;
                                    break;
                                }
                                else
                                {
                                    realATK = myATK;
                                    EnemyHP -= realATK;
                                    break;
                                }
                            }
                        case 3:
                                {
                                realATK = myATK;
                                EnemyHP -= realATK;
                                break;
                                }
                        case 4:
                                {
                                realATK=myATK;
                                EnemyHP -= realATK;
                                break;
                                }
                    }
                    if (EnemyHP <= 0) EnemyHP = 0;
                    Console.WriteLine(PlayerName + " " + "Turn atk : " + realATK + " Current Mana : " + myMana + " Enemy Hp :" + EnemyHP);
                    Thread.Sleep(2200);
                }

                //Class Wizard Attack

                else if (Class1 == 2)
                {
                    Random rndSkill = new Random();
                    int randomSkill = rndSkill.Next() % 5;
                    switch (randomSkill)
                    {
                        case 0:
                            {
                                if (myMana >= 12)
                                {
                                    Console.WriteLine("Skill 1 : Blast [Active]");
                                    realATK = myATK * 10;
                                    myMana = myMana - 12;
                                    EnemyHP -= realATK;
                                    break;
                                }
                                else
                                {
                                    realATK = myATK;
                                    EnemyHP -= realATK;
                                    break;
                                }

                            }
                        case 1:
                            {
                                if (myMana >= 35)
                                {
                                    Console.WriteLine("Skill 2 : Physical Waves [Active]");
                                    realATK = myATK * 20;
                                    myMana = myMana - 35;
                                    EnemyHP -= realATK;
                                    break;
                                }
                                else
                                {
                                    realATK = myATK;
                                    EnemyHP -= realATK;
                                    break;
                                }

                            }
                        case 2:
                            {
                                if (myMana >= 100)
                                {
                                    Console.WriteLine("Skill 3 : Comet [Active]");
                                    realATK = myATK * 50;
                                    myMana = myMana - 100;
                                    EnemyHP -= realATK;
                                    break;
                                }
                                else
                                {
                                    realATK = myATK;
                                    EnemyHP -= realATK;
                                    break;
                                }
                            }
                        case 3:
                            {
                                realATK = myATK;
                                EnemyHP -= realATK;
                                break;
                            }
                        case 4:
                            {
                                realATK = myATK;
                                EnemyHP -= realATK;
                                break;
                            }
                    }
                    if (EnemyHP <= 0) EnemyHP = 0;
                    Console.WriteLine(PlayerName + " " + "Turn atk : " + realATK + " Current Mana : " + myMana + " Enemy Hp :" + EnemyHP);
                    Thread.Sleep(3000);
                }

                //Class Archer Attack

                else if (Class1 == 3)
                {
                    Random rndSkill = new Random();
                    int randomSkill = rndSkill.Next() % 5;
                    switch (randomSkill)
                    {
                        case 0:
                            {
                                if (myMana >= 5)
                                {
                                    Console.WriteLine("Skill 1 : Double Arrow [Active]");
                                    realATK = myATK * 4;
                                    myMana = myMana - 5;
                                    EnemyHP -= realATK;
                                    break;
                                }
                                else
                                {
                                    realATK = myATK;
                                    EnemyHP -= realATK;
                                    break;
                                }

                            }
                        case 1:
                            {
                                if (myMana >= 18)
                                {
                                    Console.WriteLine("Skill 2 : Focus Arrow Strike [Active]");
                                    realATK = myATK * 9;
                                    myMana = myMana - 18;
                                    EnemyHP -= realATK;
                                    break;
                                }
                                else
                                {
                                    realATK = myATK;
                                    EnemyHP -= realATK;
                                    break;
                                }

                            }
                        case 2:
                            {
                                if (myMana >= 85)
                                {
                                    Console.WriteLine("Skill 3 : Dragon Arrow [Active]");
                                    realATK = myATK * 26;
                                    myMana = myMana - 50;
                                    EnemyHP -= realATK;
                                    break;
                                }
                                else
                                {
                                    realATK = myATK;
                                    EnemyHP -= realATK;
                                    break;
                                }
                            }
                        case 3:
                            {
                                realATK = myATK;
                                EnemyHP -= realATK;
                                break;
                            }
                        case 4:
                            {
                                realATK = myATK;
                                EnemyHP -= realATK;
                                break;
                            }
                    }
                    if (EnemyHP <= 0) EnemyHP = 0;
                    Console.WriteLine(PlayerName + " " + "Turn atk : " + realATK + " Current Mana : " + myMana + " Enemy Hp :" + EnemyHP);
                    Thread.Sleep(1500);
                }

                //Class Ninja Attack

                else if (Class1 == 4)
                {
                    Random rndSkill = new Random();
                    int randomSkill = rndSkill.Next() % 5;
                    switch (randomSkill)
                    {
                        case 0:
                            {
                                if (myMana >= 5)
                                {
                                    Console.WriteLine("Skill 1 : Blow Kick [Active]");
                                    realATK = myATK * 2;
                                    myMana = myMana - 5;
                                    EnemyHP -= realATK;
                                    break;
                                }
                                else
                                {
                                    realATK = myATK;
                                    EnemyHP -= realATK;
                                    break;
                                }

                            }
                        case 1:
                            {
                                if (myMana >= 45)
                                {
                                    Console.WriteLine("Skill 2 : Rasengan [Active]");
                                    realATK = myATK * 15;
                                    myMana = myMana - 45;
                                    EnemyHP -= realATK;
                                    break;
                                }
                                else
                                {
                                    realATK = myATK;
                                    EnemyHP -= realATK;
                                    break;
                                }

                            }
                        case 2:
                            {
                                if (myMana >= 72)
                                {
                                    Console.WriteLine("Skill 3 : Chakra Punch [Active]");
                                    realATK = myATK * 25;
                                    myMana = myMana - 72;
                                    EnemyHP -= realATK;
                                    break;
                                }
                                else
                                {
                                    realATK = myATK;
                                    EnemyHP -= realATK;
                                    break;
                                }
                            }
                        case 3:
                            {
                                realATK = myATK;
                                EnemyHP -= realATK;
                                break;
                            }
                        case 4:
                            {
                                realATK = myATK;
                                EnemyHP -= realATK;
                                break;
                            }
                    }
                    if (EnemyHP <= 0) EnemyHP = 0;
                    Console.WriteLine(PlayerName + " " + "Turn atk : " + realATK + " Current Mana : " + myMana + " Enemy Hp :" + EnemyHP);
                    Thread.Sleep(1000);
                }

                if (EnemyHP == 0)
                {
                    Console.ForegroundColor = ConColor;
                    Console.WriteLine("Player {0} : Enemy Down", ID + 1);
                    break;
                }
            }
            while (EnemyHP > 0 && Final == 0);
            Console.ResetColor();
            return;
        }


        //Finish Program
        private static void getFinal()
        {
            do
            {
                try
                {
                    Console.WriteLine("Press any key to stop Attack");
                    Final = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Final = 1;
                }
            } while (Final == 0);
            Console.ResetColor();
        }

        //Show Select Class
        public static void ShowClass()
        {
            Console.WriteLine("\n================================================================================");
            Console.WriteLine("                                      Class                                     ");
            Console.WriteLine("================================================================================");
            Console.WriteLine("Number 1 : Swordman [ Sword , strength and power to beat enemy ]");
            Console.WriteLine("Number 2 : Wizard   [        Power of magic to beat enemy      ]");
            Console.WriteLine("Number 3 : Archer   [       Long range stike to beat enemy     ]");
            Console.WriteLine("Number 4 : Ninja    [         Quick strike to beat enemy       ]");
            Console.Write("\n================================================================================");

        }

        // Show Table
        public static void ShowTable(int ID,string nameP1,int ClassP1, int mpP1, int atkP1)
        {
            Console.Write("================================================================================");
            Console.WriteLine("                                    Player {0}                                  ",ID+1);
            Console.Write("================================================================================");
            Console.WriteLine("Name  : {0}", nameP1);
            switch (ClassP1)
            {
                case 1: { Console.WriteLine("Class : Swordman"); break; }
                case 2: { Console.WriteLine("Class : Wizard"); break; }
                case 3: { Console.WriteLine("Class : Archer"); break; }
                case 4: { Console.WriteLine("Class : Ninja"); break; }
            }
            Console.WriteLine("MP    = {0}", mpP1);
            Console.WriteLine("Atk   = {0}", atkP1);
            Console.WriteLine("================================================================================");
        }
    }
}
