using System;


//Define your PokeItem model which will have a Name, and a Url.
namespace PokemonApp
{
    //Make your class public, since by default it is internal.
    public class PokeItem
    {
        //Define the constructor of your PokeItem which is the same name as class, and is not returning anything.
        //Will take a string name
        public PokeItem(int id, int height, int weight)
        {
            Id = id;
            Height = height;
            Weight = weight;
        }
        //Your Properties are auto-implemented.
        public int Id { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}