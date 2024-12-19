using System;
using System.Collections.Generic;

namespace Project.Scripts.Fishing {
    [Serializable]
    public struct Specie
    {
        public string name;
        public int value;
        public int weight;
    }

    [Serializable]
    public struct Prefix
    {
        public string name;
        public int value;
        public int weight;
    }

    [Serializable]
    public struct Attribute
    {
        public string name;
        public int value;
        public int weight;
    }

    [Serializable]
    public class FishingPool
    {
        public string poolname;
        public List<Specie> specie;
        public List<Prefix> prefix;
        public List<Attribute> attribute;
    }
}

