using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class TemplateCreator
    {
        public TemplateCreator(DiningArea diningArea)
        {
            this.DiningArea = diningArea;
            Template = new FloorplanTemplate();
        }
        public DiningArea DiningArea { get; set; }
        public List<Section> Sections { get; set; } = new List<Section>();
        public FloorplanTemplate Template { get; set; }
        public Section SelectedSection { get; set; }
    }
}
