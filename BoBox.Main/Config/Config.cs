using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Main.Config
{
    public class Config : ConfigData
    {
    }

    public class ConfigData
    {
        [Category("Bobolang Settings")]
        [DisplayName("Minimal version")]
        public int MinimalVersion { get; set; }

        [Category("Bobolang Settings")]
        [DisplayName("Bobolang compiler path")]
        public string Path { get; set; }



        [Category("Bobolang Editor")]
        [DisplayName("Verze")]
        public string SyntaxDefinitionPath { get; set; }
    }
}
