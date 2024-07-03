using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConverter.Models;

namespace UnitConverter.Service
{
    public interface IUnitConverterService
    {
        //double LengthConvert(UnitTypeLength from, UnitTypeLength to, double value);

        //double MassConvert(UnitTypeMass from, UnitTypeMass to, double value);

        //double TimeConvert(UnitTypeTime from, UnitTypeTime to, double value);
        double Convert(int from, int to, double value);

        //test pipeline
    }
}
