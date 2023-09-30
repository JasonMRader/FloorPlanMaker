﻿namespace FloorplanClassLibrary
{
    public class Server
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Section> Sections { get; set; }
        public List<Shift> Shifts { get; set; }
        public string AbbreviatedName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name))
                    return "";

                var nameParts = Name.Split(' ');
                if (nameParts.Length > 1)
                {
                    return $"{nameParts[0]} {nameParts[1][0]}.";
                }
                return Name; // If there's only one part, return it as is.
            }
        }
    }
}