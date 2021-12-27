using System;
using Xunit;

namespace VendingMachine.Test
{
    public class TestVending
    {
        private Vending sut;

        [Fact]
        public void TestAddProductAndShowAll()
        {
            // assemble
            Product toy;
            Product candy;
            string[] expected;

            // act
            expected = new string[] { "Ball: $10", "Gummy Bears: $5" };
            toy = new Toy("Ball", 10);
            candy = new Candy("Gummy Bears", 5);
            sut = new Vending();

            sut.AddProduct(toy);
            sut.AddProduct(candy);

            // assert
            Assert.Equal(expected, sut.ShowAll());

        }

        [Fact]
        public void TestMultipleAddProductAndShowAll()
        {
            // assemble
            Product toy1;
            Product toy2;
            Product candy1;
            Product candy2;
            Product[] products;
            string[] expected;

            // act
            toy1 = new Toy("Ball", 15);
            toy2 = new Toy("Tux", 1);
            candy1 = new Candy("Gummy Bears", 5);
            candy2 = new Candy("Jelly beans", 7);
            products = new Product[] { toy1, toy2, candy1, candy2 };
            expected = new string[] { "Ball: $15", "Tux: $1", "Gummy Bears: $5", "Jelly beans: $7" };

            sut = new Vending();
            sut.AddProduct(products);

            // assert
            Assert.Equal(expected, sut.ShowAll());

        }

        [Theory]
        [InlineData(2)]
        [InlineData(6)]
        [InlineData(104)]
        public void TestInsertMoneyThrowsOnIncorrectDenomination(int money)
        {
            // assemble

            // act
            sut = new Vending();

            // assert
            Assert.Throws<DenominationException>(() => sut.InsertMoney(money));
        }

        [Theory]
        // 1000, 500, 100, 50, 20, 10, 5, 1 
        [InlineData(new int[] { 1, 5, 10, 20, 50, 100, 500, 1000 }, new int[] { 1, 1, 1, 1, 1, 1, 1, 1 })]
        [InlineData(new int[] { 50, 50, 50 }, new int[] { 0, 0, 1, 1, 0, 0, 0, 0 })]
        [InlineData(new int[] { 500, 500, 50, 50 }, new int[] { 1, 0, 1, 0, 0, 0, 0, 0 })]
        public void TestInsertMoneyAndReturns(int[] money, int[] expected)
        {
            // assemble
            int[] actual;

            // act
            sut = new Vending();

            foreach (int m in money)
            {
                sut.InsertMoney(m);
            }

            actual = sut.EndTransaction();

            // assert
            Assert.Equal<int[]>(expected, actual);
            
        }

        [Fact]
        public void TestSuccessfullPurchase()
        {
            // assemble
            Product toy1;
            Product toy2;
            Product candy1;
            Product candy2;
            Product[] products;
            string expected1;
            string expected2;

            // act
            toy1 = new Toy("Ball", 15);
            toy2 = new Toy("Tux", 1);
            candy1 = new Candy("Gummy Bears", 5);
            candy2 = new Candy("Jelly beans", 7);
            products = new Product[] { toy1, toy2, candy1, candy2 };
            expected1 = "Play with the Ball, fun.";
            expected2 = "Play with the Tux, fun.";

            sut = new Vending();
            sut.AddProduct(products);
            sut.InsertMoney(1000);

            // assert
            Assert.Equal(expected1, sut.Purchase(0));
            Assert.Equal(expected2, sut.Purchase(1));
        }

        [Fact]
        public void TestUnsuccessfullPurchase()
        {
            // assemble
            Product candy;

            // act
            candy = new Candy("Jelly beans", 7);
            sut = new Vending();
            sut.AddProduct(candy);
            sut.InsertMoney(1);

            // assert
            Assert.Throws<OutOfMoneyException>(() => sut.Purchase(0));
        }

    }
}
