using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestThatTheCarGetsTheCorrectCarNumberAndThatTheReturnedCarNumberIsPerfectlyCorrectWithoutADoubt()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();

            String Loc10 = "On Sriram's Whale, Next To SQL.";
            String Loc150 = "In Sriram's Office";
            using (mocks.Record())
            {
                mockDatabase.getCarLocation(10);
                LastCall.Return(Loc10);

                mockDatabase.getCarLocation(150);
                LastCall.Return(Loc150);
            }

            var target = new Car(10);
            target.Database = mockDatabase;

            Assert.AreEqual(Loc10, target.getCarLocation(10));
            Assert.AreEqual(Loc150, target.getCarLocation(150));
        }
	}
}
