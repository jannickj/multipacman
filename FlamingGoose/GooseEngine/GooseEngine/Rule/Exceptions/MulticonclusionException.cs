﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooseEngine.Rule.Exceptions
{
    public class MultiConclusionException : Exception
    {
        private List<Conclusion> cs;

        public MultiConclusionException(params Conclusion[] cs)
        {
            this.cs = cs.ToList();
        }
        public override string Message
        {
            get
            {
                string s = "Only one conclusion allowed, however the following conclusions was made: ";

                cs.ForEach(c => s += c.ToString()+", ");

                return  s;
            }
        }
    }
}
