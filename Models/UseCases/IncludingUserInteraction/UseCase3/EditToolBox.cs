using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UseCases.IncludingUserInteraction.UseCase3
{
    public class EditToolBox
    {
        public EditToolBox()
        {
            SelectedTool = Tools.Pencil;
            StrokeWidth = 1;
        }

        public Tools SelectedTool { get; set; }

        public int StrokeWidth { get; set; }
    }
}
