using Business;
using NUnit.Framework;

namespace BLTests
{
    public class GameTests
    {
        [TestCase]
        public void TotalCouples_ReturnsCorrectNumberOfCards()
        {
            // Arrange
            Game game = new Game();
            int expectedCouples = 4;

            // Act
            List<Card> cards = game.TotalCouples(expectedCouples);

            // Assert
            Assert.That(cards.Count, Is.EqualTo(expectedCouples * 2)); // Verwacht dat het aantal kaarten correct is.
        }

        [TestCase]
        public void Match_ReturnsTrueForMatchingCards()
        {
            // Arrange
            Game game = new Game();
            Card card1 = new Card(1,1);
            Card card2 = new Card(1,1);

            // Act
            bool isMatch = game.Match(card1, card2);

            // Assert
            Assert.IsTrue(isMatch); // Verwacht dat twee kaarten met dezelfde ID als een match worden beschouwd.
        }

        [TestCase]
        public void Match_ReturnsFalseForNonMatchingCards()
        {
            // Arrange
            Game game = new Game();
            Card card1 = new Card(1,1);
            Card card2 = new Card(2,1);

            // Act
            bool isMatch = game.Match(card1, card2);

            // Assert
            Assert.IsFalse(isMatch); // Verwacht dat twee kaarten met verschillende ID's niet als een match worden beschouwd.
        }

        [TestCase]
        public void TurnCard_ReturnsCardWhenInBounds()
        {
            // Arrange
            Game game = new Game();
            game.AllCards = new List<Card>[2, 2]; // Voorbeeld van een 2x2 grid
            game.AllCards[0, 0] = new List<Card> { new Card(1,1) };
            int expectedCardId = 1;

            // Act
            Card card = game.TurnCard(1, 1);

            // Assert
            Assert.IsNotNull(card); // Verwacht dat een kaart wordt geretourneerd als de coördinaten zich binnen het speelveld bevinden.
            Assert.That(card.ID, Is.EqualTo(expectedCardId)); // Verwacht dat de ID van de geretourneerde kaart correct is.
            Assert.IsTrue(card.Turned); // Verwacht dat de kaart is omgedraaid.
        }

        [TestCase]
        public void TurnCard_ReturnsNullWhenOutOfBounds()
        {
            // Arrange
            Game game = new Game();
            game.AllCards = new List<Card>[2, 2]; // Voorbeeld van een 2x2 grid

            // Act
            Card card = game.TurnCard(3, 3);

            // Assert
            Assert.IsNull(card); // Verwacht dat null wordt geretourneerd als de coördinaten buiten het speelveld vallen.
        }

        [TestCase]
        public void ShuffleCard_ReturnsShuffledCards()
        {
            // Arrange
            Game game = new Game();
            List<Card> cards = new List<Card>
            {
                new Card(1, 1), new Card(2, 1), new Card(3, 1), new Card(4, 1)
            };

            // Act
            List<Card>[,] shuffledCards = game.ShuffleCard(cards);

            // Assert
            // Controleer of de kaarten zijn geschud door te vergelijken met de oorspronkelijke volgorde.
            List<Card> shuffledList = shuffledCards.Cast<List<Card>>().SelectMany(list => list).ToList();
            CollectionAssert.AreNotEqual(cards, shuffledList); // Verwacht dat de geschudde kaarten niet hetzelfde zijn als de oorspronkelijke volgorde.
        }
    }
}
