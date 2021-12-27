using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace VendingMachine.Test
{
    public class TestProducts
    {
        private Product sut;
        private string name;
        private int price;
        private string examine;
        private string use;

        [Fact]
        public void TestIcecream()
        {
            // assemble
            name = "Gelato Chocolato";
            price = 25;
            examine = name + ": $" + price;
            use = "Eat the " + name + ", brain freeze.";

            // act
            sut = new IceCream(name, price);

            // assert
            Assert.Equal(sut.Name, name);
            Assert.Equal(sut.Price, price);
            Assert.Equal(sut.Examine, examine);
            Assert.Equal(sut.Use, use);
        }

        [Fact]
        public void TestToy()
        {
            // assemble
            name = "Rubber Ball";
            price = 30;
            examine = name + ": $" + price;
            use = "Play with the " + name + ", fun.";

            // act
            sut = new Toy(name, price);

            // assert
            Assert.Equal(sut.Name, name);
            Assert.Equal(sut.Price, price);
            Assert.Equal(sut.Examine, examine);
            Assert.Equal(sut.Use, use);
        }

        [Fact]
        public void TestDrink()
        {
            // assemble
            name = "Tea";
            price = 30;
            examine = name + ": $" + price;
            use = "Drink the " + name + ", refreshing.";

            // act
            sut = new Drink(name, price);

            // assert
            Assert.Equal(sut.Name, name);
            Assert.Equal(sut.Price, price);
            Assert.Equal(sut.Examine, examine);
            Assert.Equal(sut.Use, use);
        }

        [Fact]
        public void TestFood()
        {
            // assemble
            name = "Crisps";
            price = 5;
            examine = name + ": $" + price;
            use = "Eat the " + name + ", nourishing.";

            // act
            sut = new Food(name, price);

            // assert
            Assert.Equal(sut.Name, name);
            Assert.Equal(sut.Price, price);
            Assert.Equal(sut.Examine, examine);
            Assert.Equal(sut.Use, use);
        }

        [Fact]
        public void TestCandy()
        {
            // assemble
            name = "Jelly beans";
            price = 10;
            examine = name + ": $" + price;
            use = "Eat the " + name + ", sugar rush.";

            // act
            sut = new Candy(name, price);

            // assert
            Assert.Equal(sut.Name, name);
            Assert.Equal(sut.Price, price);
            Assert.Equal(sut.Examine, examine);
            Assert.Equal(sut.Use, use);
        }
    }
}
