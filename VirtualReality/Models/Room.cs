﻿namespace VirtualReality.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public House House { get; set; }
    }
}
