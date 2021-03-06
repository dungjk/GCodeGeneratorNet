﻿using GCodeGeneratorNet.Core.GCodes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeGeneratorNet.Core
{
    public class GCodeStringBuilder
    {
        static CultureInfo ci = new CultureInfo("en-us");
        public static string GCodeToString(IGCode gcode)
        {
            var sb = new StringBuilder();
            sb.Append(gcode.Type);
            sb.Append(gcode.Code.ToString("D2"));
            foreach (var prop in gcode.GetType().GetProperties().Where(p => p.PropertyType == typeof(float?) && ((float?)p.GetValue(gcode)).HasValue)
                .Concat(gcode.GetType().GetProperties().Where(p => p.PropertyType == typeof(float))))
            {
                sb.Append(" ");
                sb.Append(prop.Name);
                float? val = (float?)prop.GetValue(gcode);
                sb.Append(val.Value.ToString("F4", ci));
            }
            return sb.ToString();
        }
    }
}
