using Moq.AutoMock;
using Xunit;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace VendingMachine.Test
{
    public class TestVendingHandler
    {
        private readonly VendingHandler sut;
        private readonly AutoMocker mocker;

        public TestVendingHandler()
        { 
            mocker = new AutoMocker();
            sut = mocker.CreateInstance<VendingHandler>();
        }

        [Fact]
        public void TestPurchase()
        {
            // assemble
            string expected = "Use this way";
            int index = 0;
            mocker.GetMock<IVending>()
                .Setup(p => p.Purchase(index))
                .Returns(expected);

            // act
            sut.Purchase(index);

            // assert
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine("\n" + expected), Times.Once);
        }

        [Fact]
        public void TestPurchaseHandlesOutOfMoneyException()
        {
            // assemble
            string expected = "Out of money, insert more.";
            int index = 0;
            mocker.GetMock<IVending>()
                .Setup(p => p.Purchase(index))
                .Throws<OutOfMoneyException>();

            // act
            sut.Purchase(index);

            // assert
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine(expected), Times.Once);
        }

        [Fact]
        public void TestInsertMoney()
        {
            // assemble
            string expected = "\nInsert money: ";
            string money = "10";
            mocker.GetMock<IConsoleWrapper>()
                .Setup(p => p.ReadLine())
                .Returns(money);

            // act
            sut.InsertMoney();

            // assert
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.Write(expected), Times.Once);
        }

        [Fact]
        public void TestInsertMoneyHandlesFormatException()
        {
            // assemble
            string expected = "Money must be a number.";
            string money = "a";
            mocker.GetMock<IConsoleWrapper>()
                .Setup(p => p.ReadLine())
                .Returns(money);

            // act
            sut.InsertMoney();

            // assert
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine(expected), Times.Once);
        }

        [Fact]
        public void TestInsertMoneyHandlesDenominatonException()
        {
            // assemble
            string expected = "Insert only money in valid denominations.";
            mocker.GetMock<IConsoleWrapper>()
                .Setup(p => p.ReadLine())
                .Returns("2");
            mocker.GetMock<IVending>()
                .Setup(p => p.InsertMoney(2))
                .Throws<DenominationException>();

            // act
            sut.InsertMoney();

            // assert
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine(expected), Times.Once);
        }

        [Fact]
        public void TestInsertMoneyHandlesException()
        {
            // assemble
            string expected = "Nope, try something else.";
            mocker.GetMock<IConsoleWrapper>()
                .Setup(p => p.ReadLine())
                .Throws<Exception>();

            // act
            sut.InsertMoney();

            // assert
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine(expected), Times.Once);
        }

        [Fact]
        public void TestShowAll()
        {
            // assemble
            string[] input = new string[] { "a", "b", "c" };
            mocker.GetMock<IVending>()
                .Setup(p => p.ShowAll())
                .Returns(input);

            // act
            sut.ShowAll();

            // assert
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine(input[0]), Times.Once);
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine(input[1]), Times.Once);
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine(input[2]), Times.Once);
        }

        [Fact]
        public void TestGetInfos()
        {
            // assemble
            string[] actual;
            string[] expected = new string[] { "a", "b", "c" };
            mocker.GetMock<IVending>()
                .Setup(p => p.ShowAll())
                .Returns(expected);

            // act
            actual = sut.GetInfos();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestMoneyPool()
        {
            // assemble
            int actual = 10;
            string expected = "\n$10";
            mocker.GetMock<IVending>()
                .Setup(p => p.MoneyPool)
                .Returns(actual);

            // act
            sut.MoneyPool();

            // assert
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine(expected), Times.Once);
        }

        [Fact]
        public void TestEndTransaction()
        {
            // assemble
            int money = 16;
            mocker.GetMock<IVending>()
                .Setup(p => p.MoneyPool)
                .Returns(money);
            mocker.GetMock<IVending>()
                .Setup(p => p.Denominations)
                .Returns(new int[] { 1, 5, 10 });
            mocker.GetMock<IVending>()
                .Setup(p => p.EndTransaction())
                .Returns(new int[] { 1, 1, 1 });

            // act
            sut.EndTransaction();

            // assert
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine($"\nYour change: ${money}"), Times.Once);
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine("[${0}] x {1}", 1, 1), Times.Once);
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine("[${0}] x {1}", 5, 1), Times.Once);
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine("[${0}] x {1}", 10, 1), Times.Once);
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine("\nThank you for your bussiness, come back soon."), Times.Once);
        }
    }
}
