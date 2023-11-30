namespace FloorplanClassLibrary
{
    public class Server
    {
        private List<Shift> shifts;

        public int ID { get; set; }
        public string Name { get; set; }
        //public Section? Section { get; private set; }
        //public void AssignToSection(Section section)
        //{
        //    Section = section;
        //}
        //public void RemoveFromSection()
        //{
        //    Section = null;
        //}
       

        private Section? _currentSection;
        public Section CurrentSection
        {
            get => _currentSection;
            set
            {
                if (_currentSection != value)
                {
                    _currentSection = value;
                    
                }
            }
        }

        public List<Shift> Shifts
        {
            get
            {
               
                shifts ??= new List<Shift>();

                return shifts.OrderByDescending(shift => shift.Date).ToList();
            }
            set
            {
                shifts = value;
            }
        }

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
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Server other = (Server)obj;
            return this.ID == other.ID;
        }
        //public event Action<Section> AssignedToSection;
        //public event Action<Section> RemovedFromSection;

        //public void NotifyAssignedToSection(Section section)
        //{
        //    AssignedToSection?.Invoke(section);
        //}
        //public void NotifyRemovedFromSection()
        //{
        //    RemovedFromSection?.Invoke(null);
        //}
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}