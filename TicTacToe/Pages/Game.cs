using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Pages
{
    public class Game
    {
        public Color CurrentTurn { get; set; }
        public Checker[,] Board = new Checker[8, 8];
        public bool GameOn { get; set; }

        public Game()
        {
            GameOn = true;

            for(int i = 0; i < 8; i+=2)
            {
                //Black Rows
                Board[i, 0] = new Checker(Color.Gray, i, 0);
                Board[i + 1, 1] = new Checker(Color.Gray, i + 1, 1);
                Board[i, 2] = new Checker(Color.Gray, i, 2);

                //Red Rows
                Board[i + 1, 5] = new Checker(Color.Purple, i + 1, 5);
                Board[i, 6] = new Checker(Color.Purple, i, 6);
                Board[i + 1, 7] = new Checker(Color.Purple, i + 1, 7);
            }

            CurrentTurn = Color.Purple;
        }

        public string Serialize()
        {
            string state = GameOn.ToString() + "\n";
            state += CurrentTurn.ToString() + "\n";
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if(Board[i,j] != null)
                    {
                        Checker checker = Board[i, j];
                        state += $"{checker.Color}|{checker.King}|{checker.Coords.X}|{checker.Coords.Y}\n";
                    }
                }
            }
            return state;
        }

        public static Game Marshall(string state)
        {
            string[] parts = state.Split("\n");
            Game game = new Game();
            game.GameOn = bool.Parse(parts[0]);
            game.CurrentTurn = (Color)Enum.Parse(typeof(Color), parts[1]);
            game.Board = new Checker[8, 8];
            for(int i = 2; i < parts.Length - 1; i++)
            {
                string[] pieces = parts[i].Split("|");
                Color color = pieces[0].Equals("Gray") ? Color.Gray : Color.Purple;
                bool king = bool.Parse(pieces[1]);
                int x = int.Parse(pieces[2]);
                int y = int.Parse(pieces[3]);
                game.Board[x,y] = new Checker(color, x, y, king);
            }
            return game;
        }
    }
}
