namespace RenjuTests;

[TestFixture]
public class BoardTests
{
    private RenjuBoard _board;

    [SetUp]
    public void Init()
    {
        _board = Mock.Of<RenjuBoard>();
    }

    [Test]
    public void ShouldAddMoveWithinBoard()
    {
        // Arrange
        Move m = new(0, 0, CellStone.Black);
        // Act
        _board.AddMove(m);
        // Assert
        Assert.That(_board[0, 0].Stone, Is.EqualTo(CellStone.Black));
    }

    [Test]
    public void ShouldNotAddMoveOutsideBoard()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            Move m = new(13, 13, CellStone.Black);
            _board.AddMove(m);
        });
    }

    [Test]
    public void ShouldNotAddMoveToOccupiedCell()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            Move m1 = new(0, 0, CellStone.Black);
            Move m2 = new(0, 0, CellStone.White);
            _board.AddMove(m1);
            _board.AddMove(m2);
        });
    }

    [Test]
    public void ShouldNotAddMoveWithEmptyStone()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Move m = new(0, 0, CellStone.Empty);
            _board.AddMove(m);
        });
    }

    [Test]
    public void IsTrueOnCorrectBounds()
    {
        for (int i = 0; i < 13; i++)
        for (int j = 0; j < 13; j++)
        {
            Intersection intersection = new(i, j, CellStone.Black);
            bool isWithin = RenjuBoard.IsWithinBounds(intersection.X, intersection.Y);
            Assert.That(isWithin, Is.True);
        }
    }

    [Test]
    public void ShouldThrowOnIncorrectBounds()
    {
        Assert.Multiple((() =>
        {
            Intersection wrong1 = new(13, 13, CellStone.Black),
                wrong2 = new(-1, -1, CellStone.Black);
            
            bool isWithin1 = RenjuBoard.IsWithinBounds(wrong1.X, wrong1.Y),
                isWithin2 = RenjuBoard.IsWithinBounds(wrong2.X, wrong2.Y);

            Assert.That(isWithin1, Is.False);
            Assert.That(isWithin2, Is.False);
        }));
    }
}
