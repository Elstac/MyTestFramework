using System;
using System.Collections.Generic;
using System.Text;

namespace Core.PrivateTester
{
    struct ErrorMessage
    {
        public int ParametersProvided { get; set; }
        public int ParametersExpected { get; set; }

        public override string ToString()
        {
            return $"Expected arguments: {ParametersExpected}." +
                   $" Provided arguments: {ParametersProvided}";
        }

    }
}
