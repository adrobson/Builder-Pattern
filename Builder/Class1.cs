using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public abstract class MazeBuilder
    {
        public virtual void BuildMaze()
        {

        }
        public virtual void BuildRoom(int room)
        {

        }
        public virtual void BuildDoor(int roomFrom, int roomTo)
        {

        }
        public virtual Maze GetMaze()
        {
            return null;
        }
    }

    public class StandardMazeBuilder : MazeBuilder
    {
        public StandardMazeBuilder()
        {
        }
        public override void BuildMaze()
        {
            _currentMaze = new Maze();
        }
        public override void BuildRoom(int roomNo)
        {
            if (_currentMaze != null)
            {
                Room room = new Room(roomNo);
                _currentMaze.AddRoom(room);

                room.SetSide(Direction.North, new Wall());
                room.SetSide(Direction.South, new Wall());
                room.SetSide(Direction.East, new Wall());
                room.SetSide(Direction.West, new Wall());
            }
        }
        public override void BuildDoor(int roomFrom, int roomTo)
        {
            Room r1 = _currentMaze.Rooms.Where(n => n._roomNumber == roomFrom).FirstOrDefault();
            Room r2 = _currentMaze.Rooms.Where(n => n._roomNumber == roomTo).FirstOrDefault();

            Door d = new Door(r1, r2);

            r1.SetSide(CommonWall(r1, r2), d);
            r2.SetSide(CommonWall(r2, r1), d);
        }
        public override Maze GetMaze()
        {
            return _currentMaze;
        }

        private Direction CommonWall(Room roomFrom, Room roomTo)
        {
            //implement function to determine direction of wall between two rooms
            return Direction.East;
        }

        private Maze _currentMaze;
    }

    public class CountingMazeBuilder : MazeBuilder
    {
        public CountingMazeBuilder()
        {
            _rooms = _doors = 0;
        }

        public void BuildRoom()
        {
            _rooms++;
        }

        public void BuildDoor()
        {
            _doors++;
        }
        public virtual void AddWall(int w, Direction d)
        {

        }

        public void GetCounts(out int d, out int r)
        {
            d = _doors;
            r = _rooms;
        }

        private int _doors;
        private int _rooms;
    }
    public class MazeGame
    {
        public Maze CreateMaze(MazeBuilder builder)
        {
            builder.BuildMaze();

            builder.BuildRoom(1);
            builder.BuildRoom(2);
            builder.BuildDoor(1, 2);

            return builder.GetMaze();
        }
    }

    public class Maze
    {
        public List<Room> Rooms { get; private set; }
        public Maze()
        {
            Rooms = new List<Room>();
        }
        public void AddRoom(Room r1)
        {
            Rooms.Add(r1);
        }
    }

    public abstract class MapSite
    {
        public virtual void Enter()
        {

        }
    }

    public class Room : MapSite
    {
        public Dictionary<Direction, MapSite> Sides
        {
            get; private set;
        }
        public int _roomNumber { get; private set; }

        public Room(int roomNo)
        {
            _roomNumber = roomNo;
            Sides = new Dictionary<Direction, MapSite>();
        }

        public void SetSide(Direction direction, MapSite m)
        {
            Sides.Add(direction, m);
        }
        public override void Enter()
        {

        }
    }

    public class EnchantedRoom : Room
    {
        public EnchantedRoom(int roomNo, Spell spell) : base(roomNo)
        {
            _spell = spell;
        }

        private Spell _spell;
    }

    public class RoomWithABomb : Room
    {
        public RoomWithABomb(int roomNo) : base(roomNo)
        {
        }
    }

    public class Spell
    {

    }

    public class Wall : MapSite
    {
        public override void Enter()
        {

        }
    }

    public class BombedWall : Wall
    {

    }

    public class Door : MapSite
    {
        public Door(Room r1, Room r2)
        {
            _room1 = r1;
            _room2 = r2;
        }
        public override void Enter()
        {

        }

        private Room _room1;
        private Room _room2;
        private bool _isOpen;
    }

    public enum Direction { North, South, East, West };
}
