using NUnit.Framework;
using MusicApp.Core;
namespace MusicAppTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FileHelperTest()
        {
            var a = FileHelper.GetImageExtension("qwejqwekwqekwq.png");
            Assert.AreEqual(a, FileHelper.ImageExtension.PNG);
        }
    }
}