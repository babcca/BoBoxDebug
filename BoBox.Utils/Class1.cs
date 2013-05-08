﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Utils
{
    public interface IConsole
    {
        void Log(string format, params object[] args);
        void Error(string format, params object[] args);
    }    
}
