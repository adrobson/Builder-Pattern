using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {

            Maze aMaze;

            MazeGame mazegame = new MazeGame();
            StandardMazeBuilder builder = new StandardMazeBuilder();

            mazegame.CreateMaze(builder);
            aMaze = builder.GetMaze();

            //using CountingMazeBuilder
            int rooms, doors;

            MazeGame game = new MazeGame();
            CountingMazeBuilder cBuilder = new CountingMazeBuilder();

            game.CreateMaze(cBuilder);
            cBuilder.GetCounts(out rooms, out doors);

            //Note - Abstract Factory is similar to Builder in that it too may construct complex objects
            //The primary difference is that the Builder focusses on constructing step by step. 
            //AF emphasis is on families of product objects. builder returns the object in the final step
        }
    }
}
