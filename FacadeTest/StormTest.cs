using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FacadeTest
{
    [TestClass]
    public class StormTest
    {
        const int nDucks = 65;

        [TestMethod]
        public void FillTest()
        {            
            Storm<MallardDuck> storm = new Storm<MallardDuck>();
            storm.FillStorm(nDucks);
            Assert.AreEqual(nDucks, storm.Ducks.Count);
        }

        [TestMethod]
        public void FillExceptionTest()
        {            
            Storm<MallardDuck> storm = new Storm<MallardDuck>();
            try
            {
                storm.FillStorm(-15);
                Assert.Fail();
            }
            catch(Exception){

            }

            Assert.AreEqual(0, storm.Ducks.Count);
        }

        [TestMethod]
        public void MigrateTest()
        {
            Storm<MallardDuck> storm = new Storm<MallardDuck>();
            storm.FillStorm(nDucks);

            //migrazione
            storm.Migrate(Direction.NORD, 25.3);
            storm.Migrate(Direction.OVEST, 25.3);
            storm.Migrate(Direction.SUD, 25.3);
            storm.Migrate(Direction.EST, 25.3);  

            Assert.AreEqual(0d, storm.PositionX);
            Assert.AreEqual(0d, storm.PositionY);
            Assert.AreEqual(0d, storm.LineDistanceFromStart);

            //altra migrazione
            storm.Migrate(Direction.NORD, 25.3);
            storm.Migrate(Direction.EST, 67.3);
            
            Assert.AreEqual(67.3, storm.PositionX);
            Assert.AreEqual(25.3, storm.PositionY);
            Assert.AreEqual(71.90, storm.LineDistanceFromStart);

            storm.Migrate(Direction.SUD, 30.9);
            storm.Migrate(Direction.EST, 14.9);

            Assert.AreEqual(82.2, storm.PositionX);
            Assert.AreEqual(-5.6, storm.PositionY);
            Assert.AreEqual(82.39, storm.LineDistanceFromStart);

            Assert.AreEqual(storm.TotalDistance, 239.6);            

            Assert.IsTrue(storm.Ducks.TrueForAll(dk=>dk.TotalFly == storm.TotalDistance));
        }

        [TestMethod]
        public void MigrateExceptionTest()
        {
            Storm<MallardDuck> storm = new Storm<MallardDuck>();

            try 
            {
                storm.Migrate(Direction.NORD, 25.3);
                Assert.Fail();
            }
            catch(Exception){
                
            }

            Assert.AreEqual(0d, storm.PositionX);
            Assert.AreEqual(0d, storm.PositionY);

            Assert.AreEqual(storm.TotalDistance, 0d);            

            Assert.IsTrue(storm.Ducks.TrueForAll(dk=>dk.TotalFly == storm.TotalDistance));
        }
    }
}
