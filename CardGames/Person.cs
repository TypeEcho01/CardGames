using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using static Library.Methods;

namespace CardGames
{
    public class Person
    {
        public string Name;
        public List<Card> Hand;

        public Person(string name, params Card[] hand)
        {
            Name = name;
            Hand = [.. hand];  // [.. hand] unpacks the params Card[] hand into the Hand list
        }

        public override string ToString() => Name;

        public void Speak(params object?[] values)
        {
            ConfigurePrint(end: "");
            Print($"{Name}: \"");

            ConfigurePrint(end: "");
            Print(values);

            Print("\"");
        }
    }
}