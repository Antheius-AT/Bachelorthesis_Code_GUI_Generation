﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Metadata;

namespace Models.UseCases.IncludingUserInteraction.UseCase3
{
    public class EditToolBox
    {
        public EditToolBox()
        {
            SelectedTool = Tools.Pencil;
            StrokeWidth = 1;
            NewImageCommand = () => Console.WriteLine("New image pressed");
            SaveImageCommand = () => Console.WriteLine("Save image pressed");
        }

        [Editable]
        public Tools SelectedTool { get; set; }

        [Editable]
        public int StrokeWidth { get; set; }

        public Action NewImageCommand { get; set; }

        public Action SaveImageCommand { get; set; }
    }
}
