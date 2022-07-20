using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MarvelApp.UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        readonly Func<AppQuery, AppQuery> shellHamburger = e => e.Marked("HamburgerShell");
        readonly Func<AppQuery, AppQuery> flyoutItemComics = e => e.Marked("ComicsFlyoutItem");

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        [Category("Display")]
        public void Display_Welcome_Text_In_10_Seconds_UITest()
        {
            //Arrange
            var timeout = new TimeSpan(0, 0, 10);

            //Act
            AppResult[] results = app.WaitForElement("Welcome to the world's greatest comics!", $"Welcome Text not appeared in {timeout.Seconds} seconds", timeout);

            app.Screenshot("Welcome screen.");

            //Assert
            Assert.IsTrue(results.Any());
        }

        [Test]
        [Category("Display")]
        public void Display_On_Sale_Comics_UITest()
        {
            //Arrange

            //Act
            AppResult[] results = app.WaitForElement("OnSaleComicImage");

            app.Screenshot("On Sale screen.");

            //Assert
            Assert.IsTrue(results.Any());
        }

        [Test]
        [Category("Search")]
        public void Search_Comics_And_Display_It_At_Least_3_On_Screen_UITest()
        {
            //Arrange

            //Act
            app.Tap(shellHamburger);
            app.Tap(flyoutItemComics);

            AppResult[] results2 = app.WaitForElement(x => x.Marked("ComicsCollectionView").Child());

            app.Screenshot("Comics screen.");

            //Assert
            Assert.GreaterOrEqual(results2.Length, 3);
        }
    }
}
