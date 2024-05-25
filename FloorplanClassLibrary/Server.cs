using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FloorplanClassLibrary
{
    public class Server
    {
        private List<EmployeeShift> shifts;

        public int ID { get; set; }
        public string Name { get; set; }
        public bool isDouble { get; set; }
        public bool Archived { get; set; }
        public string DisplayName { get; set; }
       

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

        public List<EmployeeShift> Shifts
        {
            get
            {
               
                shifts ??= new List<EmployeeShift>();

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
        public string FirstName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name))
                    return "";

                var nameParts = Name.Split(' ');
                if (nameParts.Length > 1)
                {
                    return $"{nameParts[0]}";
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
       
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
        public override string ToString()
        {
            if(this.DisplayName != null && this.DisplayName != "")
            {
                return this.DisplayName;
            }
            return this.AbbreviatedName;
        }

    }

}