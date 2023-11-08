using Business;
using NUnit.Framework;

namespace BLTests
{

   
    public class CardTests
    {
        [TestCase]
        public void Card_Constructor_InitializesProperties()
        {
            // Arrange
            int expectedId = 1;
            bool expectedTurned = false;

            // Act
            Card card = new Card(expectedId);

            // Assert
            Assert.That(card.ID, Is.EqualTo(expectedId));
            Assert.That(card.Turned, Is.EqualTo(expectedTurned));
        }
    }
}
