using System;

namespace AOC2021_Day21
{
    class Program
    {
        static ulong[] playerWinCount = new ulong[2];
        static int[] diracScores = { 3, 4, 5, 6, 7, 8, 9 };
        static ulong[] diracScoresCount = { 1, 3, 6, 7, 6, 3, 1 };
        static void Main(string[] args)
        {
            int[] PlayerPos = new int[2];
            PlayerPos[0] = 8;
            PlayerPos[1] = 5;

            int[] PlayerScore = new int[2];
            PlayerScore[0] = 0;
            PlayerScore[1] = 0;
            int DDie = 1;
            int turnCount = 0;
            bool Player1Turn = true;

            bool useDirac = true;

            if (useDirac)
            {
                for (int dieCount = 0; dieCount < diracScores.Length; dieCount++)
                {

                    PlayDiracRound(diracScores[dieCount], diracScoresCount[dieCount], Player1Turn, PlayerPos, PlayerScore);
                }
             

                Console.WriteLine("Player 1 Wins " + playerWinCount[0]);
                Console.WriteLine("Player 2 Wins " + playerWinCount[1]);
                Console.ReadLine();
            }
            else
            {
                while (PlayerScore[0] < 1000 && PlayerScore[1] < 1000)
                {
                    int PlayerIndex;
                    if (Player1Turn)
                    {
                        PlayerIndex = 0;
                    }
                    else
                    {
                        PlayerIndex = 1;
                    }

                    int dieTotal = 0;
                    for (int dieCount = 0; dieCount < 3; dieCount++)
                    {
                        dieTotal += DDie++;
                        if (DDie == 101)
                        {
                            DDie = 1;
                        }
                    }

                    PlayerPos[PlayerIndex] += dieTotal;
                    if (PlayerPos[PlayerIndex] % 10 == 0)
                    {
                        PlayerPos[PlayerIndex] = 10;
                    }
                    else
                    {
                        PlayerPos[PlayerIndex] = PlayerPos[PlayerIndex] % 10;
                    }
                    PlayerScore[PlayerIndex] += PlayerPos[PlayerIndex];

                    Player1Turn = !Player1Turn;
                    turnCount += 3;

                }
                Console.WriteLine(turnCount * Math.Min(PlayerScore[0], PlayerScore[1]));
            }
        }

        private static void PlayDiracRound(int dieTotal, ulong dieClones, bool player1Turn, int[] PlayerPos, int[] PlayerScore)
        {
            PlayerPos = (int[])PlayerPos.Clone();
            PlayerScore = (int[])PlayerScore.Clone();
            int PlayerIndex;
            if(player1Turn)
            {
                PlayerIndex = 0;
            }
            else
            {
                PlayerIndex = 1;
            }

            PlayerPos[PlayerIndex] += dieTotal;
            if (PlayerPos[PlayerIndex] % 10 == 0)
            {
                PlayerPos[PlayerIndex] = 10;
            }
            else
            {
                PlayerPos[PlayerIndex] = PlayerPos[PlayerIndex] % 10;
            }
            PlayerScore[PlayerIndex] += PlayerPos[PlayerIndex];

            if (PlayerScore[PlayerIndex] > 20)
            {
                playerWinCount[PlayerIndex] += dieClones;
            }
            else
            {
                for (int dieCount = 0; dieCount < diracScores.Length; dieCount++)
                {

                    PlayDiracRound(diracScores[dieCount], diracScoresCount[dieCount] * dieClones, !player1Turn, PlayerPos, PlayerScore);
                }
            }
        }
    }
}
