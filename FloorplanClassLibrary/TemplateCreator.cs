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
        }
        public DiningArea DiningArea { get; set; }
        public List<Section> Sections { get; set; }
    }
}
