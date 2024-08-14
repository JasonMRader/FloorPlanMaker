using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FloorplanClassLibrary
{
    public class Server
    {
        private List<EmployeeShift> shifts;

        public int ID { get; set; }
        public int? HSID { get; set; }
        public string Name { get; set; }
        public bool isDouble { get; set; }
        public bool Archived { get; set; }
        public string DisplayName { get; set; }
        public int CocktailPreference { get; set; }
        public int CloseFrequency { get; set; }
        public bool IsBartender
        {
            get
            {
                return Name.StartsWith("BAR");
            }
        }
       
        public int TeamWaitFrequency { get; set; }
        public int OutsideFrequency { get; set; }
        public int PreferedSectionWeight { get; set; }
        public float SalesFromPickupSection
        {
            get
            {
                if(pickUpSections.Count() > 0)
                {
                    return pickUpSections.Sum(s => s.ExpectedSalesPerServer);
                }
                return 0;
            }
        }
        public List<Section> pickUpSections { get; set; } = new List<Section>();
        public float lastTenOutsideRatio
        {
            get
            {
                var lastShiftsForPercentage = this.Shifts.Take(10);
                int OutsideShifts = 0;
                foreach (var shift in lastShiftsForPercentage)
                {
                    if (!shift.IsInside)
                    {
                        OutsideShifts += 1;
                    }
                }
                return ((float)OutsideShifts / (float)lastShiftsForPercentage.Count())*10;
            }
        }
        public float AdjustedOutsidePriority
        {
            get
            {
                return (float)OutsideFrequency - lastTenOutsideRatio;
            }
        }
        public string AdjustedOutsideDisplay
        {
            get
            {
                return (AdjustedOutsidePriority*10).ToString() +"%";
            }
        }

       

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
                return Name; 
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
                return Name;
            }
        }
        public bool IsInTeamSection
        {
            get
            {
                if(this.CurrentSection == null)
                {
                    return false;
                }
                if(this.CurrentSection.IsTeamWait)
                {
                    return true;
                }
                return false;
            }
        }
        public string LastName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name))
                    return "";

                var nameParts = Name.Split(' ');
                if (nameParts.Length > 1)
                {
                    return $"{nameParts[nameParts.Length - 1]}";
                }
                return Name; 
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