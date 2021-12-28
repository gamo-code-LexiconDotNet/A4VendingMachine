using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace VendingMachine.Test
{
    public class TestMenu
    {
        private readonly Menu sut;
        private readonly AutoMocker mocker;

        public TestMenu()
        {
            mocker = new AutoMocker();
            sut = mocker.CreateInstance<Menu>();

            sut.Title = "Title";
            sut.Heading = "Heading";
            sut.ExitOption = "0";
            sut.AddItem("0", "Exit", new Action(() => { }));
            sut.AddItem("1", "One", new Action(() => new string("")));
        }

        [Fact]
        public void DisplayMenu()
        {
            // assemble
            string menu =
                "Heading\n\n" +
                "Title\n" +
                "0) Exit.\n" +
                "1) One.\n";

            mocker.GetMock<IConsoleWrapper>()
                .Setup(p => p.ReadLine())
                .Returns("0");

            // act
            sut.Run();

            // Assert
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.Write(menu), Times.Once);

        }

        [Fact]
        public void HoldAfterSelectionAnReturn()
        {
            // assemble
            string press = "\n\n\tPress any key to continue...";

            mocker.GetMock<IConsoleWrapper>()
                .SetupSequence(p => p.ReadLine())
                .Returns("1")
                .Returns("0");

            // act
            sut.Run();

            // Assert
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.Write(press), Times.Once);
        }

        [Fact]
        public void CatchInvalidMenuOption()
        {
            // assemble
            string menu =
               "Heading\n\n" +
                "Title\n" +
                "0) Exit.\n" +
                "1) One.\n";

            mocker.GetMock<IConsoleWrapper>()
                .SetupSequence(p => p.ReadLine())
                .Returns("2")
                .Returns("x")
                .Returns("0");

            // act
            sut.Run();

            // Assert
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.Write(menu), Times.Exactly(3));
        }

        [Fact]
        public void CatchExceptions()
        {
            // assemble
            string expected = "Exception has been thrown by the target of an invocation.";
            sut.AddItem("ex", "Exception", new Action(() => throw new Exception()));

            mocker.GetMock<IConsoleWrapper>()
                .SetupSequence(p => p.ReadLine())
                .Returns("ex")
                .Returns("0");

            // act
            sut.Run();

            // Assert
            mocker.GetMock<IConsoleWrapper>()
                .Verify(p => p.WriteLine(expected), Times.Once);
        }
    }
}
