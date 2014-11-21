using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EVALab.Analysis.ROI;

namespace TestProject
{
    [TestClass]
    public class UnitTestSequencing
    {
        [TestMethod]
        public void TestMethod1()
        {

            Random rnd = new Random();
            double x = rnd.NextDouble() * 25 + 75;
            double y = rnd.NextDouble() * 25 + 75;


            SquareROI rois = new SquareROI("test", 100, 100, 50, 50);
            Assert.IsTrue(rois.IsInRoi(x, y));
        }
    }
}
