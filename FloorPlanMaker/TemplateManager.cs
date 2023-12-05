using FloorplanClassLibrary;
using FloorPlanMakerUI.Properties;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public class TemplateManager
    {
        public event EventHandler PreviewTemplateClicked;
        public event EventHandler ApplyTemplateClicked;
        public event EventHandler CancelPreviewedTemplate;
        public DiningArea DiningArea { get; set; }
        public int serverCount = 5;
        public List<MiniTableControl> MiniTableControls { get; set; } = new List<MiniTableControl>();
        public Panel[] DisplayPanels {  get; set; }
       
       
        public List<PictureBox> picSectionLabels = new List<PictureBox>();
        private bool _filterTeamYes { get; set; } = false;        
        private bool _filterPickYes { get; set; } = false;
        private bool _hasTeamFilter { get; set; } = false;
        private bool _hasPickFilter { get; set; } = false;
       // private bool isFiltered { get; set; } = false;
        public Dictionary<FloorplanTemplate, List<MiniTableControl>> TemplateMiniTables { get; set; } 
            = new Dictionary<FloorplanTemplate, List<MiniTableControl>>();
        public Dictionary<Section, List<MiniTableControl>> SectionMiniTableControls { get; set; }
            = new Dictionary<Section, List<MiniTableControl>>();

        public bool HasTeamFilter
        {
            get { return _hasTeamFilter; }
            set
            {
                _hasTeamFilter = value;
                SetFilter();
            }
        }

        public bool FilterTeamYes
        {
            get { return _filterTeamYes; }
            set
            {
                _filterTeamYes = value;
                if (_hasTeamFilter)
                    SetFilter();
            }
        }

        public bool HasPickFilter
        {
            get { return _hasPickFilter; }
            set
            {
                _hasPickFilter = value;
                SetFilter();
            }
        }

        public bool FilterPickYes
        {
            get { return _filterPickYes; }
            set
            {
                _filterPickYes = value;
                if (_hasPickFilter)
                    SetFilter();
            }
        }
        public void SetFilter()
        {
            _filteredList = new List<FloorplanTemplate>();
            FilterOption option = FilterOption.None;

            if (_hasTeamFilter && _hasPickFilter)
            {
                if (_filterTeamYes && _filterPickYes)
                    option = FilterOption.TeamTrue_And_PickTrue;
                else if (_filterTeamYes && !_filterPickYes)
                    option = FilterOption.TeamTrue_And_PickFalse;
                else if (!_filterTeamYes && _filterPickYes)
                    option = FilterOption.TeamFalse_And_PickTrue;
                else
                    option = FilterOption.TeamFalse_And_PickFalse;
            }
            else if (_hasTeamFilter)
            {
                option = _filterTeamYes ? FilterOption.TeamWaitTrueOnly : FilterOption.TeamWaitFalseOnly;
            }
            else if (_hasPickFilter)
            {
                option = _filterPickYes ? FilterOption.PickUpTrueOnly : FilterOption.PickUpFalseOnly;
            }

            SetFilters(option);
        }
        public TemplateManager(DiningArea area)
        {
            this.DiningArea = area;
            SectionMiniTableControls = new Dictionary<Section, List<MiniTableControl>>();
        }
        private List<FloorplanTemplate> _templates;
        public List<FloorplanTemplate> Templates
        {
            get => _templates;
            set
            {
                _templates = value;
                //UpdateFilteredList();  // Update FilteredList every time Templates is set
                SetFilter();
            }
        }

        //private void UpdateFilteredList()
        //{
        //    SetFilter();  
        //}

        public TemplateManager()
        {
            Templates = new List<FloorplanTemplate>();  
            
            SetFilter();
        }
        private List<FloorplanTemplate> _filteredList;
        public List<FloorplanTemplate> GetFilteredList()
        {
            return _filteredList;
            
            
        }

        public void InitializeMiniTableControls(FloorplanTemplate template)
        {           
            List<MiniTableControl> miniTableControls = new List<MiniTableControl>();
            template.GetTemplateTables();
            foreach (var table in template.Tables)
            {
                float factor = .4f; 
                int yAdjustment = 27; 
                MiniTableControl miniTableControl = new MiniTableControl(table, factor, yAdjustment);
                miniTableControls.Add(miniTableControl);
            }
            TemplateMiniTables[template] = miniTableControls;
           
        }
        public void DisplayMiniTableControls(FloorplanTemplate selectedTemplate, Panel panel)
        {
            if (TemplateMiniTables.TryGetValue(selectedTemplate, out List<MiniTableControl> miniTables))
            {
                foreach (var miniTable in miniTables)
                {
                    panel.Controls.Add(miniTable);
                }
                CreateSectionPicBox(selectedTemplate, panel);
            }
        }
        

       
        public void CreateSectionPicBox(FloorplanTemplate template, Panel panel)
        {
            foreach(Section section in template.Sections)
            {
               Panel displayPanel = new Panel()
                {
                    BackColor = Color.Black,
                    Size = new Size(34, 34),
                    Location = section.MiniMidPoint,
                   

                   
                };
                displayPanel.Left = displayPanel.Left - 17;
                displayPanel.Top = displayPanel.Top - 17;
                PictureBox picBox = new PictureBox()
                {
                    BackColor = section.Color,
                    Size = new Size(28,28),
                    Location = new Point(3,3),
                    Image = Resources.waiter,
                    SizeMode = PictureBoxSizeMode.Zoom
                };
                if(section.TemplateTeamWait)
                {
                    picBox.Image = Resources.waiters;
                }
                if(section.IsPickUp)
                {
                    picBox.Image = null;
                }
               // picSectionLabels.Add(picBox);
                displayPanel.Controls.Add(picBox);
                panel.Controls.Add(displayPanel);
                displayPanel.BringToFront();
            }
        }
        

        public enum FilterOption
        {
            None,
            TeamWaitTrueOnly,
            TeamWaitFalseOnly,
            PickUpTrueOnly,
            PickUpFalseOnly,
            TeamTrue_And_PickFalse,
            TeamTrue_And_PickTrue,
            TeamFalse_And_PickFalse,
            TeamFalse_And_PickTrue
        }
        public void GetTemplatesForFloorplan(Floorplan floorplan)
        {
            //this.Templates.Clear();
            this.Templates = SqliteDataAccess.LoadTemplatesByDiningAreaAndServerCount(floorplan.DiningArea, floorplan.Servers.Count);
            UpdateSectionNumbers();
            this.FilterTemplates(serverCount: serverCount);
        }
        public void GetAllFloorplanTemplates()
        {
            //this.Templates.Clear();
            this.Templates = SqliteDataAccess.LoadAllFloorplanTemplates();
            UpdateSectionNumbers();
            this.FilterTemplates(serverCount: serverCount);

        }
        private void UpdateSectionNumbers()
        {
            foreach(FloorplanTemplate template in  this.Templates)
            {
                template.AssignSectionNumbers();
                //template.GetTemplateTables();
                InitializeMiniTableControls(template);
            }
        }
        
        public void SetFilters(FilterOption option)
        {
           // isFiltered = true;
            switch(option)
            {
                case FilterOption.TeamWaitTrueOnly:
                    FilterTemplates(serverCount, hasTeamWait: true);
                    break;
                case FilterOption.TeamWaitFalseOnly:
                    FilterTemplates(serverCount, hasTeamWait: false);
                    break;
                case FilterOption.PickUpTrueOnly:
                    FilterTemplates(serverCount, hasPickUp: true);
                    break;
                case FilterOption.PickUpFalseOnly:
                    FilterTemplates(serverCount, hasPickUp: false); 
                    break;
                case FilterOption.TeamTrue_And_PickFalse:
                    FilterTemplates(serverCount, hasTeamWait: true, hasPickUp: false);
                    break;
                case FilterOption.TeamTrue_And_PickTrue:
                    FilterTemplates(serverCount, hasTeamWait: true, hasPickUp: true);
                    break;
                case FilterOption.TeamFalse_And_PickFalse:
                    FilterTemplates(serverCount, hasTeamWait: false, hasPickUp: false);
                    break;
                case FilterOption.TeamFalse_And_PickTrue:
                    FilterTemplates(serverCount, hasTeamWait: false, hasPickUp: true);
                    break;
                case FilterOption.None:
                default:
                   
                    //isFiltered = false;
                    FilterTemplates(serverCount);
                    break;





            }
        }
        public void FilterTemplates(int serverCount, bool? hasTeamWait = null, bool? hasPickUp = null)
        {
            // Start with all templates
            _filteredList.Clear();
            var filteredTemplates = this.Templates.AsQueryable();
            filteredTemplates = filteredTemplates.Where(t => t.ServerCount == serverCount);

            // Filter by HasTeamWait if specified
            if (hasTeamWait.HasValue)
            {
                filteredTemplates = filteredTemplates.Where(t => t.HasTeamWait == hasTeamWait.Value);
            }

            // Filter by HasPickUp if specified
            if (hasPickUp.HasValue)
            {
                filteredTemplates = filteredTemplates.Where(t => t.HasPickUp == hasPickUp.Value);
            }

            _filteredList = filteredTemplates.ToList();
        }
        public int PagesOfPanelsToDisplay(List<FloorplanTemplate> templates)
        {
            return (int)Math.Ceiling((double)templates.Count / 4);
        }
        public List<Panel> PanelsForThisPage(int pageNumber)
        {
            int panelsPerPage = 4;
            int startIndex = (pageNumber - 1) * panelsPerPage;
            int remainingPanels = DisplayPanels.Length - startIndex;
            int count = Math.Min(panelsPerPage, remainingPanels);

            // Extract and return the panels for the requested page
            List<Panel> panelsForPage = new List<Panel>();
            for (int i = 0; i < count; i++)
            {
                panelsForPage.Add(DisplayPanels[startIndex + i]);
            }

            return panelsForPage;
        }
        public void CreateTemplatePanels(List<FloorplanTemplate> templates)
        {
            this.DisplayPanels = new Panel[templates.Count];
            for(int i = 0; i < templates.Count; i++)
            {
                FloorplanTemplate template = templates[i];
                Panel panel = new Panel
                {
                    Size = new Size(268, 375),
                    Location = new Point(0, 0)
                };
                DisplayPanels[i] = panel;
                
               
                DisplayMiniTableControls(template, panel);
                panel.Tag = template;
                
                Button btnView = new Button()
                {
                    BackColor = UITheme.CTAColor,
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(0, 0),
                    Text = "Preview",
                    Size = new Size(268, 30),
                    Font = UITheme.MainFont,
                    Tag = template

                };
                btnView.Click += btnView_Clicked;
                Button btnCancel = new Button()
                {
                    BackColor = UITheme.NoColor,
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(134, 0),
                    Text = "Cancel",
                    Size = new Size(134, 30),
                    Font = UITheme.MainFont,
                    Tag = template, 
                    Visible = false
                };
                btnCancel.Click += btnCancel_Clicked;
                
                panel.Controls.Add(btnView);
                panel.Controls.Add(btnCancel);
                foreach (Control ctrl in panel.Controls)
                {

                    if (ctrl is MiniTableControl tableControl)
                    {

                        foreach (Section section in template.Sections)  
                        {

                            foreach (Table table in section.Tables)
                            {
                                if (tableControl.Table.TableNumber == table.TableNumber)
                                {
                                    tableControl.UpdateSection(section);
                                    tableControl.BackColor = section.Color;
                                    tableControl.Visible = true;
                                    tableControl.Invalidate();
                                    break; 
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnCancel_Clicked(object? sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            btnSender.Visible = false;
            Panel parentPanel = btnSender.Parent as Panel;
            if (parentPanel != null)
            {
                foreach (Control ctrl in parentPanel.Controls)
                {
                    if (ctrl is Button button && ctrl.Text == "Apply")
                    {
                        ctrl.Visible = true;
                        ctrl.Size = new Size(268, btnSender.Height);
                        ctrl.Click -= btnApply_Clicked;
                        ctrl.Click += btnView_Clicked;
                        ctrl.Text = "Preview";
                        ctrl.BackColor = UITheme.CTAColor;
                        break;
                    }
                }
            }
            CancelPreviewedTemplate?.Invoke(btnSender.Tag, e);
        }

        private void btnApply_Clicked(object? sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            ApplyTemplateClicked?.Invoke(btnSender.Tag, e);
        }

        private void btnView_Clicked(object? sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            btnSender.Click -= btnView_Clicked;
            btnSender.Click += btnApply_Clicked;
            btnSender.Size = new Size(134, btnSender.Height);
            btnSender.Text = "Apply";
            btnSender.BackColor = UITheme.YesColor;
            
            Panel parentPanel = btnSender.Parent as Panel;
            if (parentPanel != null)
            {
                foreach (Control ctrl in parentPanel.Controls)
                {
                    if (ctrl is Button button && ctrl.Text == "Cancel")
                    {
                        ctrl.Visible = true;
                        break;
                    }
                }
            }
            PreviewTemplateClicked?.Invoke(btnSender.Tag, e);
        }
        private void SetToPreviewed()
        {

        }
        private void CancelPreview()
        {

        }
    }
}
