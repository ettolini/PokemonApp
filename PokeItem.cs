using System;
using System.Collections.Generic;
using System.Text;

//Define your PokeItem model which will have a Name, and a Url.
namespace PokemonApp
{
    //Make your class public, since by default it is internal.
    public class PokeItem
    {
        //Define the constructor of your PokeItem which is the same name as class, and is not returning anything.
        //Will take a string name
        public PokeItem(string id, string height, string weight, string types)
        {
            Id = id;
            Height = height;
            Weight = weight;
            Types = types;
        }
        //Your Properties are auto-implemented.
        public string Id { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Types { get; set; }
    }
}