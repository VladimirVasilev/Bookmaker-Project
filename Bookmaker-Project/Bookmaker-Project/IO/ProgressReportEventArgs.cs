using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmakerProject.IO
{
    public class ProgressReportEventArgs : EventArgs
    {

        /// <summary>
        /// Reports the progress percentage
        /// </summary>
        /// <param name="progress"> The progress precentage [0-100]</param>
        public ProgressReportEventArgs(byte progress)
        {
            this.progress = progress;
        }
        private double progress;

        public double Progress
        {
            get { return progress; }
            set { progress = value; }
        }
    }
}
